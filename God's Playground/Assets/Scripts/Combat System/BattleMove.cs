using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CombatSystem
{
    [CreateAssetMenu(menuName = "Scriptables/Moves/Move")]
    public class BattleMove : ScriptableObject
    {
        [SerializeField]
        private Texture2D icon;

        //N devia ser feito todas as calls, so qd muda a property
        public Sprite Icon => icon.ToSprite();

        [SerializeField]
        private MoveConfig config;
        public MoveConfig Config => config;

        [SerializeField]
        private BattleEffects debuffs;
        public BattleEffects Debuffs => debuffs;

       

        private BattleAffects functionProp;

        [SerializeField]
        private BasicAffect basicFunc;

        [SerializeField]
        private BasicIgnoreDef basicIgnoreDef;

        public bool IsUsable(BattleEntityProper playerEntity,
            BattleEntityProper selectedEntity)
        {
            BattleEntity player = playerEntity.entityData;
            BattleEntity target = selectedEntity.entityData;

            if(selectedEntity is EnemyBattleEntityProper)
            {
                if (config.Mode == SelectorMode.Self || config.Mode == SelectorMode.Team)
                    return false;
            }
            else
            {
                if (config.Mode == SelectorMode.Adversary)
                    return false;
            }
            
            BattleStat stat = config.CostStat.GetValue(player);

            if (stat.Stat < config.CostValue)
                return false;

            return true;
        }

        [SerializeField]
        private BasicFlat basicFlat;

      
        [SerializeField]
        private FuncType function;

        [SerializeField]
        private BattleEffects Debuff;

        public void Function(BattleEntity attacker, IEnumerable<BattleEntity> target, float roll)
        {
            if(functionProp == null)
            {
                SelectFunc((int)function);
            }

            functionProp.Function(attacker, target, roll);

            foreach (BattleEntity be in target)
            {
                foreach(StatusEffect d in debuffs.Debuffs)
                {
                    be.AddDebuff(d);
                }
            }

            BattleStat stat = config.CostStat.GetValue(attacker);
            BattleStat newStat = 
                new BattleStat(stat.Stat - Config.CostValue, stat.MaxStat, stat.FlatStat);
            config.CostStat.param.SetValue(attacker, newStat);

        }

        public void SelectFunc(int selected)
        {
            switch (selected)
            {
                case 0:
                    functionProp = basicFunc;
                    break;
                case 1:
                    functionProp = basicIgnoreDef;
                    break;
                case 2:
                    functionProp = basicFlat;
                    break;
            }
        }
      

    }

    public enum FuncType
    {
        Basic,
        BasicIgnoreDef,
        BasicFlat
    }
    public enum MechanicType
    {
        None,
        ActionPoints
    }

}





