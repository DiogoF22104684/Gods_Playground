using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;

public class TesteAttack : MonoBehaviour
{
    [SerializeField]
    private BattleMove move;
    [SerializeField]
    private BattleEntityProper battle;
    [SerializeField]
    private BattleEntityProper target;


    private void Start()
    {
        battle.animTrigger += AnimationResponse;
    }

    private void AnimationResponse(DefaultAnimations anim)
    {
        target.PlayAnimation(anim);
    }

    public void Attack()
    {
        
        move.Function(battle.entityData, target.entityData);
    }
}
