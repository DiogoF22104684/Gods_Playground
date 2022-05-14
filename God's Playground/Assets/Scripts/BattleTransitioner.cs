using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


/// <summary>
/// Class responsible for setting up the game when transitioning between the 
/// Overworld and the Combat scenes
/// </summary>
public class BattleTransitioner : MonoBehaviour
{
    //Data to inicialize the battle
    [SerializeField]
    private BattleConfigData battleConfig;

    //True if the game is in a cutscene
    [SerializeField]
    private ScriptableBool midCutscene;

    //List of enemies to instatiate in the battle
    [SerializeField]
    List<EnemiesTemplate> enemies;

    //Enemy Info
    private EnemyAgent agent;

    //Collide with player
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EnterBattle();
        }
    }

    /// <summary>
    /// Timeline director in the scene
    /// </summary>
    public PlayableDirector Transition { get; set; }
      
    private void Start()
    {
        agent = GetComponent <EnemyAgent>();
        Transition = FindObjectOfType<PlayableDirector>();
    }

    /// <summary>
    /// Method responsible for preparing the game to enter a battle
    /// /// </summary>
    public void EnterBattle()
    {
        //Change Battle Config
        battleConfig.SetupConfig(agent, enemies, this);
        //Change bool to stop entities from moving
        midCutscene.Value = true;
        //Play the transition between scenes
        Transition.Play();
        //Save the game
        SaveLoadManager.Instance.Save();
    }
}
