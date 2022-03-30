using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionPoint : MonoBehaviour, IPointerClickHandler
{
    private ActionPointManager apManager;
    private int index;

    public void Config(int index, ActionPointManager apManager)
    {
        this.apManager = apManager;
        this.index = index;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        apManager.ClickPoint(index);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
