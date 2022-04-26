using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;

[CustomPropertyDrawer(typeof(BattlePropertyInfo))]
public class BattlePropertyInfoDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        EditorGUI.BeginProperty(position, label, property);
        //base.OnGUI(position, property, label);

        SerializedProperty prop = property.FindPropertyRelative("selected");
       
        PropertyInfo[] propInfo = typeof(BattleEntity).GetProperties()
            .Where(prop => prop.IsDefined(typeof(MoveAffecterAttribute), false)).ToArray();

        string[] propNames = propInfo
            .Select(prop => prop.Name).ToArray();

        //Rect paramRect = new Rect(position.t,0,0,0);

        prop.intValue =
             EditorGUI.Popup(position, property.name.CapFirst()
                , prop.intValue, propNames);

        EditorGUI.EndProperty();
    }
}
