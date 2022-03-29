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


}
