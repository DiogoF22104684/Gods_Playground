using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleEntity 
{

    private float hp;

    [MoveAffecter]
    public float Hp 
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            if(value <= 0)
            {
                properEntity.PlayAnimation(DefaultAnimations.Death);
                hp = 0;
            }
            properEntity.ChangeValue("hp", hp);
        } 
    }

    [MoveAffecter]
    public float atk { get; set; }

    [MoveAffecter]
    public float def { get; set; }

    [MoveAffecter]
    public float dex { get; set; }


    [MoveAffecter]
    public bool hadTurn { get; set; }

    private List<Debuff> debuffList;


    public EntityTemplate template { get; }

 
    public BattleEntityProper properEntity { get; }

    public BattleEntity(BattleEntityProper proper, EntityTemplate template)
    {
        properEntity = proper;
        this.hp = template.HP;
        atk = (template.Str * 2) / 100f;
        def = (template.Def * 2) / 100f;
        dex = template.Dex;
        this.template = template;
        debuffList = new List<Debuff> { };
    }


    public override string ToString()
    {
        return $"Hp: {hp} \nAtk: {atk} \nDef:{def}";
    }

    internal void AddDebuff(Debuff d)
    {    
        debuffList.Add(d);
    }

    internal void ResolveDebuffs()
    {
        for (int i = debuffList.Count - 1; i >= 0; i--)
        {
            Debuff d = debuffList[i];
            d.ResolveDebuff(this);

            d.timePasse++;

            if(d.timePasse >= d.CoolDown)
            {
                debuffList.RemoveAt(i);
            }
        }
    }
}
