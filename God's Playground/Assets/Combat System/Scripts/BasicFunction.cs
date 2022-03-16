using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

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

    public override void Function(BattleEntity attacker, BattleEntity target)
    {
        float roll = attacker.GetRoll();
         
        attacker.properEntity.PlayAnimation(DefaultAnimations.BasicAttack);
        //target.properEntity.PlayAnimation(DefaultAnimations.DamageTaken);
        return;
        //Debug.Log(baseValue);
        //Debug.Log((float)secondParam.GetValue(attacker));
        //Debug.Log((float)thirdParam.GetValue(target));
         
        float firstComp = baseValue +
            (baseValue * (float)secondParam.GetValue(attacker));
         
        //Debug.Log(firstComp);

        float firstAndRoll = firstComp * roll;

        //Debug.Log(firstAndRoll);

        float totalValue = firstAndRoll - (firstAndRoll *
            (float)thirdParam.GetValue(target));
        
        //Debug.Log(totalValue);

        float newValue = (float)firstParam.GetValue(attacker) - totalValue;
        
        //Debug.Log(newValue);

        firstParam.SetValue(target, newValue);
        Debug.Log(target);
    }
}
