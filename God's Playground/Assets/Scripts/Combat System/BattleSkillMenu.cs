using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;

public class BattleSkillMenu : MonoBehaviour
{
    [SerializeField]
    private BattleSkillSlot[] skillSlot;
    [SerializeField]
    private PlayerTemplate template;
    [SerializeField]
    private EntitySelector selector;


    private void Awake()
    {
        selector.onSelect += UpdateSkillDisplay;
    }
   
    // Start is called before the first frame update
    void Start()
    {
        Display();
        UpdateSkillDisplay();
    }

    private void Display()
    {
        for (int i = 0; i < template.Skills.Count; i++)
        {
            skillSlot[i].ConfigSkill(template.Skills[i]);
        }
    }

    private void UpdateSkillDisplay()
    {

        foreach (BattleSkillSlot slot in skillSlot)
        {
            if (slot.Move.IsUsable(EntitySelector.PlayerEntity,EntitySelector.SelectedEntity))
            {
                slot.Unlock();
            }
            else
            {
                slot.Lock();
            }

            
        }
    }


}
