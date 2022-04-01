using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                value.Stat = 0;
            }
            properEntity.ChangeValue("hp", hp.Stat);
        } 
    }

    [MoveAffecter]
    public BattleStat str { get; set; }

    [MoveAffecter]
    public BattleStat def { get; set; }

    [MoveAffecter]
    public BattleStat dex { get; set; }

    [MoveAffecter]
    public bool hadTurn { get; set; }

    public List<StatusEffectTimer> statusEffects { get; private set; }

    public EntityTemplate template { get; }

    public BattleEntityProper properEntity { get; }

    public System.Action onStatusEffectUpdate;

    public BattleEntity(BattleEntityProper proper, EntityTemplate template)
    {
        properEntity = proper;
        this.hp = new BattleStat(template.HP, template.HP, 0);

        float strTemp = (template.Str * 2) / 100f;
        str = new BattleStat (strTemp, strTemp, 0);

        float defTemp = (template.Def * 2) / 100f;
        def = new BattleStat(defTemp, defTemp, 0);

        float dextemp = (template.Dex * 2) / 100f;
        dex = new BattleStat(template.Dex, template.Dex, 0);

        this.template = template;
        statusEffects = new List<StatusEffectTimer> { };
    }


    public override string ToString()
    {
        return $"Hp: {hp.Stat} \nAtk: {str.Stat} \nDef:{def.Stat}";
    }

    internal void AddDebuff(StatusEffect d)
    { 
        statusEffects.Add(new StatusEffectTimer(d));
        onStatusEffectUpdate?.Invoke();
    }

    internal void ResolveDebuffs()
    {
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {
            StatusEffectTimer d = statusEffects[i];
            StatusEffect effect = d.Effect;

            effect.ResolveStatusEffect(this);

            //Debug.Log("TimePassed: " + d.Timer + "Duration:" + effect.CoolDown);

            d.Timer++;

            if(d.Timer >= effect.CoolDown)
            {
                effect.EndStatusEffect(this);
                statusEffects.RemoveAt(i);
            }
        }  
    }
}
