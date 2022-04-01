using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleDebuff
{
   
    [SerializeField]
    private List<DebuffType> debuffs;
    public List<DebuffType> Debuffs => debuffs;



    public static Debuff GetDebuff(DebuffType type)
    {

        Debuff returnDebuff = null;
        switch (type)
        {
            case DebuffType.Poison:
                returnDebuff = new PoisionDebuff();
                break;
        }

        return returnDebuff;
    }

}

public abstract class Debuff
{
    public abstract int CoolDown { get; }
    public int timePasse { get; set; }
    public abstract void ResolveDebuff(BattleEntity entity);
}

public class PoisionDebuff : Debuff
{
    public override int CoolDown => 2;

   

    public override void ResolveDebuff(BattleEntity entity)
    {
        entity.Hp -= 1;
    }
}

public enum DebuffType
{
    Poison,
    Stun
}