using System.Collections.Generic;
using UnityEngine;

public class EquippedEquipmentDisplay : MonoBehaviour
{
    [SerializeField]
    private PlayerTemplate player;

    [SerializeField]
    private List<InventorySlot> slots;

    public System.Action<Equipment> teste;

    [SerializeField]
    private InventoryDisplay invetory;

    public void Start()
    {
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
                slots[i].SetItem(player.EquipedItems[o]);
                slots[i].onClick += () =>
                {
                    teste = (Equipment equipment) =>
                    {
                        //if (player.Skills.Contains(move))
                        //    player.Skills[player.Skills.IndexOf(move)] = player.Skills[o];

                        //player.Skills[o] = move;

                        if (player.EquipedItems.CanEquip(o,equipment))
                        {
                            player.EquipedItems[o]  = equipment;
                        }
                      
                        Debug.Log("Boop");


                        Display();
                        invetory.Display();
                    };

                    Display();
                    invetory.Display();
                    invetory.eventEquip = teste;
                };

            }
            catch
            {
                slots[i].SetItem(null);
            }
        }
    }

}
