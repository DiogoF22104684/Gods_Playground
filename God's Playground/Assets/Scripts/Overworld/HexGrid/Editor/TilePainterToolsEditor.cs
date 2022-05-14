using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System;

[CustomEditor(typeof(TilePainterTools))]
public class TilePainterToolsEditor : Editor
{
    SerializedProperty inPaint;

    HexGrid grid;

    void OnEnable()
    {
        if (!Application.isEditor)
        {
            Destroy(this);
        }

        inPaint = serializedObject.FindProperty("paint");
        
        TilePainterTools painter = target as TilePainterTools;
        SceneView.duringSceneGui += OnScene;
        grid = painter.GetGrid();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        bool paint = inPaint.boolValue;
        string paintText = paint ? "End Paint" : "Start Paint";
        if (GUILayout.Button(paintText))
        {
            inPaint.boolValue = !paint;
        }


        serializedObject.ApplyModifiedProperties();
    }

    void OnScene(SceneView scene)
    {
        try
        {
            bool t = target is TilePainterTools;
            if (!inPaint.boolValue) return;

        }
        catch
        {
            SceneView.duringSceneGui -= OnScene;
            return;
        }

        TilePainterTools painter = target as TilePainterTools;

        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 2)
        {

            Vector3 mousePos = e.mousePosition;
            float ppp = EditorGUIUtility.pixelsPerPoint;
            mousePos.y = scene.camera.pixelHeight - mousePos.y * ppp;
            mousePos.x *= ppp;

           

            floodFill(grid.Grid,0,0,
                (x,y) => { return grid.Grid[x,y].Tile != null; }
                ,(x,y) => { InstatiateTile(x,y,painter); } 
                );

           e.Use();
        }

   
    }

    public void InstatiateTile(int x, int y, TilePainterTools painter)
    {
        Vector3 pos = grid.Grid[x,y].Pos;
        
        GameObject newTile = Instantiate(
                  painter.tile, pos,
                  Quaternion.identity, painter.transform);
        grid.Grid[x, y].Tile = newTile;
    }



    void floodFill<T>(T[,] matrix, int x, int y, Func<int, int, bool> query, Action<int,int> action)
    {
        
        // Base cases 
        if (x < 0 || x >= matrix.GetLength(0) || y < 0 || y >= matrix.GetLength(1))
            return;
        if (query?.Invoke(x, y) ?? true)
        {
            Debug.Log("kkk");
            return;
        }

        Debug.Log("hmmm?");
        // Replace the color at cell (x, y) 
        action?.Invoke(x,y);

        // Recursively call for north, east, south and west 
        floodFill(matrix, x + 1, y, query, action);
        floodFill(matrix, x - 1, y, query, action);
        floodFill(matrix, x, y + 1, query, action);
        floodFill(matrix, x, y - 1, query, action);
    }
}
