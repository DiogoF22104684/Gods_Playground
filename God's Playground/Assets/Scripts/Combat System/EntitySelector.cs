using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySelector : MonoBehaviour
{

    [SerializeField]
    private GameObject iconSelector;
    public BattleEntityProper SelectedEntity { get; private set; }

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



}
