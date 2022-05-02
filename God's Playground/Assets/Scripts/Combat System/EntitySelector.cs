using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class EntitySelector : MonoBehaviour
{
    public bool PlayerHasAttacked { get; internal set; }

    [SerializeField]
    private GameObject iconSelector;

    [SerializeField]
    private BattleInfoPanel infoPanel;
    public BattleEntityProper SelectedEntity { get; private set; }

    public List<BattleEntityProper> SelectedEntities { get; private set; }

    public System.Action onSelect;

    private List<BattleEntityProper> enemiesEntity;
    private BattleEntityProper playerEntity;
    public BattleEntityProper PlayerEntity => playerEntity;

   
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
                    this.SelectedEntity = x;
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
       

        iconSelector.GetComponent<RectTransform>().ScaleWithTarget(
            SelectedEntity.transform, 0.5f);

        //I use GetComponent<Collider> To much
        iconSelector.transform.position =
           Camera.main.WorldToScreenPoint( SelectedEntity.transform.position
               /*SelectedEntity.GetComponent<Collider>().bounds.center*/);
    }


    public IEnumerable<BattleEntityProper> GetTargets(CombatSystem.SelectorType type)
    {


        List<BattleEntityProper> returnlist = new List<BattleEntityProper> { };
        switch(type)
        {
            case CombatSystem.SelectorType.Solo:
                returnlist = new List<BattleEntityProper> { SelectedEntity };
                break;
            case CombatSystem.SelectorType.Area:
                returnlist = GetAllAdjacent().ToList();
                break;
            case CombatSystem.SelectorType.All:
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
        
        if (index - 1 >= 0)
        {
            returnlist.Add(enemiesEntity[index - 1]);
        }
        if (index + 1 < enemiesEntity.Count)
        {
            returnlist.Add(enemiesEntity[index + 1]);
        }

        returnlist.Add(SelectedEntity);

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
                this.SelectedEntity = enemiesEntity.First(x => x.entityData.Hp > 0);
            }
            catch
            {

            }
        }
        
        SpawnIcon();
        //onSelect?.Invoke();
    }
}
