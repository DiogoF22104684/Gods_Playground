using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSkillSlot : MonoBehaviour
{
    [SerializeField]
    private BattleManager manager;
    public bool isLocked;
    private Button button;
    private CombatSystem.BattleMove move;
    public CombatSystem.BattleMove Move => move;


    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ActivateAttack);
    }

    public void ConfigSkill(CombatSystem.BattleMove move)
    {
        this.move = move;

        GetComponent<Image>().sprite = move.Icon;
    }    

    private void ActivateAttack()
    {
        if (!isLocked)
        {
            manager.ResolvePlayerAttack(move);
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
