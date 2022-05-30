using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIUnityExamples.TicTacToe;
using CombatSystem;
using System;
using System.Linq;

public class CombatMCTSNode : AbstractMCTSNode<BattleMove, BattleEntity> 
{

    private CombatState state;

    public CombatMCTSNode(CombatState state, BattleMove move): base(move)
    {
        this.state = state;
    }

    // List of valid moves
    private IList<BattleMove> validMoves;

    public override bool IsTerminal => state.Status().HasValue;

    public override BattleEntity Turn => state.Turn;

    protected override IEnumerable<BattleMove> ValidMoves
    {
        get
        {           
            if(validMoves == null)
            {
                validMoves = 
                    Turn.Template.Skills.
                    Where(x => x.IsUsable(Turn, null)).ToList();
            }return validMoves;
        }
    }

    public override BattleEntity Playout(Func<IList<BattleMove>, BattleMove> strategy)
    {
       
        CombatState stateCopy = state.Copy();

        int playouts = 0;

        //Debug.Log("Begin Playout");

        Debug.Log("------------------------------------------Begin Playout------------------------------------------");
        // Keep playing using the given strategy until an endgame is reached
        while (!stateCopy.Status().HasValue && playouts < 50)
        {         
            // Use the given strategy to obtain a move
            BattleMove move = strategy(ValidMoves.ToList());


            Debug.Log($"----Being turn {playouts}---- ");
            // Perform move assuming the roll is max
            stateCopy.DoMove(move, 1, state);

            //Debug.Log($"Turn {playouts}" + "Player HP:" + stateCopy.Players[0].Hp + "Enemy HP:" + stateCopy.Enemies[0] + "Move:" + move);
          
            playouts++;
        }

        

        //Scuffed
        SelectorMode? winTeam = stateCopy.Status();

        Debug.Log(winTeam);
        Debug.Log("------------------------------------------End Playout------------------------------------------");

        BattleEntity winEntity = null;

        if (winTeam != null)
            winEntity = winTeam == SelectorMode.Team ? stateCopy.Players[0] :
                stateCopy.Enemies[0];
        else
            winEntity = stateCopy.Players[0];


        return winEntity;
    }

    protected override AbstractMCTSNode<BattleMove, BattleEntity> DoMakeMove(BattleMove move)
    {
        CombatState stateCopy = state.Copy();

        Debug.Log(stateCopy.Turn);

        Debug.Log("------------------------------------------Do Move------------------------------------------");
        stateCopy.DoMove(move, 1, state);
        Debug.Log("------------------------------------------END DO MOVE------------------------------------------");
        return new CombatMCTSNode(stateCopy, move);
    }

   
}
