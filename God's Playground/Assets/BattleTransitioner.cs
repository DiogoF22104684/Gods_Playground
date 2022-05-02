using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BattleTransitioner : MonoBehaviour
{
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
        battleConfig.SetupConfig(Vector3.zero,enemies);
        //Enter Battle
        midCutscene.Value = true;
        transition.Play();
    }
}
