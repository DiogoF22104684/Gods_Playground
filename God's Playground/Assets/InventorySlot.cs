using UnityEngine;
using UnityEngine.UI;
using System;

public class InventorySlot: MonoBehaviour
{
    IInventoryItem item;
    [SerializeField]
    Image image;
    [SerializeField]
    Button button;

    public Action onClick;

    public void Start()
    {
        button.onClick.AddListener(()=> { onClick?.Invoke(); });
    }

    public void SetItem(IInventoryItem item)
    {
        if (item != null)
            image.color = Color.white;
        else
            image.color = new Color(0.60f, 0.248f, 0.150f);
        image.sprite = item?.Icon;
    }

}