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
    [SerializeField]
    private Vector3 playerPos;
    public Vector3 PlayerPos => playerPos;

    public Action<BattleTransitioner> onEnterBattle;

    public List<int> enemyInScene;
    public List<Vector3> enemyPos;
    public int currentEnemy;

    internal void SetupConfig(Vector3 playerPos, List<EnemiesTemplate> enemies, BattleTransitioner bt)
    {
        this.enemies = enemies;
        this.playerPos = playerPos;
        onEnterBattle?.Invoke(bt); 
    }

    //Other things
}
