using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class WorldMap : MonoBehaviour
{
    [SerializeField]
    List<GameObject> map;
    
    [SerializeField] [OnPrefab]
    GameObject mapPlacer;
    
    [Header("Setup")] [SerializeField]
    float walkRadius;
    [SerializeField]
    float viewRadius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTiles()
    {
        map.Clear();
        foreach(Transform tile in mapPlacer.transform)
        {
            map.Add(tile.gameObject);
        }
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position,new Vector3(walkRadius,0,walkRadius));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(viewRadius, 0, viewRadius));
    }
}

