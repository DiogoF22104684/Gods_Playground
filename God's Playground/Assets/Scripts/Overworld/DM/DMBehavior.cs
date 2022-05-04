using UnityEngine;
using System;
using LibGameAI.FSMs;

public class DMBehavior : MonoBehaviour
{
    private bool temporaryBool;
    private StateMachine stateMachine;
    private float timeLeft = 1f;
    private Color oldColor;
    private Color newColor;
    private Renderer dm_renderer;
    private GameObject leftHand;
    private GameObject rightHand;
 
    // Start is called before the first frame update
    void Start()
    {
        dm_renderer = GetComponent<Renderer>();
        leftHand = gameObject.transform.GetChild(0).gameObject;
        rightHand = gameObject.transform.GetChild(1).gameObject;

        State neutralState = new State("Neutral",
        () => Debug.Log("Entering neutral state"),
        NeutralBehaviour,
        () => Debug.Log("Leaving neutral state"));

        State angryState = new State("Angry",
        () => Debug.Log("Entering angry state"),
        AngryBehaviour,
        () => Debug.Log("Leaving angry state"));

        State excitedState = new State("Excited",
        () => Debug.Log("Entering excited state"),
        ExcitedBehaviour,
        () => Debug.Log("Leaving excited state"));
        
        State annoyedState = new State("Annoyed",
        () => Debug.Log("Entering annoyed state"),
        AnnoyedBehaviour,
        () => Debug.Log("Leaving annoyed state"));
        
        State hopefulState = new State("Hopeful",
        () => Debug.Log("Entering hopeful state"),
        HopefulBehaviour,
        () => Debug.Log("Leaving hopeful state"));

        //--------------------------To Neutral State--------------------------
        angryState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad0), //if none of the variables reach the treshholds
                () => Debug.Log("Ah yes, I'm neutral"),
                neutralState));
                
        excitedState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad0), //if none of the variables reach the treshholds
                () => Debug.Log("Ah yes, I'm neutral"),
                neutralState));

        annoyedState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad0), //if none of the variables reach the treshholds
                () => Debug.Log("Ah yes, I'm neutral"),
                neutralState));

        hopefulState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad0), //if none of the variables reach the treshholds
                () => Debug.Log("Ah yes, I'm neutral"),
                neutralState));

        //--------------------------To Angry State--------------------------
        neutralState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad1), //if happiness is negative and below a certain value
                () => Debug.Log("Ah yes, I'm angry"),
                angryState));
                
        excitedState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad1), //if happiness is negative and below a certain value
                () => Debug.Log("Ah yes, I'm angry"),
                angryState));

        annoyedState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad1), //if happiness is negative and below a certain value
                () => Debug.Log("Ah yes, I'm angry"),
                angryState));

        hopefulState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad1), //if happiness is negative and below a certain value
                () => Debug.Log("Ah yes, I'm angry"),
                angryState));

        //--------------------------To Excited State--------------------------
        neutralState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad2), //if happiness is positive and above a certain value
                () => Debug.Log("Ah yes, I'm excited"),
                excitedState));
                
        angryState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad2), //if happiness is positive and above a certain value
                () => Debug.Log("Ah yes, I'm excited"),
                excitedState));

        annoyedState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad2), //if happiness is positive and above a certain value
                () => Debug.Log("Ah yes, I'm excited"),
                excitedState));

        hopefulState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad2), //if happiness is positive and above a certain value
                () => Debug.Log("Ah yes, I'm excited"),
                excitedState));

        //--------------------------To Annoyed State--------------------------
        neutralState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad3), //if annoyance is negative and below a certain value
                () => Debug.Log("Ah yes, I'm annoyed"),
                annoyedState));
                
        angryState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad3), //if annoyance is negative and below a certain value
                () => Debug.Log("Ah yes, I'm annoyed"),
                annoyedState));

        excitedState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad3), //if annoyance is negative and below a certain value
                () => Debug.Log("Ah yes, I'm annoyed"),
                annoyedState));

        hopefulState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad3), //if annoyance is negative and below a certain value
                () => Debug.Log("Ah yes, I'm annoyed"),
                annoyedState));

        //--------------------------To Hopeful State--------------------------
        neutralState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad4), //if annoyance is positive and above a certain value
                () => Debug.Log("Ah yes, I'm hopeful"),
                hopefulState));
                
        angryState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad4), //if annoyance is positive and above a certain value
                () => Debug.Log("Ah yes, I'm hopeful"),
                hopefulState));

        excitedState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad4), //if annoyance is positive and above a certain value
                () => Debug.Log("Ah yes, I'm hopeful"),
                hopefulState));

        annoyedState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad4), //if annoyance is positive and above a certain value
                () => Debug.Log("Ah yes, I'm hopeful"),
                hopefulState));


        stateMachine = new StateMachine(neutralState);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Action actions = stateMachine.Update();
        actions?.Invoke();
    }

    private void NeutralBehaviour()
    {
        newColor = new Color(1,1,1);
        dm_renderer.material.color = Color.Lerp(dm_renderer.material.color, newColor, Time.deltaTime * timeLeft);
    }
    private void AngryBehaviour()
    {
        newColor = new Color(216f/255f,32f/255f,32f/255f);
        dm_renderer.material.color = Color.Lerp(dm_renderer.material.color, newColor, Time.deltaTime * timeLeft);
    }
    private void ExcitedBehaviour()
    {
        newColor = new Color(216f/255f,216f/255f,32f/255f);
        dm_renderer.material.color = Color.Lerp(dm_renderer.material.color, newColor, Time.deltaTime * timeLeft);
    }
    private void AnnoyedBehaviour()
    {
        newColor = new Color(32f/255f,56f/255f,92f/255f);
        dm_renderer.material.color = Color.Lerp(dm_renderer.material.color, newColor, Time.deltaTime * timeLeft);
    }
    private void HopefulBehaviour()
    {
        newColor = new Color(64f/255f,216f/255f,92f/255f);
        dm_renderer.material.color = Color.Lerp(dm_renderer.material.color, newColor, Time.deltaTime * timeLeft);
    }

}
