using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using URandom = UnityEngine.Random;
using System;
using LibGameAI.FSMs;

public class Enemy_AI : MonoBehaviour
{
    [SerializeField] private float minDistanceToPlayer = 10f;
    [SerializeField] private Spawn_Area_script patrolZone;

    private Player_Control player;
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private Vector3 dest = Vector3.zero;

    
    private BattleTransitioner transitioner;

    public Spawn_Area_script PatrolZone { get => patrolZone; set => patrolZone = value; }

    // Start is called before the first frame update
    void Start()
    {
        transitioner = GetComponent<BattleTransitioner>();
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindObjectOfType<Player_Control>();
        
    // ---------------------State Machine States---------------------
        State idleState = new State("Idle",
        null,
        IdleBehaviour,
        null);

        State chaseState = new State("Chasing",
        null,
        ChasingPlayer,
        null);

        State returningState = new State("Returning",
        null,
        ReturningToSpawn,
        null);
    
    // ---------------------State Machine transitions---------------------
        idleState.AddTransition(
            new Transition (
                () => (player.gameObject.transform.position - 
                        transform.position).magnitude < minDistanceToPlayer,
                null,
                chaseState));
        
        chaseState.AddTransition(
            new Transition (
                () => (player.gameObject.transform.position - 
                        transform.position).magnitude > minDistanceToPlayer,
                null,
                returningState));
        
        returningState.AddTransition(
            new Transition (
                () => (player.gameObject.transform.position - 
                        transform.position).magnitude < minDistanceToPlayer,
                null,
                chaseState));

        returningState.AddTransition(
            new Transition (
                () => agent.remainingDistance <= 0.1,
                null,
                idleState));

        stateMachine = new StateMachine(idleState);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null) return;
        Action actions = stateMachine.Update();
        actions?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            transitioner.EnterBattle();
        }
    }

    //---------------------State Machine Behaviors---------------------
    private void IdleBehaviour()
    {
        if(agent.remainingDistance >= 0.1f ) return;

        do{
            float rotationValue  = UnityEngine.Random.Range(0, 359);
            float rangeValue  = UnityEngine.Random.Range(2f, 5f);
            gameObject.transform.localEulerAngles = 
                gameObject.transform.localEulerAngles.y(rotationValue);
            Vector3 direction = gameObject.transform.forward.normalized;
            dest = transform.position + direction * rangeValue;
        }while(!PatrolZone.InArea(dest));
        

        agent.SetDestination(dest);
    }

    private void ChasingPlayer()
    {
        agent.transform.LookAt(player.gameObject.transform.position);
        agent.SetDestination(player.gameObject.transform.position);
    }
    private void ReturningToSpawn()
    {
        agent.transform.LookAt(PatrolZone.gameObject.transform.position);
        agent.SetDestination(PatrolZone.gameObject.transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, 
        dest);
    }

}
