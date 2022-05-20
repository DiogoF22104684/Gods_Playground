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

    [SerializeField] [HideInInspector]
    private bool flatten;
    
    public static implicit operator int(RangedInt self)
    {
        return 
            self.flatten ? Random.Range(self.minV, self.maxV + 1): self.minV;
    }
}
