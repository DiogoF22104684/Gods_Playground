using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/StautsEffect/OneVar")]
public class OneVarStatusEffet : StatusEffect
{

    [SerializeField]
    private BattlePropertyInfo param;
    [SerializeField]
    private float value;

    public override void EndStatusEffect(BattleEntity entity)
    {
        if(EffectType == StatusEffectType.TemporaryEffect)
        {
            BattleStat var = param.GetValue(entity);
            BattleStat newValue =
                    new BattleStat(var.Stat + value, var.MaxStat, var.FlatStat);

            param.param.SetValue(entity, newValue);
        }
    }

    public override void ResolveStatusEffect(BattleEntity entity, int timer)
    {
        
        if (EffectType == StatusEffectType.TemporaryEffect && timer > 0)
        {
            EndStatusEffect(entity);
        }

        BattleStat var = param.GetValue(entity);

        Debug.Log($"Value: {value} Stat: {var.Stat}");

        BattleStat newValue = 
                new BattleStat(var.Stat - value,var.MaxStat,var.FlatStat);

        param.param.SetValue(entity, newValue);
    }
}