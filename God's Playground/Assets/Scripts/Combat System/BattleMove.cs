using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    [CreateAssetMenu(menuName = "Scriptables/Moves/Move")]
    public class BattleMove : ScriptableObject
    {
        [SerializeField]
        private MoveConfig config;
        public MoveConfig Config => config;

        
        private BattleAffects functionProp;

        [SerializeField]
        private BasicAffect basicFunc;

        [SerializeField]
        private FuncType function;

        public void Function(BattleEntity attacker, IEnumerable<BattleEntity> target, float roll)
        {
            if(functionProp == null)
            {
                SelectFunc((int)function);
            }

            functionProp.Function(attacker, target, roll);
        }

        public void SelectFunc(int selected)
        {
            switch (selected)
            {
                case 0:
                    functionProp = basicFunc;
                    break;
            }
        }
      

    }

    public enum FuncType
    {
        Basic
    }
}





