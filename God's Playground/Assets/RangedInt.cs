using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct RangedInt
{
    [SerializeField] 
    private int minV;
    
    [SerializeField] 
    private int maxV;

    
    public static implicit operator int(RangedInt self)
    {
        return Random.Range(self.minV, self.maxV + 1);
    }

}
