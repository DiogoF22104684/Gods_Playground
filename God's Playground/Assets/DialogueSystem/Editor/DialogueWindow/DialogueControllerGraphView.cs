using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;

namespace DialogueSystem.Editor 
{
    public class DialogueControllerGraphView : GraphView
    {


        private DialogueContainer container;

        /// <summary>
        /// Constructor of this class
        /// </summary>
        public DialogueControllerGraphView(DialogueContainer container)
        {
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());


            this.container = container;
            //For Now
            foreach (DialogueScript script in container.Controller.Scripts)
            {
                AddNode(script);
            }


        }

        private void AddNode(DialogueScript script)
        {
            Node node = new Node();
            node.name = script.DialogueName;
           
            Label nameLabel = new Label(node.name);
            Button openDialogue = new Button
            (
                () => { DialogueGraph.OpenDialogueGraphWindow(script, container); }
            );
            openDialogue.text = "Open Dialogue";
  
            node.titleContainer.Clear();
           
            node.titleContainer.Add(nameLabel);
            node.titleContainer.Add(openDialogue);
           


            

            node.RefreshExpandedState();
            node.RefreshPorts();
           

            AddElement(node);


       
        }
    }
}
