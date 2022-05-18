using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class TileInteractable : Interactable
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private CinemachineVirtualCamera cam;
    bool toggle;

    [SerializeField]
    private DialogueDisplayHandler dialogue;
    [SerializeField] private GameObject menu;
    DialogueContainer container;
    private void Start()
    {
        container = GetComponent<DialogueContainer>();
        cam.Priority = 0;
        
    }
    protected override void Update()
    {
        base.Update();        
    }

    public void EndInteraction()
    {
        dialogue.onEndDialogue -= EndInteraction;
        menu.SetActive(false);
        Selected(true);
        player.SetActive(true);
        cam.Priority = 0;
        cam.gameObject.SetActive(false);
        toggle = false;
    }

    public override void Interact()
    {
       
        if (!toggle)
        {
            menu.SetActive(true);
           
           
            Selected(false);
            player.SetActive(false);
            cam.Priority = 20;
            cam.gameObject.SetActive(true);
            toggle = true;
            dialogue.onEndDialogue += EndInteraction;
            dialogue.StartDialogue(container.GetDialogueToDisplay());
        }
      
    }

   
}
