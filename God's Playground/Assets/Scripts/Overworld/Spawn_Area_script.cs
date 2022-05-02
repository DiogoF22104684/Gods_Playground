using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Area_script : MonoBehaviour
{
    [SerializeField] [Range(1,10)]float radius;

    public bool InArea(Vector3 pos)
    {
        return  Mathf.Pow(pos.x - transform.position.x, 2) + 
                Mathf.Pow(pos.y - transform.position.y, 2) <=
                Mathf.Pow(radius, 2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 
        radius);
    }
}
