using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(WorldMap))]
public class WorldMapEditor : Editor
{
    bool inSetup;

    private void OnEnable()
    {
        inSetup = false;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WorldMap map = target as WorldMap;

        //string setupText = inSetup ? "Stop Setup" : "Start Setup";
        GUILayout.Space(10);
        if(GUILayout.Button("Setup Map"))
        {
            map.AddTiles();
            map.AddEntities();
        }

        if (GUILayout.Button("Save Data"))
        {
            string s = map.GetData();
            writeFile(s);
        }


        if (GUILayout.Button("Load Data"))
        {
            map.LoadData(File.ReadAllText("Assets/data.json"));           
        }
    }

    public void writeFile(string json)
    {
        // Write JSON to file.
        File.WriteAllText("Assets/data.json", json);
    }
}
