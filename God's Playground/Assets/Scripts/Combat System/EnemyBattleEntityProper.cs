using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;
using System.Linq;


public class EnemyBattleEntityProper : BattleEntityProper
{
    [SerializeField]
    private ActionPointProper actionPoints;
    public ActionPointProper ActionPoints => actionPoints;

    IEnumerable<BattleEntity> entitiesSelected;

    public override bool StartTurn()
    {
        if (!base.StartTurn()) return false;

        entitiesSelected = new List<BattleEntity>();

        BattleMove mo = (entityData.template as EnemiesTemplate).ResolveAction();

        entitiesSelected = EntitySelector.SelectEntity(entityData,
            new List<BattleEntity> { EntitySelector.PlayerEntity.entityData }, 
            mo.Config.Mode, mo.Config.Type);

        mo.Function(entityData, entitiesSelected, Random.Range(1, 7) / 6f);

        return true;
    }

    public override void EndTurn()
    {
        Invoke("OnEndTurn", 1);
    }



    public override void AttackTriggerAnimation(DefaultAnimations animType)
    {     
        attackTrigger?.Invoke(animType, entitiesSelected);
    }

    protected override void ConfingBars()
    {
        ConfigUIComponent<BattleSlider>(hpSliderPREFAB,
            ref hpBattleSlider,
            new Vector3(0,0,0));
        hpBattleSlider.initStats(entityData.Hp);

        ConfigUIComponent<BattleSlider>(mpSliderPREFAB,
          ref mpBattleSlider,
          new Vector3(0, -20, 0));
        mpBattleSlider.initStats(entityData.Mp);


        ConfigUIComponent<StatusEffectDisplay>(statusEffectDisplayPREFAB,
            ref statusEffectDisplay,
            new Vector3(0,60,0));
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
