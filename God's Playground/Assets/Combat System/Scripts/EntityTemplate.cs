using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class EntityTemplate: ScriptableObject
{
    [SerializeField]
    private int hp;
    public int HP => hp;

    [SerializeField]
    private int mp;
    public int Mp => mp;


    [SerializeField]
    private int dex;
    public int Dex => dex;

    //str
    //def
    //dex
    //eva
    //int
    //intu

    //prefab

}

