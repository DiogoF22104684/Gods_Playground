using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;

[System.Serializable]
public class BattleEntity 
{

    private BattleStat hp;

    [MoveAffecter]
    public BattleStat Hp 
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            
            if(value.Stat <= 0)
            {
                properEntity.PlayAnimation(DefaultAnimations.Death);
                IsDead = true;
                value.Stat = 0;
            }
            if(value.Stat > value.MaxStat)
            {
                value.Stat = value.MaxStat;
            }
            properEntity.ChangeValue("hp", hp.Stat);
        } 
    }


    private BattleStat mp;

    [MoveAffecter]
    public BattleStat Mp
    {
        get
        {
            return mp;
        }
        set
        {
            mp = value;

            if (value.Stat <= 0)
            {               
                value.Stat = 0;
            }
            properEntity.ChangeValue("mp", mp.Stat);
        }
    }


    [MoveAffecter]
    public BattleStat str { get; set; }

    [MoveAffecter]
    public BattleStat def { get; set; }

    [MoveAffecter]
    public BattleStat dex { get; set; }

    [MoveAffecter]
    public BattleStat turns { get; set; }

    public List<TurnTimer<StatusEffect>> statusEffects { get; private set; }
    public List<TurnTimer<BattleMove>> skillCooldowns { get; private set; }

    public EntityTemplate template { get; }

    public BattleEntityProper properEntity { get; }

    public System.Action onStatusEffectUpdate;

    public BattleEntity(BattleEntityProper proper, EntityTemplate template)
    {
        properEntity = proper;
        int hpValue = template.HP;
        this.hp = new BattleStat(hpValue, hpValue, 0);

        properEntity = proper;
        int mpValue = template.Mp;
        this.mp = new BattleStat(mpValue, mpValue, 0);

        turns = new BattleStat(1,1,0);

        float strTemp = (template.Str * 2) / 100f;
        str = new BattleStat (strTemp, strTemp, 0);

        float defTemp = (template.Def * 2) / 100f;
        def = new BattleStat(defTemp, defTemp, 0);

        float dextemp = (template.Dex * 2) / 100f;
        dex = new BattleStat(template.Dex, template.Dex, 0);

        this.template = template;
        statusEffects = new List<TurnTimer<StatusEffect>> { };
        skillCooldowns = new List<TurnTimer<BattleMove>> { };
    }

    public bool IsDead { get; private set; }

    public override string ToString()
    {
        return $"Hp: {hp.Stat} \nAtk: {str.Stat} \nDef:{def.Stat}";
    }

    public void AddDebuff(StatusEffect d)
    { 
        statusEffects.Add(new TurnTimer<StatusEffect>(d));
        onStatusEffectUpdate?.Invoke();
    }

    public void AddSkillCooldown(BattleMove move)
    {
        skillCooldowns.Add(new TurnTimer<BattleMove> (move));
    }


    internal void ResolveDebuffs()
    {
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {
           
            TurnTimer<StatusEffect> d = statusEffects[i];
            
            StatusEffect effect = d.Effect;

            effect.ResolveStatusEffect(this, d.Timer);

            //Debug.Log("TimePassed: " + d.Timer + "Duration:" + effect.CoolDown);

            d.Timer++;

            if (d.Timer >= effect.CoolDown)
            {
                effect.EndStatusEffect(this);
                statusEffects.RemoveAt(i);
            }
        }  
    }

    //there's probably a better way for this
    internal void fowardSkillTimer()
    {
        for (int i = skillCooldowns.Count - 1; i >= 0; i--)
        {
            TurnTimer<BattleMove> timer = skillCooldowns[i];
            if (timer.Timer >= timer.Effect.Config.Cooldown)
            {
                skillCooldowns.Remove(timer);
                continue;
            }
            timer.Timer++; 
        }
    }
}
