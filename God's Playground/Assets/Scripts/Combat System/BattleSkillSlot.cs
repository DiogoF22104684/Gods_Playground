using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSkillSlot : MonoBehaviour
{
    private BattleSkillMenu skillMenu;
    public bool isLocked;
    private Button button;
    private CombatSystem.BattleMove move;
    public CombatSystem.BattleMove Move => move;


    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ActivateAttack);
    }

    public void ConfigSkill(CombatSystem.BattleMove move, BattleSkillMenu battleSkillMenu)
    {
        this.move = move;
        skillMenu = battleSkillMenu;
        GetComponent<Image>().sprite = move.Icon;
    }    

    private void ActivateAttack()
    {
        if (!isLocked)
        {
            skillMenu.InvokeMove(move);
        }
    }

    public void Lock()
    {
        isLocked = true;
        button.interactable = false;
    }

    public void Unlock()
    {
        isLocked = false;
        button.interactable = true;
    }
}
