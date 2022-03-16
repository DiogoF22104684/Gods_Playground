using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    [CreateAssetMenu(menuName = "Scriptables/Moves/Move")]
    public class BattleMove : ScriptableObject
    {
        [SerializeField]
        private MoveFunction function;

        [SerializeField]
        private MoveConfig config;


        public void Function(BattleEntity attacker, BattleEntity target)
        {
            function.Function(attacker, target);
        }

    }
}