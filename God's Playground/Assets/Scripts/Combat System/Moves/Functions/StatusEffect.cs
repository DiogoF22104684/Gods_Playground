using UnityEngine;

public abstract class StatusEffect: ScriptableObject
{
    [SerializeField]
    private Texture2D icon;

    [SerializeField]
    private int coolDown;
    public int CoolDown => coolDown;

    public Sprite Icon => icon.ToSprite();

    public StatusEffectType EffectType => effectType;
    
    [SerializeField]
    private StatusEffectType effectType;

    public abstract void ResolveStatusEffect(BattleEntity entity, int timer);
    public abstract void EndStatusEffect(BattleEntity entity);
}


public enum StatusEffectType
{
    TemporaryEffect,
    PermanentEffect
}
