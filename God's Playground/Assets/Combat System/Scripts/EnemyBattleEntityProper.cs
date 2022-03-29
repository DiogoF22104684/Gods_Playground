using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;
using System.Linq;

public class EnemyBattleEntityProper : BattleEntityProper
{

    //Isto é kinda Dumb
    private IEnumerable<BattleEntity> players;

    [SerializeField]
    private ActionPointProper actionPoints;
    public ActionPointProper ActionPoints => actionPoints;



    //// Start is called before the first frame update
    //void Start()
    //{
    //    //Instance slider aqui vv

    //    //just for testing 
    //    //entityData = new BattleEntity(30, this);
       
    //}

    public override void StartTurn()
    {
        BattleMove mo = (entityData.template as EnemiesTemplate).ResolveAction();
        mo.Function(entityData, players.First(x => x.Hp > 0), Random.Range(1,7));
        entityData.hadTurn = true;
        


        //Animate things probably
        
       // Invoke("EndTurn", 1);
    }

    public override void EndTurn()
    {
        Invoke("OnEndTurn", 1);
    }

    public void SetPlayers(IEnumerable<BattleEntity> players)
    {
        this.players = players;
    }

    public override void AttackTriggerAnimation(DefaultAnimations animType)
    {
        attackTrigger?.Invoke(animType, players.First(x => x.Hp > 0));
    }
}
