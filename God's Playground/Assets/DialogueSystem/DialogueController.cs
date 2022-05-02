using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem 
{
    [CreateAssetMenu(menuName = "Scriptables/´DialogueController")]
    public class DialogueController : ScriptableObject
    {
        [SerializeField]
        private string id;
        public string Id => id;


        [SerializeField]
        List<DialogueScript> scripts;
        public List<DialogueScript> Scripts => scripts;
 
        [SerializeField]
        List<EventDefinition> events;
        public List<EventDefinition> Events => events;


        //Scrips NodeId LetterIndex ComponentName MethodName

    }

    //REMIND SETDIRTY IS A THING PLEASE DEAR GOD
    [System.Serializable]
    public class EventDefinition
    {
        [SerializeField]
        DialogueScript script;
        [SerializeField]
        string nodeId;
        [SerializeField]
        int letterIndex;
        [SerializeField]
        string componentName;
        [SerializeField]
        string methodName;

        [HideInInspector]
        public bool hidden;

        public DialogueScript Script { get => script; set => script = value; }
        public string NodeId { get => nodeId; set => nodeId = value; }
        public int LetterIndex { get => letterIndex; set => letterIndex = value; }
        public string ComponentName { get => componentName; set => componentName = value; }
        public string MethodName { get => methodName; set => methodName = value; }

        public EventDefinition(DialogueScript script, string nodeId, int letterIndex, string componentName, string methodName)
        {
            hidden = false;
            this.script = script;
            this.nodeId = nodeId;
            this.letterIndex = letterIndex;
            this.componentName = componentName;
            this.methodName = methodName;
        }


    }
}