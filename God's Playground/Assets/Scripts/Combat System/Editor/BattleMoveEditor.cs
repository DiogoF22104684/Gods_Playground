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
    SerializedProperty icon;
    SerializedProperty config;
    SerializedProperty funcBas;
    SerializedProperty funcIgDef;
    SerializedProperty function;
    SerializedProperty basicFlat;
    SerializedProperty statusEffect;

    private void OnEnable()
    {
        //Isto n � muito scalable  
        icon = serializedObject.FindProperty("icon");
        config = serializedObject.FindProperty("config");
        funcBas = serializedObject.FindProperty("basicFunc");
        funcIgDef = serializedObject.FindProperty("basicIgnoreDef");
        basicFlat = serializedObject.FindProperty("basicFlat");
        function = serializedObject.FindProperty("function");
        statusEffect = serializedObject.FindProperty("statusEffect").
            FindPropertyRelative("statusEffects");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(icon);

        GUILayout.Space(10);

        EditorGUILayout.PropertyField(config);

        GUILayout.Space(10);

        EditorGUILayout.PropertyField(statusEffect);

        string[] values =
            System.Enum.GetValues(typeof(FuncAffectType))
                .Cast<FuncAffectType>().Select(x => x.ToString()).ToArray();

        GUILayout.Space(10);

        function.enumValueIndex = EditorGUILayout.Popup(
            new GUIContent(function.name.CapFirst()),
            function.enumValueIndex, values);

        switch (function.enumValueIndex) 
        {
            case 0:
               
                EditorGUILayout.PropertyField(funcBas);
                break;
            case 1:                
                EditorGUILayout.PropertyField(funcIgDef);
                break;
            case 2:
                EditorGUILayout.PropertyField(basicFlat);
                break;
        }

         (target as BattleMove).SelectFunc(function.enumValueIndex);

        serializedObject.ApplyModifiedProperties();
    }
}
