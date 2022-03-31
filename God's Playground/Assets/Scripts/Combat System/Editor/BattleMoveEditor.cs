using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CombatSystem;
using UnityEngine.UIElements;
using System.Linq;

[CustomEditor(typeof(BattleMove))]
public class BattleMoveEditor : Editor 
{

    SerializedProperty config;
    SerializedProperty funcBas;
    SerializedProperty funcIgDef;
    SerializedProperty function;
    int selectedFunc;

    private void OnEnable()
    {
        config = serializedObject.FindProperty("config");
        funcBas = serializedObject.FindProperty("basicFunc");
        funcIgDef = serializedObject.FindProperty("basicIgnoreDef");
        function = serializedObject.FindProperty("function");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(config);
        string[] values =
            System.Enum.GetValues(typeof(FuncType))
                .Cast<FuncType>().Select(x => x.ToString()).ToArray();

        selectedFunc = EditorGUILayout.Popup(
            new GUIContent(function.name.CapFirst()), 
            selectedFunc,values);

        switch (selectedFunc) 
        {
            case 0:
               
                EditorGUILayout.PropertyField(funcBas);
                break;
            case 1:                
                EditorGUILayout.PropertyField(funcIgDef);
                break;
        }

         (target as BattleMove).SelectFunc(selectedFunc);

        serializedObject.ApplyModifiedProperties();
    }
}
