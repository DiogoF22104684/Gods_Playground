using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Reflection;

[CustomEditor(typeof(BasicFunction))]
public class BasicFunctionEditor : Editor
{
    SerializedProperty baseValue;
    SerializedProperty selectedProp1, selectedProp2, selectedProp3;

    void OnEnable()
    {
        baseValue = serializedObject.FindProperty("baseValue");
        selectedProp1 = serializedObject.FindProperty("selected1");
        selectedProp2 = serializedObject.FindProperty("selected2");
        selectedProp3 = serializedObject.FindProperty("selected3");


        ////This totally needs a rework
        //BasicFunction bF = target as BasicFunction;

        //PropertyInfo[] propInfo = typeof(BattleEntity).GetProperties()
        //    .Where(prop => prop.IsDefined(typeof(MoveAffecterAttribute), false)).ToArray();

        //bF.firstParam = propInfo[selectedProp1.intValue];
        //bF.secondParam = propInfo[selectedProp2.intValue];
        //bF.thirdParam = propInfo[selectedProp3.intValue];
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        BasicFunction bF = target as BasicFunction;

        PropertyInfo[] propInfo = typeof(BattleEntity).GetProperties()
            .Where(prop => prop.IsDefined(typeof(MoveAffecterAttribute), false)).ToArray();

        string[] propNames = propInfo
            .Select(prop => prop.Name).ToArray();

        selectedProp1.intValue =
            EditorGUILayout.Popup(new GUIContent("First Param"), selectedProp1.intValue, propNames);
        selectedProp2.intValue =
            EditorGUILayout.Popup(new GUIContent("Second Param"), selectedProp2.intValue, propNames);
        selectedProp3.intValue =
            EditorGUILayout.Popup(new GUIContent("Third Param"), selectedProp3.intValue, propNames);

        EditorGUILayout.PropertyField(baseValue);

        //Should use Serielized property but cant figure out how
        //to use it with PropertyInfo
        bF.firstParam = propInfo[selectedProp1.intValue];
        bF.secondParam = propInfo[selectedProp2.intValue];
        bF.thirdParam = propInfo[selectedProp3.intValue];


        string valueToChange = string.Format($"{propNames[selectedProp1.intValue]}");
        string fistParam = $"{baseValue.intValue} + ({baseValue.intValue}* ATTACKER {propNames[selectedProp2.intValue]}))";

        EditorGUILayout.LabelField($"First Param = {fistParam} * Roll");

        //string test = $"{baseValue.intValue} + ATTACKER {"1 + " + propNames[selectedProp2.intValue]}";
        //GUILayout.Label(test);
        


        GUILayout.Label($"TARGET {valueToChange} -=" +
            $" First Param - (FirstParam * TARGET {propNames[selectedProp3.intValue]})");


        serializedObject.ApplyModifiedProperties();
    }
}
