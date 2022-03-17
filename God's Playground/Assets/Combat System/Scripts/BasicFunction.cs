using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;


//This needs a rework. It's mostly broken
[CreateAssetMenu(menuName = "Scriptables/Moves/BasicFunction")]
public class BasicFunction : MoveFunction
{
    public PropertyInfo firstParam { get; set; }
    public PropertyInfo secondParam { get; set; }
    public PropertyInfo thirdParam { get; set; }

    //There's probably a better way to serialize this but for now it works
    public int selected1;
    public int selected2;
    public int selected3;

    [SerializeField]
    private int baseValue;


    private BattleEntity currentTargets;
    private float valueToChange;


    public override void Function(BattleEntity attacker, BattleEntity target)
    {
        currentTargets = target;
        float roll = attacker.GetRoll();
         
        float firstComp = baseValue +
            (baseValue * (float)secondParam.GetValue(attacker));

        float firstAndRoll = firstComp * roll;

        float totalValue = firstAndRoll - (firstAndRoll *
            (float)thirdParam.GetValue(target));
        
        valueToChange = (float)firstParam.GetValue(target) - totalValue;

        attacker.properEntity.PlayAnimation(DefaultAnimations.BasicAttack);
        target.properEntity.damageTrigger += Boop;
        
    }

    private void Boop()
    {
        firstParam.SetValue(currentTargets, valueToChange);
    }


}
