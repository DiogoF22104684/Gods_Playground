using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatLayout : MonoBehaviour
{
    [SerializeField] private Text   Strength, Defense, HP, Dexterity, 
                                    Evasion, Mana, Int, Intu;

    [SerializeField] public PlayerTemplate statsObject;

    // Start is called before the first frame update
    void Start()
    {
        StatWindow();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StatWindow();
    }

    private void StatWindow()
    {
        Strength.text = "Str: " + (int)statsObject.Str;
        Defense.text = "Def: " + (int)statsObject.Def;
        HP.text = "HP: " + (int)statsObject.HP;
        Dexterity.text = "Dex: " + (int)statsObject.Dex;
        Evasion.text = "Eva: " + (int)statsObject.Eva;
        Mana.text = "MP: " + (int)statsObject.Mp;
        Int.text = "Int: " + (int)statsObject.Int;
        Intu.text = "Intu: " + (int)statsObject.Intu;
    }
}
