using System;
using UnityEngine;

public class SelectorComponent : MonoBehaviour
{

    [SerializeField]
    private GameObject iconSelector;

    [SerializeField]
    private BattleInfoPanel infoPanel;

    [SerializeField]
    private BattleManager manager;
    private EntitySelector selector;


    public System.Action onSelect;

    private void Start()
    {
        selector = manager.CombatState.Selector;
    }

    // Update is called once per frame
    void Update()
    {

        //Select target entity
        if (Input.GetMouseButtonDown(0))
        {
            if (!selector.PlayerHasAttacked)
            {
                SelectEntity(x =>
                {
                    selector.SelectedEntity = x.entityData;
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

            if (selectedEntity != null && selectedEntity != selector.SelectedEntity?.ProperEntity)
            {
                onSelect?.Invoke(selectedEntity);
            }
        }
    }

    private void SpawnIcon()
    {
        if (selector.SelectedEntity == null) return;

        iconSelector.GetComponent<RectTransform>().ScaleWithTarget(
            selector.SelectedEntity.ProperEntity.transform, 0.5f);

        iconSelector.transform.position =
           Camera.main.WorldToScreenPoint(selector.SelectedEntity.ProperEntity.transform.position);
    }


}