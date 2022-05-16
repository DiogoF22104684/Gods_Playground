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
        target.print();

        currentTargets = target;

        float firstComp = baseValue;

        float firstAndRoll = firstComp * roll;

        attacker.properEntity.PlayAnimation(DefaultAnimations.BasicAttack);

        foreach (BattleEntity be in target)
        {
            BattleStat stat = param1.GetValue(be);
            float stat1 = stat.Stat;

            float totalValue = firstAndRoll;

            float valueToChange = stat1 - totalValue;

            be.properEntity.damageTrigger += () =>
            {
                Debug.Log("KKK");
                BattleStat value =
                       new BattleStat(valueToChange, stat.MaxStat, stat.FlatStat);
                param1.param.SetValue(be, value);
            };
        }

    }
}
