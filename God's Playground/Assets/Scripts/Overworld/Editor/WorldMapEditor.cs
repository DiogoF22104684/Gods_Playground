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
            PrefabUtility.ApplyPrefabInstance(map.gameObject,InteractionMode.AutomatedAction);
        }

        if (GUILayout.Button("Setup Save File"))
        {
            string s = map.GetData();
            #if UNITY_EDITOR
            try
            {
                PrefabUtility.ApplyPrefabInstance(map.gameObject, InteractionMode.AutomatedAction);
            }
            catch
            {

            }
            #endif
            SaveLoadManager.Instance.Save();
        }

        if (GUILayout.Button("Test Load Data"))
        {
            SaveLoadManager.Instance.Load();       
        }
    }

}
