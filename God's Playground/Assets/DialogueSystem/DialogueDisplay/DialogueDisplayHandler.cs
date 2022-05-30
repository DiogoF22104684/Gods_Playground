using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DialogueSystem;
using TMPro;
using System.Linq;
using System.Collections.Generic;

//Class responsible for the handling of the Dialogue Display
public class DialogueDisplayHandler : MonoBehaviour
{
    /// <summary>
    /// Key used to pass from a line of dialogue to the next
    /// </summary>
    [SerializeField]
    private KeyCode passDialogueKey = default;

    private GameObject content;

    /// <summary>
    /// Text component responsible for displaying the Dialogue text
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI dialogueDisplayTarget = default;

    private DialogueContainer currentContainer;
    private DialogueController currentController;
    public DialogueController CurrentController => currentController;

    /// <summary>
    /// Current Dialogue script beeing displayed 
    /// </summary>
    [SerializeField]
    private DialogueScript currentScript = default;


    /// <summary>
    /// Time between each char of the Dialogue
    /// </summary>
    [SerializeField]
    private float displaySpeed = default;


    /// <summary>
    /// GameObject that defines the container of the ChoiceButtons
    /// </summary>
    [SerializeField]
    private GameObject buttonLayout = default;

    /// <summary>
    /// Prefab of the ChoiceButton
    /// </summary>
    [SerializeField]
    private GameObject buttonPREFAB = default;


    /// <summary>
    /// Variable that defines the data of one line of dialogue
    /// </summary>
    private NodeData dialogueLine;


    /// <summary>
    /// Text component of the current line of dialogue
    /// </summary>
    private string dialogueText;


    private List<ChoiceSelector> choices;

    /// <summary>
    /// Auxiliar vairable that represents the time between each char 
    /// in one line of dialogue
    /// </summary>
    private WaitForSeconds effectSpeed;


    /// <summary>
    /// Variable that defines if tha Dialogue has ended
    /// </summary>
    private bool ended;


    /// <summary>
    /// Variable that defines if the current line of dialogue is 
    /// currenly beeing displayed
    /// </summary>
    private bool inDialogue; 
    public bool InDialogue => inDialogue;

    public DialogueContainer CurrentContainer { get => currentContainer; set => currentContainer = value; }

    [SerializeField]
    private bool playOnLoad = default;

    public System.Action<DialogueScript> onStartDialogue;
    public System.Action<NodeData> onStartLine;
    public System.Action onEndDialogue;
    public System.Action onEndLine;

    private WaitForSeconds endDelay = new WaitForSeconds(0.01f);

    private int chosenOp;

    private EntityData presetData;

    private void Start()
    {
        presetData = Resources.Load<EntityData>("EntityData");
        choices = new List<ChoiceSelector> { };
        content = transform.GetChild(0).gameObject;
        if (playOnLoad)
            StartDialogue(currentScript, null);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (currentScript == null) return;

        if (ended)
        { 
            if (dialogueLine.OutPorts.Count > 1)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    choices[chosenOp].Dehighlight();
                    chosenOp--;
                    if (chosenOp < 0)
                        chosenOp = dialogueLine.OutPorts.Count - 1;
                    choices[chosenOp].Highlight();
                }
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    choices[chosenOp].Dehighlight();
                    chosenOp++;
                    if (chosenOp > dialogueLine.OutPorts.Count - 1)
                        chosenOp = 0;
                    choices[chosenOp].Highlight();
                }

