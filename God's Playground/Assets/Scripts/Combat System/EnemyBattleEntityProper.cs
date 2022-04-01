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


    public override void StartTurn()
    {
        base.StartTurn();
        if (isDead) return;
        

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
        ConfigUIComponent<BattleSlider>(hpSliderPREFAB,
            ref hpBattleSlider,
            new Vector3(0,0,0));

        ConfigUIComponent<StatusEffectDisplay>(statusEffectDisplayPREFAB,
            ref statusEffectDisplay,
            new Vector3(0,30,0));
    }

    //Isto era fixe de fazer
    protected void ConfigUIComponent<T>(GameObject prefab,ref T component,
        Vector3 offset) where T: MonoBehaviour, IConfigurable
    {
        GameObject newUI = Instantiate(prefab, transform.position,
            Quaternion.identity, GameObject.Find("HealthBars").transform);

        component = newUI.GetComponent<T>();
        component.Config(entityData);
        component.transform.position = 
                    Camera.main.WorldToScreenPoint(transform.position);

        RectTransform rect = component.GetComponent<RectTransform>();
        rect.ScaleWithTarget(transform, 1.7f);

        Vector3 topPos =
        Camera.main.WorldToScreenPoint(
        GetComponent<Collider>().bounds.center +
        Vector3.zero.y(GetComponent<Collider>().bounds.extents.y + 0.3f));

        rect.position = topPos + offset;
    }
}
