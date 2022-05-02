using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContainer : Interactable
{

    [SerializeField]
    private DialogueController controller;
    public DialogueController Controller => controller;

    private int dialogueToDisplay;

    public override void Interact()
    {
        GameObject DialogueSystem = GameObject.Find("DDisplay");
        DialogueSystem.GetComponent<DialogueDisplayHandler>().StartDialogue(controller.Scripts[dialogueToDisplay], this);
    }

    public void testeContainer()
    {
        Debug.Log("FUCK");
    }

    public void ChangeDialogue(int dialogueId)
    {
        dialogueToDisplay = dialogueId;
        Interact();
    }

}
