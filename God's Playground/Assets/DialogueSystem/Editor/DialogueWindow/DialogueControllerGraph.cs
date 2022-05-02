using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueSystem.Editor
{
    public class DialogueControllerGraph : EditorWindow
    {

        /// <summary>
        /// GraphView component of this window
        /// </summary>
        private DialogueControllerGraphView graphview;

        /// <summary>
        /// Method called when this script is enabled
        /// </summary>
        private void OnEnable()
        {
            Selection.selectionChanged += what;
            what();
            //CreateToolbar();
        }

        private void OnDisable()
        {
            Selection.selectionChanged -= what;
        }

        private void what()
        {
            rootVisualElement.Clear();
            GameObject obj = Selection.activeGameObject;
            //Debug.Log(Selection.activeObject);
            if (obj != null)
            {
                if (obj.GetComponent<DialogueContainer>())
                {
                    CreateGraphView(obj.GetComponent<DialogueContainer>());
                }
                else
                {
                    //Create button 
                    Button controllerCreateButton = new Button(clickEvent: () =>
                    {
                        Debug.Log("Boop");
                    });

                    //Add text to buttons
                    controllerCreateButton.text = "Create new Dialogue Controller";

                    rootVisualElement.Add(controllerCreateButton);
                }
            }
            else
            {

            }
        }

        /// <summary>
        /// Creates and adds the GraphView component of this window 
        /// </summary>
        private void CreateGraphView(DialogueContainer container)
        {

            graphview = new DialogueControllerGraphView(container)
            {
                name = "Dialogue Graph"
            };

            graphview.StretchToParentSize();
            rootVisualElement.Add(graphview);
        }

        /// <summary>
        /// Method responsible for opening the Dialogue Window 
        /// where the node system is placed
        /// </summary>
        [MenuItem("Dialogue/Dialogue Controller")]
        public static void OpenDialogueGraphWindow()
        {
            DialogueControllerGraph window = GetWindow<DialogueControllerGraph>();
            window.titleContent = new GUIContent(text: "Controller");

        }


    }
}