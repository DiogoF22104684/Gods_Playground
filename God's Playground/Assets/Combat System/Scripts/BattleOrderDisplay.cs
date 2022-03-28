using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleOrderDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject debugPREFAB;

    private List<GameObject> orderIcons;

    [SerializeField]
    private float paddingLeft;
    [SerializeField]
    private float offset;

    private RectTransform rectTrans;
    private RectTransform iconRectTrans;

    private void Awake()
    {
        orderIcons = new List<GameObject> { };
        rectTrans = GetComponent<RectTransform>();
        iconRectTrans = debugPREFAB.GetComponent<RectTransform>();
    }

    public void UpdateDisplay(IEnumerable<BattleEntity> turnorder)
    {
       
        foreach(GameObject g in orderIcons)
        {
            Destroy(g);
        }
        orderIcons.Clear();



        //Wtv
        int i = 0;
        foreach (BattleEntity be in turnorder)
        {
            
            Vector3 newPosition = GetIconPos(i);

            GameObject newIcon = Instantiate(debugPREFAB, newPosition,
                    Quaternion.identity, transform);

            //Isto vai ser ligeiramente diferente mas por agora da
            if (be.properEntity is PlayerBattleEntityProper)
            {
                newIcon.GetComponent<Image>().color = Color.green;
            }
            else
            {
                newIcon.GetComponent<Image>().color = Color.red;
            }
            orderIcons.Add(newIcon);
            i++;
        }
    }

    private Vector3 GetIconPos(int i)
    {
        float iconExtents = iconRectTrans.rect.width / 2;
        float xExtents = rectTrans.rect.width / 2;
        float leftMostPoint = transform.position.x - xExtents;
        float letPadding = leftMostPoint + iconExtents + paddingLeft;
        return transform.position.x(letPadding + ((iconRectTrans.rect.width + offset) * i));
    }
}
