using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HexGrid))]
public class HexGridEditor : Editor
{

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        

        SerializedProperty size = serializedObject.FindProperty("size");
        SerializedProperty type = serializedObject.FindProperty("type");

        float tempSize = size.floatValue;
        int typeSize = type.intValue;

       
        EditorGUILayout.PropertyField(size);

       

        EditorGUILayout.PropertyField(type);

      

        serializedObject.ApplyModifiedProperties();

        if (tempSize != size.floatValue)
        {
            //On Change Size
            (target as HexGrid).ChangeSize();
        }

        if (typeSize != type.intValue)
        {
            //On Change Type
            (target as HexGrid).ChangeSize();
        }

        base.OnInspectorGUI();

    }
}
