using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CombatSystem;

public abstract class BattleEntityProper : MonoBehaviour, IConfigurable
{
    public BattleEntity entityData { get; protected set; }
    private Animator anim;

    public Action<DefaultAnimations, IEnumerable<BattleEntity>> attackTrigger;
    public Action<BattleEntity> damageTrigger;
    public Action onDeath;
    public Action onEndTurn;


    [SerializeField]
    protected GameObject hpSliderPREFAB;
    protected BattleSlider hpBattleSlider;

    [SerializeField]
    protected GameObject mpSliderPREFAB;
    protected BattleSlider mpBattleSlider;

    [SerializeField]
    protected GameObject statusEffectDisplayPREFAB;
    protected StatusEffectDisplay statusEffectDisplay;

    protected bool isDead;

    protected IEnumerable<BattleEntity> currentTargets;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        
        
    }
    protected virtual void Start()
    {
        entityData.OnStatusEffectUpdate += () =>
        {
            statusEffectDisplay.Config(entityData);
        };
    }


    public void Config(BattleEntity entityData)
    {
        this.entityData = entityData;
        ConfingBars();      
    }

    protected abstract void ConfingBars();

 
    // Update is called once per frame
    void Update()
    {
        
    }

    public override string ToString()
    {
        return entityData.ToString();
    }


    internal void ChangeValue(string propName, float value)
    {
        switch (propName)
        {
            case "hp":
                hpBattleSlider.ChangeValue(value);
                break;
            case "mp":
                mpBattleSlider.ChangeValue(value);
                break;

        }
    }

    public void PlayAnimation(DefaultAnimations animation)
    {    
        switch (animation)
        {
            case DefaultAnimations.BasicAttack:
                AnimationStart("BasicAttack");
                break;
            case DefaultAnimations.SpecialAttack:
                AnimationStart("SpecialAttack");
                break;
            case DefaultAnimations.DamageTaken:
                AnimationStart("DamageTaken");
                break;
            case DefaultAnimations.Death:
                isDead = true;
                onDeath?.Invoke();
                AnimationStart("Death");
                break;
        }
    }

    public void AttackTriggerAnimation(DefaultAnimations animType)
    {
        attackTrigger?.Invoke(animType, currentTargets);
    }

    protected void IniciateMove(CombatSystem.BattleMove move, CombatState state)
    {
        //Start Animation Set inside of move

        currentTargets =
            state.Selector.SelectEntity(entityData, move);
        
        move.Function(state, currentTargets, 1f);
        PlayAnimation(DefaultAnimations.BasicAttack);
    }

    public void Death()
    {
        
        Destroy(mpBattleSlider.gameObject);
        Destroy(hpBattleSlider.gameObject);
        Destroy(statusEffectDisplay.gameObject);
        onEndTurn?.Invoke();
        Destroy(gameObject);     
    }

    public void DamageTakenTrigger()
    {
        entityData.QueuedMove?.Invoke(entityData);
        damageTrigger?.Invoke(this.entityData);
    }

    private void AnimationStart(string name)
    {
        anim.Play(name);
    }

    public virtual bool StartTurn(CombatState state)
    { 
        entityData.ResolveStatusEffect();
        if (isDead)
        {
            //onEndTurn?.Invoke();
            return false;
        }

        statusEffectDisplay.Config(entityData);

        if (entityData.turns < 1)
        {
            EndTurn();
            return false;
        }


        //to specific

        
        entityData.turns -= 1;
        entityData.FowardSkillTimer();
        return true;
    }

    public abstract void EndTurn();

    protected void OnEndTurn()
    {        
        onEndTurn?.Invoke();
    }


}
