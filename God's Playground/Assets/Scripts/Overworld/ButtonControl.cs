using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    [SerializeField] private GameObject invButton;
    [SerializeField] private GameObject profButton;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject revealButton;
    [SerializeField] private int timeMultiplier = 7;
    private GameObject inventoryMenu;
    private GameObject profileMenu;
    private GameObject spellsMenu;
    private bool revealed;
    private Vector3 intialPos1;
    private Vector3 intialPos2;
    private Vector3 intialPos3;
    private Quaternion intialRot;
    private Quaternion target;

    // Start is called before the first frame update
    void Start()
    {
        inventoryMenu = GameObject.Find("Inventory Menu");
        profileMenu = GameObject.Find("Profile Menu");
        spellsMenu = GameObject.Find("Spells Menu");

        inventoryMenu.SetActive(false);
        profileMenu.SetActive(false);
        spellsMenu.SetActive(false);

        target = Quaternion.Euler(0, 0, 180);

        revealed = false;
        
        intialRot = revealButton.transform.rotation;

        intialPos1 = invButton.transform.position;
        intialPos2 = equipButton.transform.position;
        intialPos3 = profButton.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
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


            spellsMenu.SetActive(false);
            inventoryMenu.SetActive(false);
            profileMenu.SetActive(false);
        }


    }

    public void RevealButtons()
    {
        revealed = !revealed;
    }

    public void InventoryButton()
    {
        if (!inventoryMenu.activeSelf)
        {
            inventoryMenu.SetActive(true);
            spellsMenu.SetActive(false);
        }
        else
            inventoryMenu.SetActive(false);
            
    }
    public void ProfileButton()
    {
        if (!profileMenu.activeSelf)
            profileMenu.SetActive(true);
        else
            profileMenu.SetActive(false);
            spellsMenu.SetActive(false);
            inventoryMenu.SetActive(false);
    }
    public void SpellsButton()
    {
        if (!spellsMenu.activeSelf)
        {
            spellsMenu.SetActive(true);
            inventoryMenu.SetActive(false);
        }
        else
            spellsMenu.SetActive(false);
    }

}
