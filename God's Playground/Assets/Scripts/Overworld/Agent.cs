using UnityEngine;

public abstract class Agent: MonoBehaviour
{
    protected Mover mover;

    public Mover Mover => mover; 
}