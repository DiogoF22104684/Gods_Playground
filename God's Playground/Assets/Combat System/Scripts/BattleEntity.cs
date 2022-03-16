using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleEntity : MonoBehaviour
{
   
    [MoveAffecter]
    public float hp { get; set; }

    [MoveAffecter]
    public float atk { get; set; }

    [MoveAffecter]
    public float def { get; set; }

    public float GetRoll()
    {
        return 5f / 6f;
    }

    public BattleEntity(float hp)
    {
        this.hp = hp;
        atk = 0.4f;
        def = 0.3f;
    }

    public ProperBattleEntity properEntity { get; }

    public override string ToString()
    {
        return $"Hp: {hp} \nAtk: {atk} \nDef:{def}";
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
