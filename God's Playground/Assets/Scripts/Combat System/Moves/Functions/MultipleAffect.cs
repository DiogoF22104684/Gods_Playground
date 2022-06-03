﻿using System.Collections.Generic;
using UnityEngine;
//Lmao nomes
[System.Serializable]
public class MultipleAffect : BattleAffects
{
    [SerializeField]
    private int numb;
    public int Numb => numb;
    private int used;

    [SerializeField]
    BasicAffect affect;

    float roll;

    public override void Function(BattleEntity attacker, IEnumerable<BattleEntity> target, float roll)
    {
        this.roll = roll;
        used = 0;
        //Criar dano para cada cena

        //Play animation / Needs to be better
        attacker.ProperEntity.PlayAnimation(DefaultAnimations.BasicAttack);


        foreach (BattleEntity en in target)
        {
            AddToEvent(en, attacker);
        }
    }
    
    void RepeatMove(BattleEntity en, BattleEntity attacker)
    {
        //Animation response aqui??
        used++;

        if (used < numb)
        {
            attacker.ProperEntity.PlayAnimation(DefaultAnimations.BasicAttack);

            AddToEvent(en, attacker);
        }
        else
        {
            attacker.EndTurn();
        }
    }

    void AddToEvent(BattleEntity en, BattleEntity attacker)
    {
        en.ProperEntity.damageTrigger += (BattleEntity en) =>
        {
            en.ProperEntity.damageTrigger = null;

            affect.Function(attacker, new List<BattleEntity> { en }, roll);

            RepeatMove(en, attacker);
        };
    }

}