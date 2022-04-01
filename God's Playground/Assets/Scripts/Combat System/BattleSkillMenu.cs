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

    // Start is called before the first frame update
    void Start()
    {
        selector.onSelect += UpdateSkillDisplay;
        Display();
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
            //Isto ta feito para so 1 player. Se tiver mais membros de equipa 
            //tem de ter um rework
            if (selector.SelectedEntity is EnemyBattleEntityProper)
            {

                if (slot.Move.Config.Mode == SelectorMode.All
                    || slot.Move.Config.Mode == SelectorMode.Adversary)
                {
                    slot.Unlock();
                }
                else
                {
                    slot.Lock();
                }
            }
            else
            {
                if (slot.Move.Config.Mode == SelectorMode.All
                    || slot.Move.Config.Mode == SelectorMode.Team
                    || slot.Move.Config.Mode == SelectorMode.Self)
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


}
