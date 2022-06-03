using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;

public class WorldMap : MonoBehaviour, ISavable
{
    [SerializeField]
    private int mapId;
    public int ID { get => mapId; set => mapId = value; }

    [SerializeField] [ReadOnly]
    List<GameObject> map;

    [SerializeField]
    List<GameObject> entities;

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

    [Header("Prefab Only")]
    [SerializeField] [OnPrefab] GameObject mapPlacer;
    [SerializeField] [OnPrefab] GameObject entityPlacer;


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

  
    //Can be A LOT more efficient
    private void SetupViewabilityList()
    {
        inView = new List<GameObject>();
        Vector3 center = target != null ? target.transform.position : transform.position;
        float extend = viewRadius / 2;

        rect = new Rect(new Vector2(center.x - extend, center.z - extend), 
            new Vector2(viewRadius,viewRadius));


        foreach (GameObject tile in map)
        {
            if (tile == null) continue;
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

        foreach (GameObject entity in entities)
        {
            if (entity == null) continue;
            
            //VERY BAD
            if (rect.Contains(
                new Vector2(entity.transform.position.x, entity.transform.position.z)))
            {
                inView.Add(entity);
                if(entity.GetComponent<EnemyAgent>()?.IsAlive ?? true)
                {
                    entity.SetActive(true);
                }               
            }
            else
            {
                entity.SetActive(false);
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

    public void AddEntities()
    {
        entities.Clear();
        foreach (Transform entity in entityPlacer.transform)
        {
            entities.Add(entity.gameObject);         
        }
    }

    #region Save/Load
    [SerializeField]
    [ReadOnly]
    private Data data;


    [System.Serializable]
    struct Data
    {
        [SerializeField]
        private Vector3 position;

        public Vector3 Position { get => position; set => position = value; }  
    }

    public string GetData()
    {
        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            Formatting = Newtonsoft.Json.Formatting.None,
            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
        };

        string returnString = "{";

        #region Map Data    

        data.Position = transform.position;

        string mapDataString = "\"mapData\":";
        mapDataString += JsonUtility.ToJson(data);
        mapDataString += ",";
        returnString += mapDataString;
        #endregion


        #region Enemy Data
        string enemyDataString = "\n\"enemies\":[";

        int iterator = 0;
        for (int i = 0; i < entities.Count; i++)
        {
            GameObject g = entities[i];
            ISavable save = g.GetComponent<ISavable>();
            if (save != null)
            {
                save.ID = iterator;
                enemyDataString += save.GetData() + ",";
                iterator++;
            }
        }
        enemyDataString = enemyDataString.Substring(0, enemyDataString.Length - 1);
        enemyDataString += "]}";
        #endregion

        returnString += enemyDataString;
        return returnString;
    }

    public void LoadData(string data)
    {
        dynamic a = JObject.Parse(data);
     
        #region Load Map
        string mapData = a.mapData.ToString();
        this.data = JsonUtility.FromJson<Data>(mapData);
        transform.position = this.data.Position;
        #endregion

        #region Load Enemies
        string enemies = a.enemies.ToString();

        List<object> enemyDataDic = 
            JsonConvert.DeserializeObject<List<object>>(enemies);

        for (int i = 0; i < entities.Count; i++)
        {
            GameObject g = entities[i];
            ISavable save = g.GetComponent<ISavable>();
            if (save != null)
            {
                save.LoadData(enemyDataDic[save.ID].ToString());
            }
        }
        #endregion
    }

    #endregion

    private void OnDrawGizmos()
    {
        Vector3 center = target != null ? target.transform.position : transform.position;


        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, new Vector3(viewRadius, 0, viewRadius));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(walkRadius, 0, walkRadius));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(boardRadius, 0, boardRadius));

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(rect.center.x, 0, rect.center.y), new Vector3(rect.size.x, 0, rect.size.y));
    }

}

