using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;
using UnityEngine.UI;

[System.Serializable]
public class BattleEntity
{

    [MoveAffecter]
    public BattleStat Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;

            if (value.Stat <= 0)
            {
                ProperEntity?.PlayAnimation(DefaultAnimations.Death);
                IsDead = true;
                value.Stat = 0;
            }
            if (value.Stat > value.MaxStat)
            {
                value.Stat = value.MaxStat;
            }
            ProperEntity?.ChangeValue("hp", hp.Stat);
        }
    }
    private BattleStat hp;


    [MoveAffecter]
    public BattleStat Mp
    {
        get
        {
            return mp;
        }
        set
        {
            mp = value;

            if (value.Stat <= 0)
            {
                value.Stat = 0;
            }
            ProperEntity?.ChangeValue("mp", mp.Stat);
        }
    }
    private BattleStat mp;


    [MoveAffecter]
    public BattleStat str { get; set; }

    [MoveAffecter]
    public BattleStat def { get; set; }

    [MoveAffecter]
    public BattleStat dex { get; set; }

    [MoveAffecter]
    public BattleStat turns { get; set; }


    public List<TurnTimer<StatusEffectHelper>> statusEffects { get; private set; }
    public List<TurnTimer<BattleMove>> skillCooldowns { get; private set; }


    public EntityTemplate Template { get; }

    public BattleEntityProper ProperEntity { get; }
    
    public System.Action OnStatusEffectUpdate { get; set; }

    public bool IsDead { get; private set; }

    /// <summary>
    /// Battle move beeing acted on the entity next
    /// </summary>
    public Action<BattleEntity> QueuedMove { get; set; }

    public BattleEntity(BattleEntityProper proper, EntityTemplate template)
    {
        ProperEntity = proper;
        int hpValue = template.HP;
        this.hp = new BattleStat(hpValue, hpValue, 0);

        int mpValue = template.Mp;
        this.mp = new BattleStat(mpValue, mpValue, 0);

        turns = new BattleStat(1, 1, 0);

        float strTemp = (template.Str * 2) / 100f;
        str = new BattleStat(strTemp, strTemp, 0);

        float defTemp = (template.Def * 2) / 100f;
        def = new BattleStat(defTemp, defTemp, 0);

        float dextemp = (template.Dex * 2) / 100f;
        dex = new BattleStat(template.Dex, template.Dex, 0);

        this.Template = template;
        statusEffects = new List<TurnTimer<StatusEffectHelper>> { };
        skillCooldowns = new List<TurnTimer<BattleMove>> { };
    }
    
  
    private BattleEntity(BattleEntity entity)
    {
        this.QueuedMove = null;
        this.hp = entity.Hp.Copy();
        this.mp = entity.Mp.Copy();
        this.str = entity.str.Copy();
        this.def = entity.def.Copy();
        this.dex = entity.dex.Copy();
        this.turns = entity.turns.Copy();
        this.statusEffects = new List<TurnTimer<StatusEffectHelper>> { };
        this.statusEffects.AddRange(entity.statusEffects);
        this.skillCooldowns = entity.skillCooldowns;
        this.Template = entity.Template;
    }


  

    public void EndTurn()
    {
        ProperEntity?.EndTurn();
    }

    public BattleEntity Copy()
    {
        return new BattleEntity(this);
    }

    public bool SkillInCooldown(BattleMove battleMove)
    {       
        return skillCooldowns.Contains(battleMove);
    }

    /// <summary>
    /// Check if the entity is in the same team as the given entity.
    /// </summary>
    /// <param name="target">Entity to querry</param>
    /// <returns>True if both entities are in the same team.</returns>
    public bool IsSameTeam(BattleEntity target)
    {
        //Pretty ugly
        bool isPlayer = (Template is PlayerTemplate);
        bool otherisPlayer = (target.Template is PlayerTemplate);
        return isPlayer == otherisPlayer;
    }

    public SelectorMode Team => (Template is PlayerTemplate) ? 
        SelectorMode.Team : SelectorMode.Adversary;

  

    public override string ToString()
    {
        string teamString = Team == SelectorMode.Team ? "Player" : "Enemy";
        return $"{teamString} \n Hp: {hp.Stat} \nAtk: {str.Stat} \nDef:{def.Stat}";
    }

    public void AddStatusEffect(StatusEffectHelper statusEft)
    {
        statusEffects.Add(new TurnTimer<StatusEffectHelper>(statusEft));
        OnStatusEffectUpdate?.Invoke();
    }

    public void AddSkillCooldown(BattleMove move)
    {
        skillCooldowns.Add(new TurnTimer<BattleMove> (move));
    }

    internal void ResolveStatusEffect()
    {
        for (int i = statusEffects.Count - 1; i >= 0; i--)
        {           
            TurnTimer<StatusEffectHelper> d = statusEffects[i];

            StatusEffectHelper effect = d.Effect;

            effect.Effect?.Invoke(this, d.Timer);

            //Debug.Log("TimePassed: " + d.Timer + "Duration:" + effect.CoolDown);

            d.Timer++;

            if (d.Timer >= effect.CoolDown)
            {
                effect.EndEffect?.Invoke(this);
                statusEffects.RemoveAt(i);
            }
        }  
    }

    //there's probably a better way for this
    internal void FowardSkillTimer()
    {
        for (int i = skillCooldowns.Count - 1; i >= 0; i--)
        {
            TurnTimer<BattleMove> timer = skillCooldowns[i];
            if (timer.Timer >= timer.Effect.Config.Cooldown)
            {
                skillCooldowns.Remove(timer);
                continue;
            }
            timer.Timer++; 
        }
    }
}
