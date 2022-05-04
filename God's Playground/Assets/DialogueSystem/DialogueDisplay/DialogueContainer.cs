using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueContainer : MonoBehaviour
{

    [SerializeField][HideInInspector]
    private DialogueController controller;
    public DialogueController Controller => controller;

    [SerializeField][HideInInspector]
    private DialogueScript script;
    public DialogueScript Script => script;

    [SerializeField][HideInInspector]
    private bool useSoloScript;
    public bool UseSoloScript => useSoloScript;
    
    private int dialogueToDisplay => controller.startScriptNum;

    public void Interact()
    {
        //This is bad
        GameObject DialogueSystem = GameObject.Find("DDisplay");
        if (UseSoloScript == false)
            DialogueSystem.GetComponent<DialogueDisplayHandler>().StartDialogue(controller.Scripts[dialogueToDisplay], this);
        if (UseSoloScript == true)
            DialogueSystem.GetComponent<DialogueDisplayHandler>().StartDialogue(Script, this);
    }

    public DialogueScript GetDialogueToDisplay()
    {
        if (UseSoloScript == false)
            return controller.Scripts[dialogueToDisplay];
        else
        {
            return Script;
        }
    }

    public void ChangeDialogue(int dialogueId, bool next)
    {
        controller.startScriptNum = dialogueId;
        if (next)
            Interact();
    }

    public void ChangeDialogueAndNext(int dialogueId)
    {
        controller.startScriptNum = dialogueId;
        Interact();
    }

}
