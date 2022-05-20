using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Saves;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class SaveLoadManager: MonoBehaviour
{
    [SerializeField]
    private List<GameObject> savables;

    [SerializeField] [ReadOnly]
    private WorldMap currentMap;

    [SerializeField]
    private WorldMap[] maps;

    static string DirPath => Application.persistentDataPath + $"/Saves/";


    public static SaveLoadManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Load((Saves)PlayerPrefs.GetInt("saveType"));
    }

    public void Save(Saves saveType = Autosave)
    {
        string saveString = "";
       
        List<ISavable> s = 
            savables.Select(x => x.GetComponent<ISavable>())
            .Where(x => x != null)
            .ToList();

        foreach(ISavable save in s)
        {
            saveString += save.GetData() + "";
        }

        saveString = $"{{\"currentMapID\": \"{currentMap.ID}\" }}";

       

        if(!Directory.Exists(DirPath))
            Directory.CreateDirectory(DirPath);
        
        
        File.WriteAllText(DirPath + 
            $"{saveType}/Main_{saveType}.json", saveString);

        File.WriteAllText(DirPath + 
            $"{saveType}/Map_{currentMap.ID}_Save_{saveType}.json", currentMap.GetData());
    }
 
    public void Load(Saves saveType = Autosave)
    {
        if (!Directory.Exists(DirPath))
            Directory.CreateDirectory(DirPath + $"/{saveType}");

        string maindataString = 
            File.ReadAllText(DirPath + $"{saveType}/Main_{saveType}.json");
        dynamic mainData = JObject.Parse(maindataString);
        int mapId = mainData.currentMapID;

        //Load playerData

        //Load Dm Data

        //Load Map and its Data

        string mapDataString =
            File.ReadAllText(DirPath + $"{saveType}/Map_{mapId}_Save_{ saveType}.json");

        currentMap = maps.First(x => x.ID == mapId);
        currentMap.LoadData(mapDataString);

    }

    /// <summary>
    /// Return the value inside the save given the specific operation and the 
    /// save file name.
    /// </summary>
    /// <param name="func">Operation to retrieve saved data.
    /// Given the dynamic value of the file, call the specific field 
    /// to retrieve the data stored in it.
    /// </param>
    /// <param name="fileName">The name of the save file</param>
    /// <returns>Data inside the specific field</returns>
    public static dynamic ForceGetValue(Func<dynamic, dynamic> func, string fileName = "Main")
    {       
        dynamic mainData = GetAutoSave(fileName);
        return func?.Invoke(mainData);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="func"></param>
    /// <param name="fileName"></param>
    /// <param name="valueName"></param>
    /// <returns></returns>
    public static List<dynamic>  ForceGetListValue(Func<dynamic, dynamic> func, string fileName)
    {

        dynamic mainData = GetAutoSave(fileName);

        List<dynamic> dataDic =
            JsonConvert.DeserializeObject<List<dynamic>>(func?.Invoke(mainData).ToString());

        return dataDic;
    }


    //Very dumb but im so tired of trying to make this work. PLEASE JUST WORK GOD
    //Maybe later I'll make this generic
    public static void ForceSetListValue(
        Func<dynamic, dynamic> func, int id, int newValue, string saveName)
    {
        dynamic mainData = GetAutoSave(saveName);
        dynamic list = func?.Invoke(mainData);
        List<EnemyAgent.Data> data =
            JsonConvert.DeserializeObject<List<EnemyAgent.Data>>(list.ToString());
        data[id].States = (EnemyAgent.States)newValue;

        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            Formatting = Newtonsoft.Json.Formatting.None,
            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,          
        };

        mainData.enemies = JToken.FromObject(data);

        File.WriteAllText(DirPath +
            $"{Autosave}/Map_{0}_Save_{Autosave}.json", mainData.ToString());
    }

    private static void RewriteSaveFile(string fileData, string fileName)
    {

    }

    private static dynamic GetAutoSave(string saveName)
    {
        string dirPath = Application.persistentDataPath + $"/Saves/{Autosave}";
        string dataString =
            File.ReadAllText(dirPath + $"/{saveName}_{Autosave}.json");
        return JObject.Parse(dataString);
    }
}
