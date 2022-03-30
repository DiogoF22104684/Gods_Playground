using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/BattleConfigData")]
public class BattleConfigData : ScriptableObject
{
    [SerializeField]
    PlayerTemplate playerTemplate;
    public PlayerTemplate PlayerTemplate => playerTemplate;

    [SerializeField]
    List<EnemiesTemplate> enemies;
    public List<EnemiesTemplate> Enemies => enemies;

    //Other things
}
