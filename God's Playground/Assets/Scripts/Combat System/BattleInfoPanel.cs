using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleInfoPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    public void Display(BattleEntityProper entity)
    {
        text.text = entity.ToString();
    }

}
