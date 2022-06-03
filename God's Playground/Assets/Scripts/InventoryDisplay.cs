using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using CombatSystem;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField]
    private PlayerTemplate player;

    private enum Type { Items, Spells }
    [SerializeField]
    private Type type;

    List<IInventoryItem> items;
    [SerializeField]
    private List<InventorySlot> slots;

    public Action<BattleMove> eventSkill;
    public Action<Equipment> eventEquip;

    // Start is called before the first frame update
    void Start()
    {
        items = 
            type == Type.Items ? 
            player.Inventory.Equipment.Select(x => x as IInventoryItem).ToList()
            : player.Inventory.Skills.Select(x => x as IInventoryItem).ToList();
        Display();
    }

    public void Display()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            int o = i;
            try
            {
                slots[i].onClick = null;
                slots[i].SetItem(items[i]);

                slots[i].onClick += () =>
                {
                    if (type == Type.Spells)
                    {
                        eventSkill?.Invoke(player.Inventory.Skills[o]);
                    }
                    else 
                    {
                        eventEquip?.Invoke(player.Inventory.Equipment[o]);
                    }
                };
            }
            catch
            {
                slots[i].SetItem(null);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
