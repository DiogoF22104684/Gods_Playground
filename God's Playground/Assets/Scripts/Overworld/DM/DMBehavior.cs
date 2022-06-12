using UnityEngine;
using System;
using LibGameAI.FSMs;

public class DMBehavior : MonoBehaviour
{
    [SerializeField] public PlayerTemplate statsObject;

    private bool temporaryBool, slammed;
    private StateMachine stateMachine;
    private float timeLeft = 1f, modifierX, modifierZ = 10, hpSlamLoss = 0.5f;
    private Color oldColor, newColor;
    private Renderer dm_renderer;
    private Animator animator;
    private GameObject leftHand, rightHand;
    
    [SerializeField]
    private GameObject happyMask, sadMask, player, enemyPrefab;

    [SerializeField]
    private float yModifier;

    public BattleSlider something;

    // Start is called before the first frame update
    void Start()
    {
        dm_renderer = GetComponent<Renderer>();
        animator = GetComponent<Animator>();
        leftHand = gameObject.transform.GetChild(0).gameObject;
        rightHand = gameObject.transform.GetChild(1).gameObject;

        #region States

        State neutralState = new State("Neutral",
        () => Debug.Log("Entering neutral state"),
        NeutralBehaviour,
        () => Debug.Log("Leaving neutral state"));

        State angryState = new State("Angry",
        () => Debug.Log("Entering angry state"),
        AngryBehaviour,
        () => slammed = false);

        State excitedState = new State("Excited",
        () => Debug.Log("Entering excited state"),
        ExcitedBehaviour,
        () => Debug.Log("Leaving excited state"));
        
        State annoyedState = new State("Annoyed",
        () => Debug.Log("Entering annoyed state"),
        AnnoyedBehaviour,
        () => modifierX = 0);
        
        State hopefulState = new State("Hopeful",
        () => Debug.Log("Entering hopeful state"),
        HopefulBehaviour,
        () => Debug.Log("Leaving hopeful state"));
        
        #endregion

        #region State Transitions
        
        #region To Neutral
        
        angryState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad0), //if none of the variables reach the treshholds
                () => Debug.Log("I'm neutral"),
                neutralState));
                
        excitedState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad0), //if none of the variables reach the treshholds
                () => Debug.Log("I'm neutral"),
                neutralState));

        annoyedState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad0), //if none of the variables reach the treshholds
                () => Debug.Log("I'm neutral"),
                neutralState));

        hopefulState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad0), //if none of the variables reach the treshholds
                () => Debug.Log("I'm neutral"),
                neutralState));

        #endregion

        #region To Angry
        neutralState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad1), //if happiness is negative and below a certain value
                () => Debug.Log("I'm angry"),
                angryState));
                
        excitedState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad1), //if happiness is negative and below a certain value
                () => Debug.Log("I'm angry"),
                angryState));

        annoyedState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad1), //if happiness is negative and below a certain value
                () => Debug.Log("I'm angry"),
                angryState));

        hopefulState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad1), //if happiness is negative and below a certain value
                () => Debug.Log("I'm angry"),
                angryState));

        #endregion

        #region To Excited
        neutralState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad2), //if happiness is positive and above a certain value
                () => Debug.Log("I'm excited"),
                excitedState));
                
        angryState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad2), //if happiness is positive and above a certain value
                () => Debug.Log("I'm excited"),
                excitedState));

        annoyedState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad2), //if happiness is positive and above a certain value
                () => Debug.Log("I'm excited"),
                excitedState));

        hopefulState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad2), //if happiness is positive and above a certain value
                () => Debug.Log("I'm excited"),
                excitedState));

        #endregion
        
        #region To Annoyed
        neutralState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad3), //if annoyance is negative and below a certain value
                () => Debug.Log("I'm annoyed"),
                annoyedState));
                
        angryState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad3), //if annoyance is negative and below a certain value
                () => Debug.Log("I'm annoyed"),
                annoyedState));

        excitedState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad3), //if annoyance is negative and below a certain value
                () => Debug.Log("I'm annoyed"),
                annoyedState));

        hopefulState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad3), //if annoyance is negative and below a certain value
                () => Debug.Log("I'm annoyed"),
                annoyedState));

        #endregion

        #region To Hopeful
        neutralState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad4), //if annoyance is positive and above a certain value
                () => Debug.Log("I'm hopeful"),
                hopefulState));
                
        angryState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad4), //if annoyance is positive and above a certain value
                () => Debug.Log("I'm hopeful"),
                hopefulState));

        excitedState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad4), //if annoyance is positive and above a certain value
                () => Debug.Log("I'm hopeful"),
                hopefulState));

        annoyedState.AddTransition(
            new Transition (
                () => Input.GetKey(KeyCode.Keypad4), //if annoyance is positive and above a certain value
                () => Debug.Log("I'm hopeful"),
                hopefulState));
        #endregion
        
        #endregion

        stateMachine = new StateMachine(neutralState);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Action actions = stateMachine.Update();
        actions?.Invoke();
    }

    #region State Actions

    private void NeutralBehaviour()
    {
        happyMask.SetActive(false);
        sadMask.SetActive(false);

        newColor = new Color(1,1,1);
        dm_renderer.material.color = Color.Lerp(dm_renderer.material.color, 
                                                newColor, 
                                                Time.deltaTime * timeLeft);

    }
    private void AngryBehaviour()
    {
        happyMask.SetActive(false);
        sadMask.SetActive(true);


        while (slammed == false)
        {

            animator.Play("DM_Slam");
            
            /* something.ChangeValue("hp", hpSlamLoss); */
            /* something.Hp -= hpSlamLoss; */

           /*  something.hpBattleSlider.initStats(something.entityData.Hp); */

           /*  something.ChangeValue(hpSlamLoss);

            print("after:" + something.valueToChange); */

            /* statsObject.HP = statsObject.HP - hpSlamLoss; */

            slammed = true;
        }

        newColor = new Color(216f/255f,32f/255f,32f/255f);
        dm_renderer.material.color = Color.Lerp(dm_renderer.material.color, 
                                                newColor, 
                                                Time.deltaTime * timeLeft);
    }
    private void ExcitedBehaviour()
    {
        sadMask.SetActive(false);
        happyMask.SetActive(true);

        newColor = new Color(216f/255f,216f/255f,32f/255f);
        dm_renderer.material.color = Color.Lerp(dm_renderer.material.color, 
                                                newColor, 
                                                Time.deltaTime * timeLeft);
    }
    private void AnnoyedBehaviour()
    {
        happyMask.SetActive(false);
        sadMask.SetActive(true);
        
        while (modifierX < 10)
        {
        EnemyInstantiation(modifierX, modifierZ);
        modifierX += 2;
        }

        newColor = new Color(32f/255f,56f/255f,92f/255f);
        dm_renderer.material.color = Color.Lerp(dm_renderer.material.color, 
                                                newColor, 
                                                Time.deltaTime * timeLeft);
    }
    private void HopefulBehaviour()
    {
        sadMask.SetActive(false);
        happyMask.SetActive(true);
        
        newColor = new Color(64f/255f,216f/255f,92f/255f);
        dm_renderer.material.color = Color.Lerp(dm_renderer.material.color, 
                                                newColor, 
                                                Time.deltaTime * timeLeft);
    }

    #endregion

    private void EnemyInstantiation(float x, float z)
    {
        Instantiate(enemyPrefab, 
                    new Vector3(player.transform.position.x + x,
                                yModifier,
                                player.transform.position.z + z), 
                                Quaternion.identity);
    }
}
