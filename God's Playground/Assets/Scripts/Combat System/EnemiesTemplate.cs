using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;

[CreateAssetMenu(menuName = "Scriptables/EntitiesTemplates/Enemy")]
public class EnemiesTemplate : EntityTemplate
{
    //Decision Tree?
    [SerializeField]
    List<BattleMove> moves;

    [SerializeField]
    GameObject prefab;
    public GameObject Prefab => prefab;
    

    public BattleMove ResolveAction()
    {
        //mata-me :c
        return moves[0];
    }
}
