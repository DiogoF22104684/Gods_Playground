using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class EquipmentBoard
{
    [SerializeField]
    private Equipment head, chest, legs, asse1, asse2;

    public Equipment this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return head;
                case 1:
                    return chest;
                case 2:
                    return legs;
                case 3:
                    return asse1;
                case 4:
                    return asse2;
            }
            return null;
        }
        set
        {
            switch (index)
            {
                case 0:
                    head = value;
                    break;
                case 1:
                    chest = value;
                    break;
                case 2:
                    legs = value;
                    break;
                case 3:
                    asse1 = value;
                    break;
                case 4:
                    asse2 = value;
                    break;
                default:
                    throw new Exception($"{index} is not a valid equipment piece. Keep it between 0 and 4");
                    break;
            }
        
        }
    }

   
    public bool CanEquip(int o, Equipment equipment)
    {
        if (equipment.EquimentType == (EquipmentType)o)
            return true;
        return false;
    }
}