using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;

[System.Serializable]
public class Inventory
{
    [SerializeField]
    List<Equipment> equipment;
    [SerializeField]
    List<BattleMove> skills;

    public List<Equipment> Equipment => equipment;
    public List<BattleMove> Skills => skills;


}

[CreateAssetMenu(menuName = "Scriptables/Equipment")]
public class Equipment : ScriptableObject, IInventoryItem
{
    public Sprite Icon => throw new System.NotImplementedException();

}

