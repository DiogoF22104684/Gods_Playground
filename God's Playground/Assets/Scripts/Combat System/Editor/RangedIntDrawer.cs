using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RangedInt))]
public class RangedIntDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
        GUIContent label)
    {
        return 47;
    }

    public override void OnGUI(Rect position, SerializedProperty property,
        GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty vMin = 
            property.FindPropertyRelative("minV");
        SerializedProperty vMax = 
            property.FindPropertyRelative("maxV");


        int currentMin = vMin.intValue;
        int currentMax = vMax.intValue;

        float indent = position.width * 0.05f;
        float half = position.width / 2;        
        float labelSize = 35;
        float offset = indent;
        float rectSize = half - labelSize - offset;

        float rectY = (position.y + position.height / 2);
        float sizeY = (position.height / 2) - 5f;

        Rect nameRect = new Rect(position.x,
            position.y, position.width, 20);

        Rect maxRect = new Rect(indent + position.x + 
            half + labelSize, rectY, rectSize, sizeY);

        Rect minRect = new Rect(indent + position.x + 
            labelSize, rectY, rectSize, sizeY);
        
        Rect maxRectLabel = new Rect(indent + position.x +
            half, rectY, labelSize, sizeY);

        Rect minRectLabel = new Rect(indent + position.x,
            rectY, labelSize, sizeY);


        string pName = property.name[0].ToString().ToUpper() + 
            property.name.Substring(1,property.name.Length - 1);


        EditorGUI.LabelField(nameRect,
            new GUIContent(pName,
            "Returns a value between the min and the max"));
        EditorGUI.LabelField(maxRectLabel, 
            new GUIContent("Max", 
            "Maximum value of the range. Inclusive"));
        EditorGUI.LabelField(minRectLabel,
            new GUIContent("Min", 
            "Minimum value of the range. Inclusive."));

        EditorGUI.PropertyField(minRect, vMin, GUIContent.none);
        EditorGUI.PropertyField(maxRect, vMax, GUIContent.none);

        bool wrongOrder = vMax.intValue < vMin.intValue;
        if (vMin.intValue != currentMin && wrongOrder)
            vMax.intValue = vMin.intValue;

        else if (vMax.intValue != currentMax && wrongOrder)
            vMin.intValue = vMax.intValue;



        EditorGUI.EndProperty();
    }
}
