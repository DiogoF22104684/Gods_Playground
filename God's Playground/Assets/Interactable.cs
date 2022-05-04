using System;
using UnityEngine;

public abstract class Interactable: MonoBehaviour
{
    public GameObject icon;

    protected virtual void Update()
    {
       
    }

    public void Selected(bool v)
    {
        if (icon != null)
            icon.SetActive(v);
    }

    public abstract void Interact();
}