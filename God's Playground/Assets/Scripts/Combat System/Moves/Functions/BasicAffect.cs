using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BasicAffect : BattleAffects
{
    [SerializeField]
    private BattlePropertyInfo param1;
    [SerializeField]
    private BattlePropertyInfo param2;
    [SerializeField]
    private BattlePropertyInfo param3;

    [SerializeField]
    private int baseValue;


    private IEnumerable<BattleEntity> currentTargets;


    public override void Function(BattleEntity attacker, IEnumerable<BattleEntity> target, float roll)
    {
        currentTargets = target;

        float stat2 = (param2.param.GetValue(attacker) as BattleStat).Stat;
        


        float firstComp = baseValue + 
            (baseValue * stat2) * roll;


   
        attacker.properEntity.PlayAnimation(DefaultAnimations.BasicAttack);

        foreach (BattleEntity be in target)
        {
            float stat3 = (param3.param.GetValue(be) as BattleStat).Stat;
            BattleStat stat = (param1.param.GetValue(be) as BattleStat);
            float stat1 = stat.Stat;

            float totalValue = firstComp - (firstComp * stat3);

            //Make sure the damage is never 0
            totalValue = totalValue == 0 ? 1 : totalValue;



            float valueToChange = stat1 - totalValue;

            be.properEntity.damageTrigger += ()=> 
            {
                BattleStat value = 
                       new BattleStat(valueToChange, stat.MaxStat, stat.FlatStat);
                param1.param.SetValue(be, value);        
            };

        }

    }
}
