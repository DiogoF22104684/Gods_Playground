using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Linq;

namespace DialogueSystem.Editor
{
    [CustomEditor(typeof(DialogueNodeInspector))]
    public class DialogueNodeInspectorEditor : UnityEditor.Editor
    {
        SerializedProperty dialogueText;
        SerializedProperty events;
        DialogueNodeInspector nodeInsp;

        int funcSelectPopup;

        void OnEnable()
        {
            dialogueText = serializedObject.FindProperty("dialogueText");
            events = serializedObject.FindProperty("events");
        }

        public override void OnInspectorGUI()
        {           
            nodeInsp = target as DialogueNodeInspector;

            serializedObject.Update();

            base.OnInspectorGUI();

            DialogueController controller = nodeInsp.Container?.Controller;
            string dialTxtTemp = dialogueText.stringValue; 
            EditorGUILayout.PropertyField(dialogueText, new GUIContent("Dialogue Text"));
            if (dialTxtTemp != dialogueText.stringValue)
                nodeInsp.ChangeDialogue(dialogueText.stringValue);

            GUILayout.BeginHorizontal();
          

            GUILayout.EndHorizontal();

            GUILayout.Space(5);

            EditorGUI.indentLevel = 2;

            
          
            if (controller != null)
            {
                GUILayout.Space(10);

                GUILayout.Label(controller.name);

                GUILayout.Space(10);

                if (GUILayout.Button("Add Event"))
                {
                    controller.Events.Add(
                        new EventDefinition(nodeInsp.Script, nodeInsp.id, 0, "None","None")
                        ); 
                }

                if (GUILayout.Button("Clear"))
                {
                    for (int i = events.arraySize - 1; i >= 0; i--)
                    {
                        RemoveEventFromManager(i);
                    }


                    events.ClearArray();
                }

                EventDefinition[] def = 
                    controller.Events
                    .Where(x=>x.Script == nodeInsp.Script).ToArray()
                    .Where(x => x.NodeId == nodeInsp.id).ToArray();

                EditorUtility.SetDirty(controller);

                for (int i = 0; i < def.Length; i++)
                {
                    string newId = def[i].NodeId;
                    int index = def[i].LetterIndex;
                    
                    GUILayout.Space(10);

                    #region Component Type PopUp
                    GameObject selectedObj = nodeInsp.Container.gameObject;
                    
                    Component[] components = selectedObj.GetComponents(typeof(Component));
                    List<System.Type> listType = new List<Type> { };

                    int methodInt = 0;
                    int componentInt = 0;
                    
                    foreach (Component comp in components)
                    {
                        listType.Add(comp.GetType());
                    }

                    if (listType.Count > 0)
                    {
                        List<string> typeNameList = listType.Select(x => x.FullName).ToList();
                        componentInt = typeNameList.IndexOf(def[i].ComponentName);
                        if (componentInt < 0) componentInt = 0;
                        componentInt =
                            EditorGUILayout.Popup(new GUIContent("Object Components"), componentInt, typeNameList.ToArray());
                        def[i].ComponentName = typeNameList[componentInt];
                        //assembly = listType[componentInt].Assembly;
                    }
                    #endregion

                    #region Method PopUp
                    if (listType.Count > 0)
                    {
                        string assemblyName = listType[componentInt].Assembly.FullName;
                        string type = listType[componentInt].FullName;
                        string qualifiedName = Assembly.CreateQualifiedName(assemblyName, type);
                        System.Type typeSelected = Type.GetType(qualifiedName);

                        GUILayout.Label(typeSelected?.Name ?? "Null");

                        List<MethodInfo> methodInfo = typeSelected.GetMethods().ToList();
                        
                        List<string> methodNames = methodInfo.Select(x => x.Name).ToList();
                        methodInt = methodNames.IndexOf(def[i].MethodName);
                        if (methodInt < 0) methodInt = 0;

                        methodInt =
                            EditorGUILayout.Popup(new GUIContent("Methods"), methodInt, methodNames.ToArray());
                        
                        def[i].MethodName = methodNames[methodInt];
                    }
                    #endregion

                    #region Show Text Toggle
                    def[i].hidden = EditorGUILayout.Toggle(def[i].hidden);
                    if (def[i].hidden)
                        DrawTriggerLabel(index, dialogueText.stringValue);
                   
                    #endregion
                   
                    #region Selected Letter Index Trigger
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(28);
                    GUILayout.Label("Index");

                    def[i].LetterIndex = (int)EditorGUILayout.Slider(def[i].LetterIndex,
                        0, dialogueText.stringValue.Length - 1);

                    GUILayout.EndHorizontal();
                    #endregion


                    //def[i] = new EventDefinition("", newId, index, component, method);
                }
            }
            else
            {
                GUILayout.Space(10);
                GUILayout.Label("Scripts needs to be in controller to access Event System.");
            }

            EditorGUI.indentLevel = 1;
            
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawTriggerLabel(int intValue, string text)
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            
            for (int i = 0; i < text.Length; i++)
            {
                if (i % 25 == 0)
                {
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }
   
                if (intValue == i)
                {
                    GUIStyle style = new GUIStyle();
                    style.normal.textColor = Color.red;
                    style.fontSize = 20;
                    GUILayout.Label(text[i].ToString(), style);
                }
                else
                {
                    GUILayout.Label(text[i].ToString());
                }

             
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
     
        
        private void RemoveEventFromManager(int index)
        {
            SerializedProperty gameObj =
                   events.GetArrayElementAtIndex(index).FindPropertyRelative("gameObj");
            SerializedProperty uniqueID =
                  events.GetArrayElementAtIndex(index).FindPropertyRelative("uniqueID");
            SerializedProperty savedID =
              events.GetArrayElementAtIndex(index).FindPropertyRelative("savedID");

            GameObject oldObj = gameObj.objectReferenceValue as GameObject;

            //Deselect Old Object
            if (oldObj != null)
            {
                //Compare the objects in the wating list
                if (uniqueID.stringValue ==
                    savedID.stringValue)
                {
                    //Object Changed from One In Used
                    nodeInsp.SaveWaitingList.WaitListToDelete.Add(oldObj);
                }
                else
                {
                    //Object Changed from one who is not In use
                    nodeInsp.SaveWaitingList.WaitListToAdd.Remove(oldObj);
                }
                //Remove The object equal to this one 
                //nodeInsp.SaveWaitingList.WaitListToAdd.Remove(oldObj);
            }
        }

    }
}
