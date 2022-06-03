using CombatSystem;
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
    private RangedInt eva;
    public RangedInt Eva => eva;

    [SerializeField]
    private RangedInt str;
    public RangedInt Str => str;

    [SerializeField]
    private RangedInt def;
    public RangedInt Def => def;


    [SerializeField]
    private RangedInt inte;
    public RangedInt Int => inte;


    [SerializeField]
    private RangedInt intu;
    public RangedInt Intu => intu;
    //str
    //def
    //dex
    //eva
    //int
    //intu

    //prefab
    [SerializeField]
    private List<BattleMove> skills;
    public List<BattleMove> Skills => skills;

}

