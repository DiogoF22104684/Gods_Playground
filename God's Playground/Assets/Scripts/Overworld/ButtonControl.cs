using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    [SerializeField] private GameObject invButton, profButton, spellsButton, revealButton, settingsButton, invMenu, profMenu, spellsMenu, settingsMenu;
    [SerializeField] private int timeMultiplier = 7;

    private bool revealed;
    private Vector3 initialPos1, initialPos2, initialPos3, initialPos4;
    private Vector3 transformPos1, transformPos2, transformPos3, transformPos4;
    private Quaternion initialRot, target;

    // Start is called before the first frame update
    void Start()
    {
        target = Quaternion.Euler(0, 0, 180);

        revealed = false;
        
        initialRot = revealButton.transform.rotation;   

        initialPos1 = invButton.transform.position;
        initialPos2 = spellsButton.transform.position;
        initialPos3 = profButton.transform.position;
        initialPos4 = settingsButton.transform.position; 

    }

    // Update is called once per frame
    void Update()
    {
        positionSetup();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            RevealButtons();
        }

        if (revealed)
        {
            revealButton.transform.rotation = 
                        Quaternion.Slerp(revealButton.transform.rotation, 
                        target, 
                        Time.deltaTime * timeMultiplier);
                            
            invButton.transform.position = 
                        Vector3.Lerp(invButton.transform.position, 
                        transformPos1, Time.deltaTime * timeMultiplier);

            spellsButton.transform.position = 
                        Vector3.Lerp(spellsButton.transform.position, 
                        transformPos2, Time.deltaTime * timeMultiplier);

            profButton.transform.position = 
                        Vector3.Lerp(profButton.transform.position, 
                        transformPos3, Time.deltaTime * timeMultiplier);
            
            settingsButton.transform.position = 
                        Vector3.Lerp(settingsButton.transform.position, 
                        transformPos4, Time.deltaTime * timeMultiplier);
            
        }

        else
        {
            DisableMenus();

            revealButton.transform.rotation = 
                        Quaternion.Slerp(revealButton.transform.rotation, 
                        initialRot, 
                        Time.deltaTime * timeMultiplier);
                            
            invButton.transform.position = 
                        Vector3.Lerp(invButton.transform.position, 
                        revealButton.transform.position, 
                        Time.deltaTime * timeMultiplier);

            spellsButton.transform.position = Vector3.Lerp(spellsButton.transform.position, 
                        revealButton.transform.position, 
                        Time.deltaTime * timeMultiplier);

            profButton.transform.position = Vector3.Lerp(profButton.transform.position, 
                        revealButton.transform.position, 
                        Time.deltaTime * timeMultiplier);
            
            settingsButton.transform.position = Vector3.Lerp(settingsButton.transform.position, 
                        revealButton.transform.position, 
                        Time.deltaTime * timeMultiplier);

        }


    }

    public void RevealButtons()
    {
        revealed = !revealed;
    }

    private void positionSetup()
    {
        float width = Screen.width;

        float screenRatio = (float)Screen.width * 0.01f;
               
        float offset = width * 0.065f;

        transformPos1 = revealButton.transform.position - new Vector3 (offset, 0f, 0f);
        transformPos2 = transformPos1 - new Vector3 (offset, 0f, 0f);
        transformPos3 = transformPos2 - new Vector3 (offset, 0f, 0f);
        transformPos4 = transformPos3 - new Vector3 (offset, 0f, 0f);
 
    } 

    private void DisableMenus()
    {
        invMenu.SetActive(false);
        profMenu.SetActive(false);
        spellsMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }
}
