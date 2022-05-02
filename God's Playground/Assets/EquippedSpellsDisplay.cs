using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;

public class EquippedSpellsDisplay : MonoBehaviour
{
    [SerializeField]
    private PlayerTemplate player;

    [SerializeField]
    private List<InventorySlot> slots;

    public System.Action<BattleMove> teste;

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
                slots[i].SetItem(player.Skills[i]);
                slots[i].onClick += ()=> 
                {
                    teste = (BattleMove move) =>
                    {
                        if (player.Skills.Contains(move))
                            player.Skills[player.Skills.IndexOf(move)] = player.Skills[o];

                        player.Skills[o] = move;
                        Display();
                        invetory.Display();
                    };

                    Display();
                    invetory.Display();
                    invetory.eventSkill = teste;
                };
                
            }
            catch
            {
                slots[i].SetItem(null);
            }
        }
    }

}
