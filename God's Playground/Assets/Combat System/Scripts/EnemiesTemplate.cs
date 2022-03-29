using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;

[CreateAssetMenu(menuName = "Scriptables/EntitiesTemplates/Enemy")]
public class EnemiesTemplate : EntityTemplate
{
    //Behavior Tree?
    [SerializeField]
    List<BattleMove> moves;

    public BattleMove ResolveAction()
    {
        //mata-me :c
        return moves[0];
    }
}