                if (Input.GetKeyDown(passDialogueKey))
                {
                    StartCoroutine("DelayNextChoice");
                }
            }
        }
    
        if (Input.GetKeyDown(passDialogueKey))
        {
            if (content.activeSelf == false) return;
            if (!inDialogue) return;

            if (ended)
            {
                if (dialogueLine.OutPorts.Count > 1)
                {
                    
                    if (dialogueLine.OutPorts[0].ChoiceText != "") return;
                    
                }
                
                NextLine(0);
            }
            else
            {              
                dialogueDisplayTarget.text += dialogueText;
                dialogueText = "";
                buttonLayout.SetActive(true);
                ended = true;
                StopCoroutine("TypeWriterEffect");
            }          
        }
      
    }

    /// <summary>
    /// Method responsible for switching to the passed DialogueScript
    /// </summary>
    /// <param name="script">Dialogue Script to inicialize</param>
    public void StartDialogue(DialogueScript script, DialogueContainer container)
    {        
        currentScript = script;
        

        currentContainer = container;
        currentController = container?.Controller;

        content.SetActive(true);
        onStartDialogue?.Invoke(currentScript);
        PrepareNewDialogue();
    }


    /// <summary>
    /// Method responsible for seting up the Dialogue
    /// </summary>
    public void PrepareNewDialogue()
    {
        //Display Specificationss
        effectSpeed = new WaitForSeconds(displaySpeed);

        //Initialize First Line
        dialogueLine = currentScript[0];
        dialogueText = currentScript[0].Dialogue;

        //Handle button Layout
        InstatiateChoices();
        DisplayLine();
    }


    /// <summary>
    /// Method responsible for selecting and instantiating the respective 
    /// Choice Buttons of the current Dialogue 
    /// </summary>
    private void InstatiateChoices()
    {
        choices.Clear();
        
        

        foreach (Transform g in buttonLayout.transform)
        {
            Destroy(g.gameObject);
        }

        int choiceNumb = dialogueLine.OutPorts.Count;
        if (choiceNumb == 0) return;

        //Dumb and Hardcoded
        if(choiceNumb > 2)
        {
            buttonLayout.transform.localPosition = 
                new Vector3(buttonLayout.transform.localPosition.x, -60);
        }
        else
        {
            buttonLayout.transform.localPosition =
                new Vector3(buttonLayout.transform.localPosition.x, -133);
        }

        for (int i = 0; i < choiceNumb; i++)
        {
            
            GameObject temp = Instantiate(buttonPREFAB, transform.position,
                Quaternion.identity, buttonLayout.transform);


            ChoiceSelector cs = temp.GetComponent<ChoiceSelector>();
            cs.ChangeChoiceText(dialogueLine.OutPorts[i].ChoiceText);
            cs.ChoiceNumb = i;
            cs.NextLine = NextLine;
            choices.Add(cs);
        }

        buttonLayout.SetActive(false);
        chosenOp = 0;
        choices[0].Highlight();
    }


    /// <summary>
    /// Method responsible for deciding initializing next line of the current 
    /// DialogueScript
    /// </summary>
    /// <param name="choice">The selected choice of the current line</param>
    public void NextLine(int choice)
    {

        dialogueLine =
               currentScript.GetNextNode(dialogueLine, choice);

        if (dialogueLine == null)
        {
            EndDialogue();
            return;
        }

        dialogueText = dialogueLine.Dialogue;

       

        InstatiateChoices();
        DisplayLine();
    }


    /// <summary>
    /// Method responsible for ending the current DialogueScript
    /// </summary>
    private void EndDialogue()
    {
        StartCoroutine("EndDialogueDelay");
        currentScript = null;
        //content.SetActive(false);               
        dialogueDisplayTarget.text = "";
        StopCoroutine("TypeWriterEffect");

    }

    

    /// <summary>
    /// Method that starts the next line in the Dialogue
    /// </summary>
    private void DisplayLine()
    {

       
        //DUMB AND HARCODED
        //if (presetData[dialogueLine.PresetName].EntityName == "Default")
        //{
        //    dialogueDisplayTarget.gameObject.transform.localPosition =
        //        new Vector3(
        //            0,
        //            dialogueDisplayTarget.gameObject.transform.localPosition.y,
        //            dialogueDisplayTarget.gameObject.transform.localPosition.z);
        //    RectTransform rectT = dialogueDisplayTarget.gameObject.GetComponent<RectTransform>();

        //    rectT.sizeDelta =
        //        new Vector2(
        //            5910.446f,
        //            rectT.sizeDelta.y);
        //}
        //else
        //{
        //    dialogueDisplayTarget.gameObject.transform.localPosition =
        //       new Vector3(
        //           -192.3f,
        //           dialogueDisplayTarget.gameObject.transform.localPosition.y,
        //           dialogueDisplayTarget.gameObject.transform.localPosition.z);
        //    RectTransform rectT = dialogueDisplayTarget.gameObject.GetComponent<RectTransform>();

        //    rectT.sizeDelta =
        //        new Vector2(
        //            4628.585f,
        //            rectT.sizeDelta.y);
        //}

        onStartLine?.Invoke(dialogueLine);
        StopCoroutine("TypeWriterEffect");
        StartCoroutine("TypeWriterEffect");
    }


    /// <summary>
    /// IEnumerator that creates a TypeWriteEffect
    /// </summary>
    /// <returns></returns>
    IEnumerator TypeWriterEffect()
    {
        inDialogue = true;
        ended = false;
        dialogueDisplayTarget.text = "";
        int index = 0;

        while (dialogueText.Length > 0)
        {
            yield return effectSpeed;
                       
            index++;
            if (currentController != null)
            {
                EventDefinition[] inNodes =
                    currentController.Events.Where(x => x.NodeId == dialogueLine.GUID).ToArray();

                foreach (EventDefinition def in inNodes)
                {
                    if (def.LetterIndex == index)
                    {
                        MonoBehaviour obj =
                            currentContainer.gameObject.GetComponent(def.ComponentName) as MonoBehaviour;


                        if (def.Parameters != null)
                        {
                            if (def.Parameters.Count != 0)
                            {

                                List<object> parms = def.Parameters.Select(x => x.GetValue()).ToList();
                                parms.RemoveAt(0);
                                
                                def.MethodInfo.methodInfo.Invoke(obj, parms.ToArray());
                            }
                            else
                            {
                                def.MethodInfo.methodInfo.Invoke(obj, null);
                            }
                        }
                        else
                        {
                            def.MethodInfo.methodInfo.Invoke(obj, null);
                        }
                        //obj.Invoke(def.MethodName, 0);                                                 
                        
                       
                    }
                }
            }

            //foreach (EventTriggerData data in dialogueLine.Events)
            //{                
            //    if (data.IndexPos == index)
            //    {
                    
            //        GameObject obj = DialogueEventManager.GetGameObject(data.UniqueID);
            //        MonoBehaviour mono = obj.GetComponent(data.SelectedComponent) as MonoBehaviour;
            //        mono.Invoke(data.FunctionName, 0);

            //        //DialogueUniqueId[] test = GameObject.FindObjectsOfType<DialogueUniqueId>();
            //        //GameObject currentObject = null;
                  
            //        //foreach(DialogueUniqueId d in test)
            //        //{
            //        //    if(d.UniqueID == data.UniqueID)
            //        //    {
            //        //        currentObject = d.gameObject;
                                                       
            //        //        MonoBehaviour obj = currentObject.GetComponent(data.SelectedComponent) as MonoBehaviour;
            //        //        obj.Invoke(data.FunctionName,0);
            //        //        break;
            //        //    }
                        
            //        //}
                
            //    }
            //}

            char nextChar = dialogueText[0];
            dialogueDisplayTarget.text += nextChar;
            dialogueText = dialogueText.Substring(1);
        }
        ended = true;
        onEndLine?.Invoke();
        buttonLayout.SetActive(true);
    }

    IEnumerator DelayNextChoice()
    {
        yield return endDelay;
        choices[chosenOp].SelectChoice();
    }

    IEnumerator EndDialogueDelay()
    {
        yield return endDelay;
        onEndDialogue?.Invoke();
        //content.GetComponent<Animator>().Play("DialogueExit");
        content.SetActive(false);
        inDialogue = false;
    }
}
