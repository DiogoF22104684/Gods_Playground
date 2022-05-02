using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Interactable: MonoBehaviour
{
    [SerializeField]
    private GameObject interactIcon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (interactIcon == null) return;
        if (collision.gameObject.tag == "Player")
        {
            interactIcon.SetActive(true);
            interactIcon.GetComponent<Animator>().Play("IconEnter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interactIcon == null) return;
        if (collision.gameObject.tag == "Player")
            //interactIcon.SetActive(false);
            interactIcon.GetComponent<Animator>().Play("IconExit");
    }

    public abstract void Interact();
}