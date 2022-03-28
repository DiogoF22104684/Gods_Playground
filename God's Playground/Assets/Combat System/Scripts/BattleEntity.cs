using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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



    public EntityTemplate template { get; }

    public BattleEntity(float hp, BattleEntityProper proper, EntityTemplate template = null)
    {
        properEntity = proper;
        this.hp = hp;
        atk = 0.4f;
        def = 0.3f;
        this.template = template;
        dex = template.Dex;
    }

    public BattleEntityProper properEntity { get; }


    public override string ToString()
    {
        return $"Hp: {hp} \nAtk: {atk} \nDef:{def}";
    }

}
