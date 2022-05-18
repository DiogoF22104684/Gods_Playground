using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using CombatSystem;
using static CombatSystem.SelectorMode;
using static CombatSystem.SelectorType;
public class EntitySelector : MonoBehaviour
{
    public bool PlayerHasAttacked { get; internal set; }

    [SerializeField]
    private GameObject iconSelector;

    [SerializeField]
    private BattleInfoPanel infoPanel;
    public static BattleEntityProper SelectedEntity { get; private set; }

    public List<BattleEntityProper> SelectedEntities { get; private set; }

    public System.Action onSelect;

    private static List<BattleEntityProper> enemiesEntity;
    private static BattleEntityProper playerEntity;
    public static BattleEntityProper PlayerEntity => playerEntity;

   
    // Update is called once per frame
    void Update()
    {

        //Select target entity
        if (Input.GetMouseButtonDown(0))
        {
            if (!PlayerHasAttacked)
            {
                SelectEntity(x =>
                {
                    SelectedEntity = x;
                    SpawnIcon();
                    onSelect?.Invoke();
                });
            }
        }

        //Select info entity
        if (Input.GetMouseButton(1))
        {
            SelectEntity(x => {
                
                infoPanel.transform.position = 
                    Camera.main.WorldToScreenPoint(x.transform.position);
                infoPanel.gameObject.SetActive(true);
                infoPanel.Display(x);
            });
        }
        else
        {
            infoPanel.gameObject.SetActive(false);
        }
    }


    private void SelectEntity(Action<BattleEntityProper> onSelect)
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
            hitInfo: out hit))
        {
            BattleEntityProper selectedEntity =
                hit.collider.gameObject.GetComponent<BattleEntityProper>();

            if (selectedEntity != null && selectedEntity != SelectedEntity)
            {
                onSelect?.Invoke(selectedEntity);
            }
        }
    }

    private void SpawnIcon()
    {
        if (SelectedEntity == null) return;

        iconSelector.GetComponent<RectTransform>().ScaleWithTarget(
            SelectedEntity.transform, 0.5f);

        iconSelector.transform.position =
           Camera.main.WorldToScreenPoint(SelectedEntity.transform.position);
    }

    public IEnumerable<BattleEntityProper> GetTargets(BattleEntity entity, SelectorType type)
    {
        List<BattleEntityProper> returnlist = new List<BattleEntityProper> { };
        switch(type)
        {
            case Solo:
                returnlist = new List<BattleEntityProper> { SelectedEntity };
                break;
            case Area:
                returnlist = GetAllAdjacent().ToList();
                break;
            case SelectorType.All:
                returnlist = enemiesEntity;
                break;


        }
        SelectedEntities = returnlist;
        return returnlist;
    }

    private IEnumerable<BattleEntityProper> GetAllAdjacent()
    {
        List<BattleEntityProper> returnlist = new List<BattleEntityProper> { };
        int index = enemiesEntity.IndexOf(SelectedEntity);
        
        returnlist.Add(SelectedEntity);

        if (index - 1 >= 0)
        {
            returnlist.Add(enemiesEntity[index - 1]);
        }
        if (index + 1 < enemiesEntity.Count)
        {
            returnlist.Add(enemiesEntity[index + 1]);
        }

        

        return returnlist;
    }
    
    internal void Config(BattleEntityProper playerProper, 
        List<BattleEntityProper> enemies)
    {

        playerEntity = playerProper;
        enemiesEntity = enemies;

        if (SelectedEntity == null || SelectedEntity.entityData.Hp <= 0)
        {
            try 
            {
                SelectedEntity = enemiesEntity.First(x => x.entityData.Hp > 0);
            }
            catch
            {

            }
        }
        
        SpawnIcon();
        //onSelect?.Invoke();
    }

    /// <summary>
    /// Get team given an entity 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="ofTeam"></param>
    /// <returns></returns>
    private static IEnumerable<BattleEntity> GetTeam(BattleEntity entity, bool ofTeam = false)
    {
        
        bool isPlayer = !(entity.IsSameTeam(playerEntity.entityData) ^ ofTeam);
        return isPlayer ?
            new List<BattleEntity> { playerEntity.entityData }
            : 
            enemiesEntity.Select(x => x.entityData)
            ;
    }

    /// <summary>
    /// Get entity given an entity 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="ofTeam"></param>
    /// <returns></returns>
    private static BattleEntity GetSelected(BattleEntity entity, bool ofTeam = false)
    {
        bool isPlayer = !(entity.IsSameTeam(playerEntity.entityData) ^ ofTeam);
        return isPlayer ? playerEntity.entityData: SelectedEntity.entityData;
    }

    public static IEnumerable<BattleEntity> SelectEntity(
        BattleEntity attacker, IEnumerable<BattleEntity> target, 
        SelectorMode team, SelectorType type)
    {
        List<BattleEntity> returnlist = new List<BattleEntity> { };

        //Bad dumb and ugly
        switch (type)
        {
            case Solo:
                switch (team)
                {
                    case SelectorMode.All:
                        returnlist.Add(GetSelected(attacker));
                        return returnlist;
                    case Self:
                        returnlist.Add(attacker);
                        return returnlist;
                    case Adversary:
                        returnlist.Add(GetSelected(attacker));
                        return returnlist;
                    case Team:
                        returnlist.Add(GetSelected(attacker));
                        return returnlist;
                }
                break;           
            case SelectorType.All:
                switch (team)
                {
                    case SelectorMode.All:
                        returnlist = enemiesEntity.Select(x =>x.entityData).ToList();
                        returnlist.Add(playerEntity.entityData);
                        return returnlist;
                    case Self:
                        returnlist.Add(attacker);
                        return returnlist;
                    case Adversary:                        
                        return GetTeam(attacker);
                    case Team:
                        return GetTeam(attacker, true);
                }
                break;
        }

        Debug.Log(returnlist.print());

        return returnlist;
    }
    
}
