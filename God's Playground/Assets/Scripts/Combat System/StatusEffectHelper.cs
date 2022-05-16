using System;
using UnityEngine;

namespace CombatSystem
{
    //I think this class is redundant
    public class StatusEffectHelper
    {
        public Sprite Icon { get; private set; }

        public int CoolDown { get; private set; }

        public StatusEffectType EffectType { get; private set; }

        public Action<BattleEntity, int> Effect { get; private set; }
        public Action<BattleEntity> EndEffect { get; private set; }

        public SelectorMode Team { get; private set; }

        public SelectorType Type { get; private set; }

        public StatusEffectHelper(StatusEffect statusEffect)
        {
            Icon = statusEffect.Icon;
            CoolDown = statusEffect.CoolDown;
            EffectType = statusEffect.EffectType;
            Effect = statusEffect.ResolveStatusEffect;
            EndEffect = statusEffect.EndStatusEffect;
            Team = SelectorMode.Adversary;
            Type = SelectorType.Solo;
        }

        public StatusEffectHelper(StatusEffect statusEffect, int cooldown, 
            SelectorMode team, SelectorType type)
        {
            Icon = statusEffect.Icon;
            CoolDown = cooldown;
            EffectType = statusEffect.EffectType;
            Effect = statusEffect.ResolveStatusEffect;
            EndEffect = statusEffect.EndStatusEffect;
            Team = team;
            Type = type;
        }
    }
}






