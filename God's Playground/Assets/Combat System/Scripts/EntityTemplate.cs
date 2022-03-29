using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class EntityTemplate: ScriptableObject
{
    [SerializeField]
    private RangedInt hp;
    public RangedInt HP => hp;

    [SerializeField]
    private RangedInt mp;
    public RangedInt Mp => mp;


    [SerializeField]
    private RangedInt dex;
    public RangedInt Dex => dex;

    [SerializeField]
    private RangedInt str;
    public RangedInt Str => str;


    //str
    //def
    //dex
    //eva
    //int
    //intu

    //prefab

}

