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
    SerializedProperty function;
    int selectedFunc;

    private void OnEnable()
    {
        config = serializedObject.FindProperty("config");
        funcBas = serializedObject.FindProperty("basicFunc");
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
                (target as BattleMove).SelectFunc(selectedFunc);
                EditorGUILayout.PropertyField(funcBas);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
