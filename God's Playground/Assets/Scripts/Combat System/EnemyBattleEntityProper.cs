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
        base.StartTurn();
        BattleMove mo = (entityData.template as EnemiesTemplate).ResolveAction();
        mo.Function(entityData, players.Where(x => x.Hp > 0), Random.Range(1,7) / 6f);
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

    protected override void ConfingBars()
    {
        //Maybe try to find the canvas component and add to that gameObject instead
        GameObject slider = Instantiate(hpSliderPREFAB, transform.position,
            Quaternion.identity, GameObject.Find("HealthBars").transform);

        hpBattleSlider = slider.GetComponent<BattleSlider>();
        hpBattleSlider.Config(entityData.Hp);
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        RectTransform sliderRect = slider.GetComponent<RectTransform>();

        sliderRect.ScaleWithTarget(transform, 1.7f);

        Vector3 topPos =
            Camera.main.WorldToScreenPoint(
                GetComponent<Collider>().bounds.center +
                Vector3.zero.y(GetComponent<Collider>().bounds.extents.y + 0.3f));


        sliderRect.position = topPos;

    }
}
