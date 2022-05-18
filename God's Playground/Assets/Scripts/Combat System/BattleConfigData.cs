using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/BattleConfig")]
public class BattleConfigData : ScriptableObject
{
    [SerializeField]
    PlayerTemplate playerTemplate; 
    [SerializeField]
    List<EnemiesTemplate> enemies;

    public List<EnemiesTemplate> Enemies => enemies;
    public PlayerTemplate PlayerTemplate => playerTemplate;

    public int EnemyID { get => currentEnemies; set => currentEnemies = value; }

    [SerializeField] [ReadOnly]
    private int currentEnemies;

    internal void SetupConfig(EnemyAgent enemy,List<EnemiesTemplate> enemies, BattleTransitioner bt)
    {
        this.enemies = enemies;
        currentEnemies = enemy.ID;
    }

}
