using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Function to be used when using a specific attack. This iteration uses 
/// the stat values of the attacker and the targets.
/// </summary>
[System.Serializable]
public class BasicAffect : BattleAffects
{
    
    //First parameter of the move.
    [SerializeField]
    private BattlePropertyInfo param1;
    //Second parameter of the move. 
    [SerializeField]
    private BattlePropertyInfo param2;
    //Third parameter of the move. 
    [SerializeField]
    private BattlePropertyInfo param3;

    //Base value of the move.
    [SerializeField]
    private int baseValue;

    //Targets of the move.
    private IEnumerable<BattleEntity> currentTargets;

    /// <summary>
    /// Function to be used when using a specific attack. This iteration uses 
    /// the stat values of the attacker and the targets.
    /// </summary>
    /// <param name="attacker">Entity using the move.</param>
    /// <param name="target">Entities beeing affected by the move.</param>
    /// <param name="roll">Roll value of the dice and quick time events.</param>
    public override void Function(BattleEntity attacker, IEnumerable<BattleEntity> target, float roll)
    {
        //targets of the move
        currentTargets = target;

        //stat being used by the attacker 
        float stat2 = (param2.param.GetValue(attacker) as BattleStat).Stat;
        
        float firstComp =(baseValue + 
            (baseValue * stat2)) * roll;

        //Play animation / Needs to be better
        attacker.properEntity.PlayAnimation(DefaultAnimations.BasicAttack);

        //Apply stat being used by the targets
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
