using System;
using UnityEngine;

namespace CombatSystem
{
    [Serializable]
    public class StatusEffecOverrider
    {
        [SerializeField]
        private StatusEffect statusEffect;

        public StatusEffecOverrider(StatusEffect statusEffect)
        {
            this.statusEffect = statusEffect;
        }

        public StatusEffect StatusEffects => statusEffect;
        
        [SerializeField]
        private bool overrideThis;
        [SerializeField]
        private int coolDown; 
        [SerializeField]
        SelectorMode team;
        [SerializeField]
        SelectorType type;


        public StatusEffectHelper GetValues()
        {
            StatusEffectHelper seHelper = null;
               

            if (overrideThis)
            {
                seHelper = new StatusEffectHelper(statusEffect, coolDown, team, type);
            }
            else
            {
                seHelper = new StatusEffectHelper(statusEffect);
            }

            return seHelper;
        }

    }
}






