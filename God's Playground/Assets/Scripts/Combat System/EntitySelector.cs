using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class EntitySelector : MonoBehaviour
{

    [SerializeField]
    private GameObject iconSelector;
    public BattleEntityProper SelectedEntity { get; private set; }

    public List<BattleEntityProper> SelectedEntities { get; private set; }


    private List<BattleEntityProper> enemiesEntity;
    private BattleEntityProper playerEntity;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
          
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                hitInfo: out hit))
            {
                BattleEntityProper selectedEntity =
                    hit.collider.gameObject.GetComponent<BattleEntityProper>();
                
                if (selectedEntity != null && selectedEntity != SelectedEntity)
                {
                    this.SelectedEntity = selectedEntity;
                    SpawnIcon();      
                }
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
    }
}
