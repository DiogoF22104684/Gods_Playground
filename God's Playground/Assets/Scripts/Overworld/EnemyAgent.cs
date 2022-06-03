using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using LibGameAI.FSMs;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class EnemyAgent : MonoBehaviour, ISavable
{
    [SerializeField] private float minDistanceToPlayer = 10f;
    [SerializeField] private Spawn_Area_script patrolZone;

    private Player_Control player;
    private StateMachine stateMachine;
    private Vector3 dest = Vector3.zero;
    private NavMeshAgent agent;

    public Spawn_Area_script PatrolZone { get => patrolZone; set => patrolZone = value; }

    public bool IsAlive => data.States != States.Dead;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player_Control>();
        
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

        #region State Machine Trasitions
       
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
                () => false,
                null,
                idleState));

        #endregion

        stateMachine = new StateMachine(idleState);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        data.Position = transform.localPosition;
        if (player == null) return;
        Action actions = stateMachine.Update();
        actions?.Invoke();
    }

    #region State Machine Behaviors
   
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
        }while(!PatrolZone.InArea(dest) && PatrolZone.InArea(this.gameObject.transform.position));
        

        agent.SetDestination(dest);
    }

    private void ChasingPlayer()
    {
        //agent.transform.LookAt(player.gameObject.transform.position);
        agent.SetDestination(player.gameObject.transform.position);
    }
  
    private void ReturningToSpawn()
    {
        //agent.transform.LookAt(PatrolZone.gameObject.transform.position);
        agent.SetDestination(PatrolZone.gameObject.transform.position);
    }

    #endregion
 
    #region Load/Save
    public int ID { get => iD; set => iD = value; }

    [Header("Saving Settings")]
    [SerializeField]
    [ReadOnly]
    private int iD;

    [SerializeField][ReadOnly]
    private Data data;


    [Serializable]
    public class Data
    {
        [SerializeField]
        private Vector3 position;
        [SerializeField]
        private States states;

        public Vector3 Position { get => position; set => position = value; }
        public States States { get => states; set => states = value; }
    }

    public enum States
    {
        Alive,
        Dead
    }

   
    string ISavable.GetData()
    {
        data.Position = transform.localPosition;

        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            Formatting = Newtonsoft.Json.Formatting.None,
            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
        };

        return JToken.FromObject(data).ToString();
    }

    public void LoadData(string data)
    { 
        this.data = JsonConvert.DeserializeObject<Data>(data);
        transform.localPosition = this.data.Position;
        gameObject.SetActive(this.data.States == (States)0);
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,
        dest);
    }
}

