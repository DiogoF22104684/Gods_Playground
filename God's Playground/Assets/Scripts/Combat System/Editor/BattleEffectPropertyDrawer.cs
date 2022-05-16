using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CombatSystem
{

    [CustomPropertyDrawer(typeof(StatusEffecOverrider))]
    public class BattleEffectPropertyDrawer : PropertyDrawer
    {
        private bool initialized = false;
        SerializedProperty statusEffect;
        SerializedProperty overrideValue;
        SerializedProperty coolDown;
        SerializedProperty team;
        SerializedProperty type;

        int numbOfOverriders => 3;
        int OverrideHeigh => 
            2 + (overrideValue?.boolValue ?? false ? numbOfOverriders : 0);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {

            return base.GetPropertyHeight(property, label) * OverrideHeigh;
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!initialized)
                Init(property);
            float heighPropValue = position.height / OverrideHeigh;

            Rect statusRect = new Rect(position.x, position.y, position.width, heighPropValue);
            Rect overrideRect = new Rect(position.x, position.y + heighPropValue, position.width, heighPropValue);
            Rect coolDownRect = new Rect(position.x, position.y + heighPropValue * 2, position.width, heighPropValue);
            Rect teamReact = new Rect(position.x, position.y + heighPropValue * 3, position.width, heighPropValue);
            Rect typeRect = new Rect(position.x, position.y + heighPropValue * 4, position.width, heighPropValue);

            EditorGUI.PropertyField(statusRect, statusEffect);
            EditorGUI.PropertyField(overrideRect, overrideValue);

            if (overrideValue.boolValue)
            {
                EditorGUI.PropertyField(coolDownRect, coolDown);
                EditorGUI.PropertyField(teamReact, team);
                EditorGUI.PropertyField(typeRect, type);
            }
        }

        private void Init(SerializedProperty property)
        {
            statusEffect = property.FindPropertyRelative("statusEffect");
            overrideValue = property.FindPropertyRelative("overrideThis");
            coolDown = property.FindPropertyRelative("coolDown");
            team = property.FindPropertyRelative("team");
            type = property.FindPropertyRelative("type");
            initialized = true;

        }
    }



}