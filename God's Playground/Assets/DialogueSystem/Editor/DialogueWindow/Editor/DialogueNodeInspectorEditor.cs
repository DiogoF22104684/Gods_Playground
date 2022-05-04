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
                    .Where(x => x.NodeId == nodeInsp.id).ToArray();

                EditorUtility.SetDirty(controller);

                DialogueScript[] s = controller.Events.Select(x => x.Script).ToArray();


                for (int i = 0; i < def.Length; i++)
                {
 
                    string newId = def[i].NodeId;
                    int index = def[i].LetterIndex;
                    
                    GUILayout.Space(10);

                    GUILayout.BeginHorizontal();
                    float defaultLabelWidth = EditorGUIUtility.labelWidth;
                    EditorGUIUtility.labelWidth = 100;
                    
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
                            EditorGUILayout.Popup(new GUIContent("Component"), componentInt, typeNameList.ToArray());
                        def[i].ComponentName = typeNameList[componentInt];
                        //assembly = listType[componentInt].Assembly;                      
                    }
                    #endregion
                    
                    GUILayout.Space(1);
                    
                    #region Method PopUp
                    if (listType.Count > 0)
                    {
                        string assemblyName = listType[componentInt].Assembly.FullName;
                        string type = listType[componentInt].FullName;
                        string qualifiedName = Assembly.CreateQualifiedName(assemblyName, type);
                        System.Type typeSelected = Type.GetType(qualifiedName);

                       
                        List<MethodInfo> methodInfo = typeSelected.GetMethods().ToList();
                        


                        List<string> methodNames = methodInfo.Select(x => x.Name).ToList();
                        methodInt = methodNames.IndexOf(def[i].MethodName);
                        if (methodInt < 0) methodInt = 0;

                        methodInt =
                            EditorGUILayout.Popup(new GUIContent("Method"), methodInt, methodNames.ToArray());
                        

                        def[i].MethodName = methodNames[methodInt];
                        def[i].MethodInfo = new SerializableMethodInfo(methodInfo[methodInt]);

                        GUILayout.EndHorizontal();
                        EditorGUIUtility.labelWidth = defaultLabelWidth;
                        
                        ParameterInfo[] paramsInfo = methodInfo[methodInt].GetParameters();

                        if (def[i].Parameters == null) { def[i].Parameters = new List<ParamsContainer> { }; };
                       
                        if (def[i].Parameters.Count == 0)
                        {
                            ParamsContainer parm = 
                                new ParamsContainer(methodInfo[methodInt].Name, typeof(string));
                            def[i].Parameters.Add(parm);

                            foreach (ParameterInfo info in paramsInfo)
                            {
                                ParamsContainer parm1 = new ParamsContainer(info.GetType().GetDefault(), info.GetType());
                                def[i].Parameters.Add(parm1);
                            }
                        }
                        else
                        {
                            if (def[i].Parameters[0].String1 != methodInfo[methodInt].Name)
                            {
                                def[i].Parameters.Clear();
                                ParamsContainer parm = 
                                    new ParamsContainer(methodInfo[methodInt].Name, typeof(string));
                                def[i].Parameters.Add(parm);
                                foreach (ParameterInfo info in paramsInfo)
                                {
                                    ParamsContainer parm1 = new ParamsContainer(info.GetType().GetDefault(), info.GetType());
                                    def[i].Parameters.Add(parm1);
                                }
                            }
                            else
                            {
                                
                                for (int i1 = 0; i1 < paramsInfo.Length; i1++)
                                {                               
                                    ParameterInfo info = paramsInfo[i1];
                                   ParamsContainer parm1 = 
                                        new ParamsContainer(
                                            CreateGenericField(info, def[i].Parameters[i1 + 1]), 
                                        info.ParameterType);
                                    def[i].Parameters[i1 + 1] = parm1;
                                }
                               
                            }
                        }
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

        private object CreateGenericField(ParameterInfo info, ParamsContainer value)
        {

           
            
            if (System.Type.GetType(value.QualifiedName).IsSubclassOf(typeof(UnityEngine.Object)))
            {
                GUILayout.Label("Object not implemented");
            }
            else if(value.Type == typeof(int).ToString())
            {
                try
                {
                    return EditorGUILayout.IntField(new GUIContent(info.Name), value.Int1);
                }
                catch
                {
                    return 0;
                }
            }
            else if (value.Type == typeof(string).ToString())
            {
                try
                {                    
                    return EditorGUILayout.TextField(new GUIContent(info.Name), value.String1);
                }
                catch
                {
                    return "";
                }
            }
            else if (value.Type == typeof(bool).ToString())
            {
                try
                {
                    return EditorGUILayout.Toggle(new GUIContent(info.Name), value.Bool1);
                }
                catch
                {
                    return false;
                }
                //GUILayout.Label("String not implemented");
            }
            else if (System.Type.GetType(value.QualifiedName).IsEnum)
            {
                string enumName = System.Type.GetType(value.QualifiedName).Name;
                List<string> enumValues = new List<string> { };
                Array what = System.Type.GetType(value.QualifiedName).GetEnumValues();
               
                for (int i = 0; i < what.Length; i++)
                {
                    enumValues.Add(what.GetValue(i).ToString());
                }
                return EditorGUILayout.Popup(new GUIContent(info.Name), value.Enum1,enumValues.ToArray());
                //System.Type.GetType(value.QualifiedName).GetEnumValues();

                GUILayout.Label("Enum not implemented");
                return default;
            }

            GUILayout.Label("Type not suported");
            return default;
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
