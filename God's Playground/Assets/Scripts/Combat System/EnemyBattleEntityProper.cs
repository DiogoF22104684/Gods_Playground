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

    public override bool StartTurn(CombatState state)
    {
        if (!base.StartTurn(state)) return false;

        currentTargets = new List<BattleEntity>();

        BattleMove mo = (entityData.Template as EnemiesTemplate).ResolveAction(entityData, state);

        IniciateMove(mo, state);

        return true;
    }

    public override void EndTurn()
    {
        Invoke("OnEndTurn", 1);
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
