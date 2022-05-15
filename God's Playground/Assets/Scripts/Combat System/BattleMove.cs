using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CombatSystem
{
    /// <summary>
    /// Move to be used during combat
    /// </summary>
    [CreateAssetMenu(menuName = "Scriptables/Moves/Move")]
    public class BattleMove : ScriptableObject, IInventoryItem
    {
        //Texture to be used in the skill menu
        [SerializeField]
        private Texture2D icon;

        //N devia ser feito todas as calls, so qd muda a property
        /// <summary>
        /// Icon used in the skill menu
        /// </summary>
        public Sprite Icon => icon.ToSprite();

        //Move config
        [SerializeField]
        private MoveConfig config;
        
        /// <summary>
        /// Move config 
        /// </summary>
        public MoveConfig Config => config;

        //Status Effects applied when the move is used
        [SerializeField]
        private BattleEffects statusEffect;

        /// <summary>
        /// Status Effects applied when the move is used
        /// </summary>
        public BattleEffects StatusEffects => statusEffect;

        #region Battle Affects
        //Function currently selected. Used to resolve the effect
        private BattleAffects selectedFuncAffect;

        //Basic Function
        [SerializeField]
        private BasicAffect basicFunc;

        //Basic Funcition without counting the adversary Def
        [SerializeField]
        private BasicIgnoreDef basicIgnoreDef;

        //Basic Function without stat aditives. Flat Value
        [SerializeField]
        private BasicFlat basicFlat;

        //Type of Affect Selected
        [SerializeField]
        private FuncAffectType function;
        #endregion


        /// <summary>
        /// Check if the move can be used in the current combat state.
        /// </summary>
        /// <param name="entityUsingMove">Entity using the move.</param>
        /// <param name="selectedEntity">Target entity selected.</param>
        /// <returns>True if the move can be used.</returns>
        public bool IsUsable(BattleEntityProper entityUsingMove,
            BattleEntityProper selectedEntity)
        {
            //Get Battle Entities from EntityProper
            BattleEntity attacker = entityUsingMove.entityData;
            BattleEntity target = selectedEntity.entityData;

            //Check if the entities are in the same team
            bool isSameTeam  = attacker.IsSameTeam(target);

            //Compare the target with the skillmode 
            if (!isSameTeam)
            {
                if (config.Mode == SelectorMode.Self || config.Mode == SelectorMode.Team)
                    return false;
            }
            else
            {
                if (config.Mode == SelectorMode.Adversary)
                    return false;
            }
            
            BattleStat stat = config.CostStat.GetValue(attacker);

            //Check if the skill colldown
            if (entityUsingMove.skillInCooldown(this))
            {
                return false;
            }

            //Check if the user has enough stats to 'pay' for the cost of
            //using the skill
            if (stat.Stat < config.CostValue)
                return false;

            return true;
        }

    
        /// <summary>
        /// Result of using the battle move.
        /// </summary>
        /// <param name="attacker">User using the move.</param>
        /// <param name="target">Targets of the move.</param>
        /// <param name="roll">Roll value of the dice and quick time events.</param>
        public void Function(BattleEntity attacker, IEnumerable<BattleEntity> target, float roll)
        {
            //Check what affect function is selected.
            if(selectedFuncAffect == null)
            {
                SelectFunc((int)function);
            }

            //Calculate the result of the move and apply it to the targets.
            selectedFuncAffect.Function(attacker, target, roll);

            //Apply the status effects on the targets
            foreach (BattleEntity be in target)
            {
                foreach(StatusEffect d in statusEffect.StatusEffects)
                {
                    be.AddDebuff(d);
                }
            }

            //If aplicable remove the cost value from the user stats 
            BattleStat stat = config.CostStat.GetValue(attacker);
            //Apply modified cost value to the user
            BattleStat newStat = 
                new BattleStat(stat.Stat - Config.CostValue, stat.MaxStat, stat.FlatStat);
           
            config.CostStat.param.SetValue(attacker, newStat);
        }


        /// <summary>
        /// Select Function Affect 
        /// </summary>
        /// <param name="selected">Value corresponding to the affect func.</param>
        public void SelectFunc(int selected)
        {
            switch (selected)
            {
                case 0:
                    selectedFuncAffect = basicFunc;
                    break;
                case 1:
                    selectedFuncAffect = basicIgnoreDef;
                    break;
                case 2:
                    selectedFuncAffect = basicFlat;
                    break;
            }
        }
      

    }

}





