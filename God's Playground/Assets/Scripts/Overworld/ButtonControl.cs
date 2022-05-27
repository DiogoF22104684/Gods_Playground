using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    [SerializeField] private GameObject invButton;
    [SerializeField] private GameObject profButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject revealButton;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private int timeMultiplier = 7;

    private bool revealed;
    private Vector3 initialPos1;
    private Vector3 initialPos2;
    private Vector3 initialPos3;
    private Vector3 initialPos4;
    private Vector3 transformPos1;
    private Vector3 transformPos2;
    private Vector3 transformPos3;
    private Vector3 transformPos4;
    private Quaternion initialRot;
    private Quaternion target;

    // Start is called before the first frame update
    void Start()
    {
        target = Quaternion.Euler(0, 0, 180);

        revealed = false;
        
        initialRot = revealButton.transform.rotation;   

        initialPos1 = invButton.transform.position;
        initialPos2 = equipButton.transform.position;
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
            
            equipButton.transform.position = 
                        Vector3.Lerp(equipButton.transform.position, 
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
            revealButton.transform.rotation = 
                        Quaternion.Slerp(revealButton.transform.rotation, 
                        initialRot, 
                        Time.deltaTime * timeMultiplier);
                            
            invButton.transform.position = 
                        Vector3.Lerp(invButton.transform.position, 
                        revealButton.transform.position, 
                        Time.deltaTime * timeMultiplier);
            
            equipButton.transform.position = Vector3.Lerp(equipButton.transform.position, 
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
        
        print(width);

        float offset = width * 0.065f;

        transformPos1 = revealButton.transform.position - new Vector3 (offset, 0f, 0f);
        transformPos2 = transformPos1 - new Vector3 (offset, 0f, 0f);
        transformPos3 = transformPos2 - new Vector3 (offset, 0f, 0f);
        transformPos4 = transformPos3 - new Vector3 (offset, 0f, 0f);
 
    } 
}
