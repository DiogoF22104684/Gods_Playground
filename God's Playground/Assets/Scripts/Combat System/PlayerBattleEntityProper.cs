using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleEntityProper : BattleEntityProper
{

    //private PlayerBattleMenu battleMenu;
    [SerializeField]
    private BattleSkillMenu battleMenu;   
    
    protected override void Start()
    {
        base.Start();
        battleMenu.onSelectedMove += IniciateMove;  
    }

    public override bool StartTurn(CombatState state)
    {
        if (!base.StartTurn(state)) return false;

        
            
        battleMenu.gameObject.SetActive(true);
        
        return true;
        //Invoke("EndTurn", 1);
    }

    public override void EndTurn()
    {
        battleMenu.gameObject.SetActive(false);
        Invoke("OnEndTurn", 2);
        //battleMenu.Deactivate();
    }

    protected override void ConfingBars()
    {
        //Dumb and bad and dumb(just for now)
        hpBattleSlider = hpSliderPREFAB.GetComponent<BattleSlider>();
        mpBattleSlider = mpSliderPREFAB.GetComponent<BattleSlider>();

        hpBattleSlider.Config(entityData);
        mpBattleSlider.Config(entityData);
        statusEffectDisplay = statusEffectDisplayPREFAB.GetComponent<StatusEffectDisplay>();
        statusEffectDisplay.Config(entityData);
        hpBattleSlider.initStats(entityData.Hp);
        mpBattleSlider.initStats(entityData.Mp);
    }
}
