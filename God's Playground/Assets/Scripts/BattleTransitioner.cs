using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BattleTransitioner : MonoBehaviour
{

    private int id;
    

    [SerializeField]
    private BattleConfigData battleConfig;
    
    [SerializeField]
    private bool debug;
    [SerializeField]
    private string sceneName;

    [SerializeField]
    List<EnemiesTemplate> enemies;

    [SerializeField]
    private PlayableDirector transition;

    [SerializeField]
    private ScriptableBool midCutscene;

    public int Id { get => id; set => id = value; }
    public PlayableDirector Transition { get => transition; set => transition = value; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                EnterBattle();
            }   
        }
    }

    public void EnterBattle()
    {
        //Change Battle Config
        battleConfig.SetupConfig(transform.position,enemies, this);
        //Enter Battle
        midCutscene.Value = true;
        Transition.Play();
    }
}
