using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using URandom = UnityEngine.Random;
using System;
using LibGameAI.FSMs;

public class Enemy_AI : MonoBehaviour
{
    private bool playerInZone = false;
    private bool playerInSight = false;
    [SerializeField] private Spawn_Area_script patrolZone;
    private Player_Control player;
    private StateMachine stateMachine;
    private NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindObjectOfType<Player_Control>();
        State idleState = new State("Idle",
        () => Debug.Log("Entering idle state"),
        IdleBehaviour,
        () => Debug.Log("Leaving idle state"));

        State chaseState = new State("Chasing",
        () => Debug.Log("Entering chasing state"),
        ChasingPlayer,
        () => Debug.Log("Entering chasing state"));

        State returningState = new State("Returning",
        () => Debug.Log("Entering returning state"),
        ReturningToSpawn,
        () => Debug.Log("Entering returning state"));
    
        idleState.AddTransition(
            new Transition (
                () => playerInZone && playerInSight, //Player in the outpost zone
                () => Debug.Log("I'm coming for you"),
                chaseState));
        
        chaseState.AddTransition(
            new Transition (
                () => !playerInZone, //Player out of the outpost zone
                () => Debug.Log("I'm going back"),
                returningState));
        
        returningState.AddTransition(
            new Transition (
                () => playerInZone, //Player enters the outpost zone again
                () => Debug.Log("You dare to make a fool of me!"),
                chaseState));

        returningState.AddTransition(
            new Transition (
                () => !playerInSight && !playerInZone, //Player did not return to the outpost zone
                () => Debug.Log("That person better be gone"),
                idleState));

        stateMachine = new StateMachine(idleState);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Action actions = stateMachine.Update();
        actions?.Invoke();
    }

    private void IdleBehaviour()
    {
        if(agent.remainingDistance >= 0.1f ) return;

        Vector3 dest = Vector3.zero;
        do{
        float rotationValue  = UnityEngine.Random.Range(0, 359);
        float rangeValue  = UnityEngine.Random.Range(0f, 1f);
        gameObject.transform.localEulerAngles = 
            gameObject.transform.localEulerAngles.y(rotationValue);
        Vector3 direction = gameObject.transform.forward.normalized;
        dest = transform.position + direction * rangeValue;
        }while(!patrolZone.InArea(dest));
        

        agent.SetDestination(dest);
    }

    private void ChasingPlayer()
    {

    }
    private void ReturningToSpawn()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
            playerInZone = true;
    }
}
