using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStat
{
    private float stat;
    private float maxStat;
    private float flatStat;

    public BattleStat(float stat, float maxStat, float flatStat)
    {
        this.stat = stat;
        this.maxStat = maxStat;
        this.flatStat = flatStat;
    }

    public float Stat { get => stat; set => stat = value; }
    public float MaxStat { get => maxStat; private set => maxStat = value; }
    public float FlatStat { get => flatStat; private set => flatStat = value; }


    public static implicit operator float(BattleStat self)
    {
        return self.Stat;
    }
 
}


