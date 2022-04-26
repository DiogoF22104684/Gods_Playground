using System;

public class TurnTimer<T>: IEquatable<TurnTimer<T>>
{
    T effect;
    int timer;

    public TurnTimer(T effect)
    {
        this.effect = effect;
        this.timer = 0;
    }

    public T Effect => effect;
    public int Timer { get => timer; set => timer = value; }


    public static implicit operator TurnTimer<T>(T self)
    {
        return new TurnTimer<T>(self);
    }

    public bool Equals(TurnTimer<T> other)
    {
        return other.effect.Equals(effect);
    }

    public override string ToString()
    {
        return effect.ToString() + " with time: " + timer;
    }
}