using UnityEngine;
using System.Collections.Generic;
using CombatSystem;

[CreateAssetMenu(menuName = "Scriptables/Equipment")]
public class Equipment : ScriptableObject, IInventoryItem
{
    [SerializeField]
    private Sprite icon;
    public Sprite Icon => icon;
    
    [SerializeField]
    private EquipmentType equimentType;
    public EquipmentType EquimentType => equimentType;

    [SerializeField]
    private List<StatChanger> equipmentModifiers;
    public List<StatChanger> EquipmentModifiers  => equipmentModifiers;


    [System.Serializable]
    public struct StatChanger
    {
        [SerializeField]
        private BattlePropertyInfo stat;
        [SerializeField]
        private RangedInt value;

        public BattlePropertyInfo Stat => stat;
        public RangedInt Value  => value;
    }
}
