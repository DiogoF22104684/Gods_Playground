using UnityEngine;

public abstract class StatusEffect: ScriptableObject
{
    [SerializeField]
    private Texture2D icon;

    [SerializeField]
    private int coolDown;
    public int CoolDown => coolDown;
    public int timePassed { get; set; }
    public Sprite Icon => icon.ToSprite();

    public abstract void ResolveDebuff(BattleEntity entity);
}
