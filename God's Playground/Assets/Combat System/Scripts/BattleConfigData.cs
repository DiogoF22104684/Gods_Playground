using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/BattleConfigData")]
public class BattleConfigData : ScriptableObject
{
    [SerializeField]
    List<EnemiesTemplate> enemies;
    List<EnemiesTemplate> Enemies => enemies;



    //Other things
}
