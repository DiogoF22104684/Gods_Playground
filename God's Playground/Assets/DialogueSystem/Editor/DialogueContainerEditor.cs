using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueContainer))] [CanEditMultipleObjects]
public class DialogueContainerEditor : Editor
{
    SerializedProperty useSoloScript;
    SerializedProperty script;
    SerializedProperty controller;

    void OnEnable()
    {
        useSoloScript = serializedObject.FindProperty("useSoloScript");
        script = serializedObject.FindProperty("script");
        controller = serializedObject.FindProperty("controller");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();

        if (useSoloScript.boolValue == true)
        {
            GUILayout.Label("Dialogue Script");
        }
        else
        {
            GUILayout.Label("Dialogue Controller");
        }

        GUILayout.FlexibleSpace();
        float defaultWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 5;

        useSoloScript.boolValue = EditorGUILayout.Toggle(useSoloScript.boolValue);
        EditorGUIUtility.labelWidth = defaultWidth;
       
        if (useSoloScript.boolValue == true)
        {
           EditorGUILayout.PropertyField(script, new GUIContent(""));
        }
        else
        {
            EditorGUILayout.PropertyField(controller, new GUIContent(""));
        }
      
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
}
