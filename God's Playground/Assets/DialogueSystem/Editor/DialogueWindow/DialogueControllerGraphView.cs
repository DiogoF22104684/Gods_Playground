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
            if (container?.Controller == null) return;
            for (int i = 0; i < container.Controller.Scripts.Count; i++)
            {
                DialogueScript script = (DialogueScript)container.Controller.Scripts[i];
                AddNode(script, i);
            }


        }

        private void AddNode(DialogueScript script, int index)
        {
            Node node = new Node();
            node.name = script.DialogueName;
           
            Label nameLabel = new Label(node.name);
            Button openDialogue = new Button
            (
                () => { DialogueGraph.OpenDialogueGraphWindow(script, container); }
            );
            openDialogue.text = "Open Dialogue";

            int offset = 10;
            node.SetPosition(new Rect(offset, offset + (50 * index),0,0));

            node.titleContainer.Clear();
           
            node.titleContainer.Add(nameLabel);
            node.titleContainer.Add(openDialogue);
           
            

            

            node.RefreshExpandedState();
            node.RefreshPorts();
           

            AddElement(node);


       
        }
    }
}
