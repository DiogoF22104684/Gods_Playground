using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private Interactable currentInteractable;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Interactable")
        {
            currentInteractable = other.GetComponent<Interactable>();
            currentInteractable.Selected(true); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            currentInteractable.Selected(false);

            currentInteractable = null;

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(currentInteractable != null)
                currentInteractable.Interact();
        }
    }

}


