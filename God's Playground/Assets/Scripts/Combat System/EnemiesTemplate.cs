using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;
using System;
using AIUnityExamples.TicTacToe;

[CreateAssetMenu(menuName = "Scriptables/EntitiesTemplates/Enemy")]
public class EnemiesTemplate : EntityTemplate
{
    
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab => prefab;

    // Time I can take to play in seconds
    private const float timeToThink = 0.5f;

    // Balance between exploitation and exploration
    private readonly float k = 2 / Mathf.Sqrt(2);

    public BattleMove ResolveAction(BattleEntity currentEntity, CombatState state)
    {
        // Keep start time
        DateTime startTime = DateTime.Now;

        // What is my deadline?
        DateTime deadline = startTime + TimeSpan.FromSeconds(timeToThink);

        // Create the root node using the current table
        CombatMCTSNode root = new CombatMCTSNode(state, Skills[0]);

        // The node to be selected for play
        CombatMCTSNode selected;

        int it = 0;
        //Run MCTS and keep improving statistics while we have time
        while (DateTime.Now < deadline && it < 10)
        {
            it++;
            MCTS(root);
        }
      
        // Get the best move, i.e. the one with a higher win ratio
        // (by setting k = 0)
        selected = SelectMovePolicy(root, 0);

        //return MCTS()
        Debug.Log("Move: " + selected.Move);
        return selected.Move;
    }

    private void MCTS(CombatMCTSNode root)
    {
        CombatMCTSNode current = root;

        bool selected = false;

        Stack<CombatMCTSNode> moveSequence = new Stack<CombatMCTSNode>();

        moveSequence.Push(current);

        while (!current.IsTerminal && !selected)
        {
            // Is the current node fully expanded? (i.e. have we
            // tried/expanded all possible moves?)
            if (current.IsFullyExpanded)
            {
                // Then the "new" current node will be selected among the
                // children of the "current" current node
                current = SelectMovePolicy(current, k);
            }
            else
            {
                // Otherwise let's expand one of the currently untried
                // moves and select one of its children as the current node
                current = ExpandPolicy(current);
                selected = true;
            }

            // Add another node to the sequence
            moveSequence.Push(current);
        }

        // Perform a playout / rollout from the current node until the end
        // of the game and obtain the result
        SelectorMode result = current.Playout(PlayoutPolicy).Team;

        // Backpropagate the result along the move sequence
        while (moveSequence.Count > 0)
        {
            // Pop the top node in the sequence
            CombatMCTSNode node = moveSequence.Pop();

            // Increment its number of playouts
            node.Playouts++;

            // Update the win/lose count according whose turn it was to play
            // in the previous turn
            if (result == node.Turn.Team) node.Wins++;
            else if (result == node.Turn.Team) node.Wins--;
        }
    }

    private BattleMove PlayoutPolicy(IList<BattleMove> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    private CombatMCTSNode ExpandPolicy(CombatMCTSNode node)
    {
        // Get the currently untried moves
        IReadOnlyList<BattleMove> untriedMoves = node.UntriedMoves;

        // Randomly select one of the untried moves
        BattleMove move = 
            untriedMoves[UnityEngine.Random.Range(0,untriedMoves.Count)];

        Debug.Log("Do Make Move");
        // Make the untried move and return the respective node
        return node.MakeMove(move) as CombatMCTSNode;
    }

    private CombatMCTSNode SelectMovePolicy(CombatMCTSNode node, float k)
    {
        float lnN = Mathf.Log(node.Playouts);
        CombatMCTSNode bestChild = null;
        float bestUCT = float.NegativeInfinity;
        foreach (AbstractMCTSNode<BattleMove, BattleEntity> childNode in node.Children)
        {
            float uct = childNode.Wins / (float)childNode.Playouts
                + k * (float)Math.Sqrt(lnN / childNode.Playouts);
            if (uct > bestUCT)
            {
                bestUCT = uct;
                bestChild = childNode as CombatMCTSNode;
            }
        }
        return bestChild;
    }


}
