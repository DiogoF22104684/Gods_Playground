using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;

[CustomPropertyDrawer(typeof(OnPrefabAttribute))]
public class OnPrefabAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        PrefabStage stage = PrefabStageUtility.GetCurrentPrefabStage();

        if (stage != null)
        {
            EditorGUILayout.PropertyField(property, new GUIContent(property.name));
        }

        
    }
}
