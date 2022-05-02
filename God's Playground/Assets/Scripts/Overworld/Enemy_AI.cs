using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using URandom = UnityEngine.Random;
using System;
using LibGameAI.FSMs;

public class Enemy_AI : MonoBehaviour
{
    private bool playerInProximity = false;
    [SerializeField] private Spawn_Area_script patrolZone;
    private Player_Control player;
    private StateMachine stateMachine;
    private NavMeshAgent agent;

    
    private BattleTransitioner transitioner;

    // Start is called before the first frame update
    void Start()
    {
        transitioner = GetComponent<BattleTransitioner>();
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
                () => playerInProximity,
                () => Debug.Log("I'm coming for you"),
                chaseState));
        
        chaseState.AddTransition(
            new Transition (
                () => !playerInProximity,
                () => Debug.Log("I'm going back"),
                returningState));
        
        returningState.AddTransition(
            new Transition (
                () => playerInProximity,
                () => Debug.Log("You dare to make a fool of me!"),
                chaseState));

        returningState.AddTransition(
            new Transition (
                () => agent.remainingDistance <= 0.1,
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
        agent.SetDestination(player.gameObject.transform.position);
    }
    private void ReturningToSpawn()
    {
        agent.SetDestination(patrolZone.gameObject.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            playerInProximity = true;
            transitioner.EnterBattle();
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject == player.gameObject)
        {
            //Colocar aqui a transição e passagem entre cenas
        }
    }
}
