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
        SerializedProperty flatten =
            property.FindPropertyRelative("flatten");

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

        Rect flattenRect = new Rect(position.width - 10,
            position.y, position.width, 20);

      


        string pName = property.name.CapFirst();

        flatten.boolValue = EditorGUI.Toggle(flattenRect,flatten.boolValue);


        Rect maxRect = new Rect(indent + position.x +
          half + labelSize, rectY, rectSize, sizeY);

        Rect minRect = new Rect(indent + position.x +
            labelSize + 5, rectY, position.width - 55, sizeY);

        Rect maxRectLabel = new Rect(indent + position.x +
            half, rectY, labelSize, sizeY);

        Rect minRectLabel = new Rect(indent + position.x,
            rectY, labelSize, sizeY);

        string minValue = "Value";
        string minValueTooltip = "Int value of the property";


        if (flatten.boolValue == false)
        {
            minValue = "Min";
            minValueTooltip = "Minimum value of the range. Inclusive";

            //Change rects to accommodate max value
            minRect = new Rect(indent + position.x +
            labelSize, rectY, rectSize, sizeY);

            minRectLabel = new Rect(indent + position.x,
            rectY, labelSize, sizeY);

            //Display max value label
            EditorGUI.LabelField(maxRectLabel,
                new GUIContent("Max",
                "Maximum value of the range. Inclusive"));

            //Display max property
            EditorGUI.PropertyField(maxRect, vMax, GUIContent.none);
        }

        //Display label of property
        EditorGUI.LabelField(nameRect,
         new GUIContent(pName,
         "Returns a value between the min and the max"));

        //Display min value label
        EditorGUI.LabelField(minRectLabel,
            new GUIContent(minValue,
            minValueTooltip));

        //Display min property 
        EditorGUI.PropertyField(minRect, vMin, GUIContent.none);
       

        bool wrongOrder = vMax.intValue < vMin.intValue;
        if (vMin.intValue != currentMin && wrongOrder)
            vMax.intValue = vMin.intValue;

        else if (vMax.intValue != currentMax && wrongOrder)
            vMin.intValue = vMax.intValue;



        EditorGUI.EndProperty();
    }
}
