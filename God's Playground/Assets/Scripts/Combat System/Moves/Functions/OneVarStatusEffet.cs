using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/StautsEffect/OneVar")]
public class OneVarStatusEffet : StatusEffect
{

    [SerializeField]
    private BattlePropertyInfo param;
    [SerializeField]
    private float value;

    public override void ResolveDebuff(BattleEntity entity)
    {
        
        BattleStat var = param.GetValue(entity);
        BattleStat newValue = 
                new BattleStat(var.Stat - value,var.MaxStat,var.FlatStat);

        param.param.SetValue(entity, newValue);
    }
}