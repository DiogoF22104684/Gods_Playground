using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class WorldMap : MonoBehaviour
{
    [SerializeField]
    List<GameObject> map;
    

    
    [Header("Setup")] [SerializeField]
    float walkRadius;
    [SerializeField]
    float viewRadius;
    [SerializeField]
    float boardRadius;
    [SerializeField]
    GameObject target;

    List<GameObject> inView;
    private Rect rect;

    //[SerializeField]
    //bool constrain;
    //[Header("Movement Values")]
    //[SerializeField]
    //[ReadOnly]
    //private Vector3 previousPos;
    //private Vector3 currentPos;

    [Header("Prefab Only")]
    [SerializeField] [OnPrefab] GameObject mapPlacer;


    // Start is called before the first frame update
    void Start()
    {
        SetupViewabilityList();
    }

    // Update is called once per frame
    void Update()
    {
        SetupViewabilityList();       
    }

    private void FixedUpdate()
    {
        //if (target == null) return;
      
        //if (constrain)
        //{
        //    previousPos = currentPos;
        //    currentPos = target.transform.position;

        //    if (previousPos != Vector3.zero)
        //    {
        //        Vector3 dir = currentPos - previousPos;

        //        //mapPlacer.transform.position -= dir.y(0);
        //        //ground.transform.position -= dir.y(0);

        //        target.transform.position = 
        //            previousPos.y(target.transform.position.y);
        //        currentPos = previousPos;
        //    }
        //}
    }


    private void SetupViewabilityList()
    {
        inView = new List<GameObject>();
        Vector3 center = target != null ? target.transform.position : transform.position;
        float extend = viewRadius / 2;

        rect = new Rect(new Vector2(center.x - extend, center.z - extend), 
            new Vector2(viewRadius,viewRadius));

       

        foreach (GameObject tile in map)
        {
            if (rect.Contains(
                new Vector2(tile.transform.position.x, tile.transform.position.z)))
            {
                inView.Add(tile);
                tile.SetActive(true);
            }
            else
            {
                tile.SetActive(false);
            }
        }
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
        Vector3 center = target != null ? target.transform.position: transform.position;


        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, new Vector3(viewRadius, 0, viewRadius));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position,new Vector3(walkRadius,0,walkRadius));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(boardRadius, 0, boardRadius));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(rect.center.x, 0, rect.center.y), new Vector3(rect.size.x, 0, rect.size.y));
    }
}

