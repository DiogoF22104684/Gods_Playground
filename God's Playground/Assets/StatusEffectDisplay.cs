using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectDisplay : MonoBehaviour, IConfigurable
{
    [SerializeField]
    List<StatusEffectTimer> statusEffects;

    [SerializeField]
    GameObject iconPREFAB;

    private List<GameObject> iconList;


    private void Awake()
    {
        iconList = new List<GameObject> { };
    }

    public void Config(BattleEntity entity)
    {
      
        statusEffects = entity.statusEffects;
        CleanStatusList();

        for (int i = 0; i < statusEffects.Count; i++)
        {
            StatusEffect se = statusEffects[i].Effect;
            GameObject newIcon =
                Instantiate(iconPREFAB,
                transform.position,
                Quaternion.identity,
                this.transform);

            iconList.Add(newIcon);
            newIcon.GetComponent<Image>().sprite = se.Icon;
        }

    }

    private void CleanStatusList()
    {
        for (int i = iconList.Count - 1; i >= 0; i--)
        {
            Destroy(iconList[i]);
        }

        iconList.Clear();
    }
}
