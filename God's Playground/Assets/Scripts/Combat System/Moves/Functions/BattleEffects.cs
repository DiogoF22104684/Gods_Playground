using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleEffects
{
   
    [SerializeField]
    private List<StatusEffect> statusEffects;
    public List<StatusEffect> StatusEffects => statusEffects;
}
