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

    public void ResetValues()
    {
        playerPos = new Vector3(3.55f, 2, -13.6f);
        currentEnemy = -1;
        enemyInScene = new List<int> { 0, 1 };
        enemyPos = new List<Vector3> { new Vector3(21.4f, 1.34f, -12.35f), new Vector3(36.84f, 1.34f, -12.11f) };
    }
    //Other things
}
