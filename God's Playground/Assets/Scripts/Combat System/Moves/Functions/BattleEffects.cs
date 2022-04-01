using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleEffects
{
   
    [SerializeField]
    private List<StatusEffect> debuffs;
    public List<StatusEffect> Debuffs => debuffs;
}
