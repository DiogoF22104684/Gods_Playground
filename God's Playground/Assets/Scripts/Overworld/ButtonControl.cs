using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    [SerializeField] private GameObject invButton;
    [SerializeField] private GameObject profButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject revealButton;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private int timeMultiplier = 7;

    private bool revealed;
    private Vector3 intialPos1;
    private Vector3 intialPos2;
    private Vector3 intialPos3;
    private Vector3 intialPos4;
    private Quaternion intialRot;
    private Quaternion target;

    // Start is called before the first frame update
    void Start()
    {
        target = Quaternion.Euler(0, 0, 180);

        revealed = false;
        
        intialRot = revealButton.transform.rotation;

        intialPos1 = invButton.transform.position;
        intialPos2 = equipButton.transform.position;
        intialPos3 = profButton.transform.position;
        intialPos4 = settingsButton.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
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
                        intialPos1, Time.deltaTime * timeMultiplier);
            
            equipButton.transform.position = 
                        Vector3.Lerp(equipButton.transform.position, 
                        intialPos2, Time.deltaTime * timeMultiplier);

            profButton.transform.position = 
                        Vector3.Lerp(profButton.transform.position, 
                        intialPos3, Time.deltaTime * timeMultiplier);
            
            settingsButton.transform.position = 
                        Vector3.Lerp(settingsButton.transform.position, 
                        intialPos4, Time.deltaTime * timeMultiplier);
            
        }
        else
        {
            revealButton.transform.rotation = 
                        Quaternion.Slerp(revealButton.transform.rotation, 
                        intialRot, 
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
}
