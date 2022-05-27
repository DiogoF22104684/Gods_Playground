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


    private void Awake()
    {
        anim = GetComponent<Animator>();
        
        
    }
    private void Start()
    {

        entityData.onStatusEffectUpdate += () =>
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

    internal bool skillInCooldown(BattleMove battleMove)
    {       
        return entityData.skillCooldowns.Contains(battleMove);
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

    public abstract void AttackTriggerAnimation(DefaultAnimations animType);

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
        damageTrigger?.Invoke(this.entityData);
    }

    private void AnimationStart(string name)
    {
        anim.Play(name);
    }

    public virtual bool StartTurn()
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

        
        entityData.turns.Stat--;
        entityData.fowardSkillTimer();
        return true;
    }

    public abstract void EndTurn();

    protected void OnEndTurn()
    {
        onEndTurn?.Invoke();
    }


}
