using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
            if (inSetup) 
            {
                
            }
            else
            {

            }

            //inSetup = !inSetup;
        }
    }
}
