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



    public EntityTemplate template { get; }

    public BattleEntity(BattleEntityProper proper,  EntityTemplate template)
    {
        properEntity = proper;
        this.hp = template.HP;
        atk = (template.Str * 2) /100f;
        def = (template.Def * 2) / 100f;       
        dex = template.Dex;
        this.template = template;
    }

    public BattleEntityProper properEntity { get; }


    public override string ToString()
    {
        return $"Hp: {hp} \nAtk: {atk} \nDef:{def}";
    }

}
