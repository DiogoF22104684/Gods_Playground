using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using URandom = UnityEngine.Random;
using System;
using LibGameAI.FSMs;

public class DMBehavior : MonoBehaviour
{
    private bool temporaryBool;
    private StateMachine stateMachine;
 
    // Start is called before the first frame update
    void Start()
    {
        State neutralState = new State("Neutral",
        () => Debug.Log("Entering neutral state"),
        NeutralBehaviour,
        () => Debug.Log("Leaving neutral state"));

        State angryState = new State("Angry",
        () => Debug.Log("Entering angry state"),
        AngryBehaviour,
        () => Debug.Log("Leaving angry state"));

        State excitedState = new State("Returning",
        () => Debug.Log("Entering returning state"),
        ExcitedBehaviour,
        () => Debug.Log("Leaving returning state"));
        
        State annoyedState = new State("Returning",
        () => Debug.Log("Entering returning state"),
        AnnoyedBehaviour,
        () => Debug.Log("Leaving returning state"));
        
        State hopefulState = new State("Returning",
        () => Debug.Log("Entering returning state"),
        HopefulBehaviour,
        () => Debug.Log("Leaving returning state"));

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
        
    }
    private void AngryBehaviour()
    {

    }
    private void ExcitedBehaviour()
    {

    }
    private void AnnoyedBehaviour()
    {

    }
    private void HopefulBehaviour()
    {

    }
}
