using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BattleTransitioner : MonoBehaviour
{

    [SerializeField]
    private BattleConfigData battleConfig;

    [SerializeField]
    private PlayableDirector transition;

    [SerializeField]
    private ScriptableBool midCutscene;

    [SerializeField]
    List<EnemiesTemplate> enemies;

    public PlayableDirector Transition { get => transition; set => transition = value; }


    public void EnterBattle()
    {
        //Change Battle Config
        battleConfig.SetupConfig(transform.position,enemies, this);
        //Enter Battle
        midCutscene.Value = true;
        Transition.Play();
    }
}
