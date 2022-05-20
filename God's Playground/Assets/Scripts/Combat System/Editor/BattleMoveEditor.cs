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
    SerializedProperty funcBas;
    SerializedProperty funcIgDef;
    SerializedProperty function;
    SerializedProperty basicFlat;

    private void OnEnable()
    {
        //Isto n é muito scalable  
        funcBas = serializedObject.FindProperty("basicFunc");
        funcIgDef = serializedObject.FindProperty("basicIgnoreDef");
        basicFlat = serializedObject.FindProperty("basicFlat");
        function = serializedObject.FindProperty("function");
    }
    public override void OnInspectorGUI()
    {
        
        serializedObject.Update();
        EditorGUI.BeginChangeCheck();
        DrawPropertiesExcluding(serializedObject, "basicFunc", "basicIgnoreDef", "basicFlat");
       

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

        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }
}
