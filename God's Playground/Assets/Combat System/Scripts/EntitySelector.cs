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
        Vector3 center = SelectedEntity.transform.position;

        Vector3 top = SelectedEntity.transform.position.y(
            SelectedEntity.transform.position.y +
            SelectedEntity.gameObject.GetComponent<Collider>().bounds.extents.y);


        Vector3 screenCenter = Camera.main.WorldToScreenPoint(center);
        Vector3 screenTop = Camera.main.WorldToScreenPoint(top);

        float radius = (screenTop.y - screenCenter.y) / 2.5f;

        iconSelector.GetComponent<RectTransform>().sizeDelta = new Vector2(radius,radius);
   

        iconSelector.transform.position =  
            Camera.main.WorldToScreenPoint(center);
    }
}
