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

        float firstComp = baseValue +
            (baseValue * (float)param2.param.GetValue(attacker));

        float firstAndRoll = firstComp * roll;


        attacker.properEntity.PlayAnimation(DefaultAnimations.BasicAttack);

        foreach (BattleEntity be in target)
        {
            float totalValue = firstAndRoll - (firstAndRoll *
                       (float)param3.param.GetValue(be));

            //Make sure the damage is never 0
            totalValue = totalValue == 0 ? 1 : totalValue;

            float valueToChange = (float)param1.param.GetValue(be) - totalValue;

            be.properEntity.damageTrigger += ()=> 
            {
                param1.param.SetValue(be, valueToChange);        
            };

        }

    }
}
