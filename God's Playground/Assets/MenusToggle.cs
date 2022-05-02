using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class MenusToggle : MonoBehaviour
{

    [SerializeField]
    UnityEvent onSelect;
    [SerializeField]
    UnityEvent onDeselect;

    [SerializeField]
    private bool toggle;

    public void Toggle()
    {
        if (toggle)
        {
            onDeselect?.Invoke();
            toggle = false;
        }
        else
        {
            onSelect?.Invoke();
            toggle = true;
        }
    } 
}
