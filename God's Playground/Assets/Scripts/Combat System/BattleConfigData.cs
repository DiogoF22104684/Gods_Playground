using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/BattleConfig")]
public class BattleConfigData : ScriptableObject
{
    [SerializeField]
    PlayerTemplate playerTemplate;
    public PlayerTemplate PlayerTemplate => playerTemplate;

    [SerializeField]
    List<EnemiesTemplate> enemies;
    public List<EnemiesTemplate> Enemies => enemies;

    private Vector3 playerPos;

    internal void SetupConfig(Vector3 playerPos, List<EnemiesTemplate> enemies)
    {
        this.enemies = enemies;
        this.playerPos = playerPos;
    }

    //Other things
}
