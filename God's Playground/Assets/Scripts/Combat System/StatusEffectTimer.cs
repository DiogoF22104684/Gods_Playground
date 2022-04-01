public class StatusEffectTimer
{
    StatusEffect effect;
    int timer;

    public StatusEffectTimer(StatusEffect effect)
    {
        this.effect = effect;
        this.timer = 0;
    }

    public StatusEffect Effect => effect;
    public int Timer { get => timer; set => timer = value; }
}
