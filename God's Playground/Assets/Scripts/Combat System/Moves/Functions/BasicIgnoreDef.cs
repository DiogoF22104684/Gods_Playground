using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BasicIgnoreDef : BattleAffects
{
    [SerializeField]
    private BattlePropertyInfo param1;
    [SerializeField]
    private BattlePropertyInfo param2;

    [SerializeField]
    private int baseValue;


    private IEnumerable<BattleEntity> currentTargets;
    private float valueToChange;


    public override void Function(BattleEntity attacker, IEnumerable<BattleEntity> target, float roll)
    {
        currentTargets = target;

        float firstComp = baseValue +
            (baseValue * (float)param2.param.GetValue(attacker));

        float firstAndRoll = firstComp * roll;



        attacker.properEntity.PlayAnimation(DefaultAnimations.BasicAttack);

        foreach (BattleEntity be in target)
        {
            float totalValue = firstAndRoll;

            valueToChange = (float)param1.param.GetValue(be) - totalValue;

            be.properEntity.damageTrigger += Resolve;
        }

    }


    private void Resolve(BattleEntity target)
    {
        param1.param.SetValue(target, valueToChange);
    }


}
