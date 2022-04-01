using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleEntityProper : BattleEntityProper
{

    //private PlayerBattleMenu battleMenu;
    [SerializeField]
    private GameObject battleMenu;


    // Start is called before the first frame update
    //void Start()
    //{
    //    //Instance slider aqui vv

    //    //just for testing         
    //}

    public override void AttackTriggerAnimation(DefaultAnimations animType)
    {
        attackTrigger?.Invoke(animType, null);
    }

    public override void StartTurn()
    {
        base.StartTurn();
        if (isDead) return;
        entityData.hadTurn = true;
        battleMenu.SetActive(true);
        //Invoke("EndTurn", 1);
    }

    public override void EndTurn()
    {
       
        battleMenu.SetActive(false);
        Invoke("OnEndTurn", 2);
        //battleMenu.Deactivate();
    }

    protected override void ConfingBars()
    {
        //Dumb and bad and dumb(just for now)
        hpBattleSlider = hpSliderPREFAB.GetComponent<BattleSlider>();

        hpBattleSlider.Config(entityData);
        statusEffectDisplay = statusEffectDisplayPREFAB.GetComponent<StatusEffectDisplay>();
        statusEffectDisplay.Config(entityData);

    }
}
