using System.Collections.Generic;
using CombatSystem;
using System.Linq;
using static CombatSystem.SelectorMode;
using static CombatSystem.SelectorType;
using System;

public class CombatState
{
    //AAAAAAAAAAAA
    public List<BattleEntity> Players { get; private set; }
    public List<BattleEntity> Enemies { get; private set; }

    public List<BattleEntity> EntityOrder { get; private set; }

   

    public int TurnNumber { get; private set; }

    public BattleEntity Turn { get; private set; }

    private EntitySelector selector;
    public EntitySelector Selector => selector;

    public CombatState(IEnumerable<BattleEntity> entities)
    {
        Players = new List<BattleEntity> { };
        Enemies = new List<BattleEntity> { };
       
        foreach (BattleEntity be in entities)
        {         
            if (be.Team == Team)
                Players.Add(be);
            else if (be.Team == Adversary)
                Enemies.Add(be);
        }

        selector = new EntitySelector(this);

        PrepareTurnOrder();
    }


    CombatState original;
    public void DoMove(BattleMove move, float roll, CombatState original)
    {
        this.original = original;

        //Get targets
        IEnumerable<BattleEntity> targets =
            selector.SelectEntity(Turn, move);

        //UnityEngine.Debug.Log("Targets");
        //targets.print();
        //UnityEngine.Debug.Log($"THE CURRENT ENEMY IS: " + Enemies.First());
        //UnityEngine.Debug.Log($"THE CURRENT PLAYER IS: " + Players.First());
        UnityEngine.Debug.Log($"{Turn.Team.GetTeamName()} used {move}!");
        UnityEngine.Debug.Log($"{targets.First().Team.GetTeamName()} had {targets.First().Hp.Stat} health...");

        //UnityEngine.Debug.Log($"ENTERING BATTLE HASH: {original.Enemies.First().GetHashCode()}");

        //Resolve selected battle move
        move.Function(this, targets, roll);

        //UnityEngine.Debug.Log($"LEAVING BATTLE HASH: {original.Enemies.First().GetHashCode()}");
        Turn.turns -= 1;
        foreach (BattleEntity t in targets)
        {
            t.QueuedMove?.Invoke(t);
        }


        //UnityEngine.Debug.Log($"{targets.First().Team.GetTeamName()} now has: " + targets.First().Hp.Stat + " health!");

        //UnityEngine.Debug.Log(Players[0].Hp);
        //UnityEngine.Debug.Log("-----End Turn------");
        //Pass turn
        NextTurn();
    }

    internal bool CanUseMove(BattleMove move)
    {
        return move.IsUsable(Turn); //&& selector.IsSelectable();
    }

    private bool PrepareTurnOrder()
    {
        //Ignore if the combat has ended
        if (Status() != null) return false;

        ///UnityEngine.Debug.Log("Player in Prepare: " + Players[0].Hp);

        List<BattleEntity> totalTurnList = new List<BattleEntity>(Enemies);
        totalTurnList.AddRange(Players);

        //Get a list of all the entities that can still make a move
        List<BattleEntity> canMoveList = totalTurnList.
            Where(x => x.turns.Stat > 0).ToList();

        //Check if there's any entity in the list 
        if (canMoveList.Count == 0)
        {
            //If not restore the entity turn value to the default one
            foreach (BattleEntity be in totalTurnList)
            {
                be.turns.RestorToDefault();
                
            }
            canMoveList = totalTurnList.
             Where(x => x.turns.Stat > 0).ToList();
        } 
      
        //Order the list by dexterity stat
        totalTurnList = canMoveList.
            OrderBy(x => -x.dex.Stat).ToList();

        //Select first entity in order to be the current entity
        if (totalTurnList.Count > 0)
        {
            Turn = totalTurnList[0];
        }

        EntityOrder = totalTurnList;
        return true;
        //selector.Config(playerProper, enemies.Select(x => x.ProperEntity).ToList());
    }

    public void NextTurn()
    {
        ///selector.PlayerHasAttacked = false;

        ///UnityEngine.Debug.Log("Player in Next: " + Players[0].Hp);

        //Update turn order and get current turn's entity
        PrepareTurnOrder();
        TurnNumber++;
       
        ///inTurnEntity.ProperEntity.StartTurn();
    }

    public CombatState Copy()
    {
        List<BattleEntity> copyList = new List<BattleEntity>();

        //UnityEngine.Debug.Log($"Original Enemy: {Enemies.First()}");
        copyList.AddRange(Players.Select(x => x.Copy()));
        copyList.AddRange(Enemies.Select(x => x.Copy()));

     

        CombatState state = new CombatState(copyList);
        state.Enemies.First().Hp.Stat -= 1;

        // UnityEngine.Debug.Log($"Copy Enemy: {state.Enemies.First()}");
        UnityEngine.Debug.Log($"During Copy Copy HASH: {state.Enemies.First().GetHashCode()}");
        // UnityEngine.Debug.Log($"Original Enemy: {Enemies.First()} ");
        UnityEngine.Debug.Log($"During Copy Original HASH: {Enemies.First().GetHashCode()}");

        //Probably need to add something here
        state.Turn = this.Turn;
        return state;
    }

    public SelectorMode? Status()
    {
        if(Players.Count(x => !x.IsDead) == 0)
        {
            return SelectorMode.Adversary;
        }
        else if (Enemies.Count(x => !x.IsDead) == 0)
        {
            return SelectorMode.Team;
        }
        else
        {
            return null;
        }
    }

}