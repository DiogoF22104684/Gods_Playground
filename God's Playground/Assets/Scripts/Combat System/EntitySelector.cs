using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CombatSystem;
using static CombatSystem.SelectorMode;
using static CombatSystem.SelectorType;

public class EntitySelector
{
    public bool PlayerHasAttacked { get; internal set; }
 
    public BattleEntity SelectedEntity { get; set; }

    public List<BattleEntity> SelectedEntities { get; private set; }
 
    private CombatState state;
      
    public EntitySelector(CombatState state)
    {
        this.state = state;
    }


    private IEnumerable<BattleEntity> GetAllAdjacent()
    {
        List<BattleEntity> returnlist = new List<BattleEntity> { };
        int index = state.Enemies.IndexOf(SelectedEntity);
        
        returnlist.Add(SelectedEntity);

        if (index - 1 >= 0)
        {
            returnlist.Add(state.Enemies[index - 1]);
        }
        if (index + 1 < state.Enemies.Count)
        {
            returnlist.Add(state.Enemies[index + 1]);
        }

        

        return returnlist;
    }
      
    /// <summary>
    /// Get team given an entity 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="ofTeam"></param>
    /// <returns></returns>
    private IEnumerable<BattleEntity> GetTeam(BattleEntity entity, bool ofTeam = false)
    {
        
        bool isPlayer = !(entity.IsSameTeam(state.Players[0]) ^ ofTeam);
        return isPlayer ?
            state.Players
            : 
            state.Enemies
            ;
    }

    /// <summary>
    /// Get entity given an entity 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="ofTeam"></param>
    /// <returns></returns>
    private BattleEntity GetSelected(BattleEntity entity, bool ofTeam = false)
    {
        bool isPlayer = !(entity.IsSameTeam(state.Players[0]) ^ ofTeam);

        //lmao
        return isPlayer ? state.Players[0] != null ? state.Players[0] : GetRandom(Team) :
            SelectedEntity != null ? SelectedEntity : GetRandom(Adversary);
    }

    private BattleEntity GetRandom(SelectorMode team)
    {
        IList<BattleEntity> selectedTeamList = 
            team == Team ? state.Players : state.Enemies;

        int rnd = UnityEngine.Random.Range(0, selectedTeamList.Count);

        return selectedTeamList[rnd];
    }
 
    public IEnumerable<BattleEntity> SelectEntity(
        BattleEntity attacker,
        BattleMove move)
    {
        List<BattleEntity> returnlist = new List<BattleEntity> { };

        //Bad dumb and ugly
        switch (move.Config.Type)
        {
            case Solo:
                switch (move.Config.Mode)
                {
                    case SelectorMode.All:
                        returnlist.Add(GetSelected(attacker));
                        return returnlist;
                    case Self:
                        returnlist.Add(attacker);
                        return returnlist;
                    case Adversary:
                        returnlist.Add(GetSelected(attacker));
                        return returnlist;
                    case Team:
                        returnlist.Add(GetSelected(attacker));
                        return returnlist;
                }
                break;           
            case SelectorType.All:
                switch (move.Config.Mode)
                {
                    case SelectorMode.All:
                        returnlist = state.Enemies;
                        returnlist.AddRange(state.Players);
                        return returnlist;
                    case Self:
                        returnlist.Add(attacker);
                        return returnlist;
                    case Adversary:                        
                        return GetTeam(attacker);
                    case Team:
                        return GetTeam(attacker, true);
                }
                break;
        }
        
        return returnlist;
    }

    public IEnumerable<BattleEntity> SelectEntity(
        BattleEntity attacker,
        SelectorType type,
        SelectorMode team)
    {
        List<BattleEntity> returnlist = new List<BattleEntity> { };

        //Bad dumb and ugly
        switch (type)
        {
            case Solo:
                switch (team)
                {
                    case SelectorMode.All:
                        returnlist.Add(GetSelected(attacker));
                        return returnlist;
                    case Self:
                        returnlist.Add(attacker);
                        return returnlist;
                    case Adversary:
                        returnlist.Add(GetSelected(attacker));
                        return returnlist;
                    case Team:
                        returnlist.Add(GetSelected(attacker));
                        return returnlist;
                }
                break;
            case SelectorType.All:
                switch (team)
                {
                    case SelectorMode.All:
                        returnlist = state.Enemies;
                        returnlist.AddRange(state.Players);
                        return returnlist;
                    case Self:
                        returnlist.Add(attacker);
                        return returnlist;
                    case Adversary:
                        return GetTeam(attacker);
                    case Team:
                        return GetTeam(attacker, true);
                }
                break;
        }

        return returnlist;
    }


    public IEnumerable<BattleEntity> GetRandomRange(
        BattleEntity attacker, SelectorMode team, int numb, bool canRepeat = true)
    {
        List<BattleEntity> affectedEntities = state.Enemies;
        affectedEntities.AddRange(state.Players);

        if (team == Adversary || team == Team)
        {
            bool isTeam = true;
            if (team == Adversary)
            {
                isTeam = false;
            }

            affectedEntities = GetTeam(attacker, isTeam).ToList();
        }
        else if (team == Self)
        {
            affectedEntities = state.Players;
        }


        List<BattleEntity> returnList = new List<BattleEntity>();
        for (int i = 0; i < numb; i++)
        {
            int r =  UnityEngine.Random.Range(0,affectedEntities.Count);
            returnList.Add(affectedEntities[r]);
            if (!canRepeat)
            {
                affectedEntities.RemoveAt(r);
            }
        }

        return returnList;
    }

    //public static IEnumerable<BattleEntity> SelectEntity(
    //        SelectorType type, SelectorMode team)
    //{
    //    List<BattleEntity> returnlist = new List<BattleEntity> { };

    //    //Bad dumb and ugly
    //    switch (type)
    //    {
    //        case Solo:
    //            switch (team)
    //            {
    //                case SelectorMode.All:
    //                    returnlist.Add(GetSelected(attacker));
    //                    return returnlist;
    //                case Self:
    //                    returnlist.Add(state.Turn);
    //                    return returnlist;
    //                case Adversary:
    //                    returnlist.Add(GetSelected(attacker));
    //                    return returnlist;
    //                case Team:
    //                    returnlist.Add(GetSelected(attacker));
    //                    return returnlist;
    //            }
    //            break;
    //        case SelectorType.All:
    //            switch (team)
    //            {
    //                case SelectorMode.All:
    //                    returnlist = enemiesEntity.Select(x => x.entityData).ToList();
    //                    returnlist.Add(playerEntity.entityData);
    //                    return returnlist;
    //                case Self:
    //                    returnlist.Add(attacker);
    //                    return returnlist;
    //                case Adversary:
    //                    return GetTeam(attacker);
    //                case Team:
    //                    return GetTeam(attacker, true);
    //            }
    //            break;
    //    }

    //    return returnlist;
    //}


}
