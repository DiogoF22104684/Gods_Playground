using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;

public class TesteAttack : MonoBehaviour
{
    [SerializeField]
    private BattleMove move;
    private BattleEntity battle;
        

    private void Start()
    {
        battle = new BattleEntity(40);
    }


    public void Attack()
    {
        move.Function(battle, battle);
    }
}
