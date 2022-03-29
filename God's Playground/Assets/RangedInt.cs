using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct RangedInt
{
    [SerializeField] [Tooltip("Minimum value of the range. Inclusive")]
    private int minV;
    
    [SerializeField] [Tooltip("Maximum value of the range. Inclusive")]
    private int maxV;

    
    public static implicit operator int(RangedInt self)
    {
        return Random.Range(self.minV, self.maxV + 1);
    }

}
