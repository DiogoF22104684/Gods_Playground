using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BasicFlat : BattleAffects
{
    [SerializeField]
    private BattlePropertyInfo param1;
  
    [SerializeField]
    private int baseValue;

    private IEnumerable<BattleEntity> currentTargets;


    public override void Function(BattleEntity attacker, IEnumerable<BattleEntity> target, float roll)
    {
        currentTargets = target;

        float firstComp = baseValue;

        float firstAndRoll = firstComp * roll;



        attacker.properEntity.PlayAnimation(DefaultAnimations.BasicAttack);

        foreach (BattleEntity be in target)
        {
            float totalValue = firstAndRoll;

            float valueToChange = (float)param1.param.GetValue(be) - totalValue;

            be.properEntity.damageTrigger += () =>
            {
                param1.param.SetValue(be, valueToChange);
            };
        }

    }
}
