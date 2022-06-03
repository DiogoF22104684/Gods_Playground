using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;
using System;

public class BattleSkillMenu : MonoBehaviour
{
    [SerializeField]
    private BattleSkillSlot[] skillSlot;

    [SerializeField]
    private PlayerTemplate template;

    [SerializeField]
    private SelectorComponent selector;

    [SerializeField]
    private BattleManager manager;


    public Action<BattleMove,CombatState> onSelectedMove { get; set; }

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
            skillSlot[i].ConfigSkill(template.Skills[i], this);
            
        }
    }

    private void UpdateSkillDisplay()
    {
        foreach (BattleSkillSlot slot in skillSlot)
        {
            bool i = manager.CombatState.CanUseMove(slot.Move);

            if (slot.Move.IsUsable(manager.CombatState.Turn, 
                manager.CombatState.Selector.SelectedEntity))
            {
                slot.Unlock();
            }
            else
            {
                slot.Lock();
            }           
        }
    }

    internal void InvokeMove(BattleMove move)
    {
        onSelectedMove?.Invoke(move, manager.CombatState);
    }
}
