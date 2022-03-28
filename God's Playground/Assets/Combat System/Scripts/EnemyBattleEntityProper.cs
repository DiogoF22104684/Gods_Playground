using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleEntityProper : BattleEntityProper
{


    [SerializeField]
    private ActionPointProper actionPoints;
    public ActionPointProper ActionPoints => actionPoints;

    // Start is called before the first frame update
    void Start()
    {
        //Instance slider aqui vv

        //just for testing 
        //entityData = new BattleEntity(30, this);
        if (hpSlider != null)
            hpSlider.Config(entityData.Hp);
    }

    public override void StartTurn()
    {
        //(entityData.template as EnemiesTemplate).ResolveAction();

        entityData.hadTurn = true;
        Debug.Log("Beep");

        //Animate things probably
        
        Invoke("EndTurn", 1);
    }

    public override void EndTurn()
    {
        Invoke("OnEndTurn", 1);
    }

    

}
