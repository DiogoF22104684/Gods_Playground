using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BattleEntityProper : MonoBehaviour
{
    public BattleEntity entityData { get; protected set; }
    private Animator anim;

    public Action<DefaultAnimations, BattleEntity> attackTrigger;
    public Action damageTrigger;
    public Action onDeath;
    public Action onEndTurn;


    [SerializeField]
    protected GameObject hpSliderPREFAB;
    protected BattleSlider hpBattleSlider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        
        
    }
    private void Start()
    {
       
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
                AnimationStart("Death");
                break;
        }
    }

    public abstract void AttackTriggerAnimation(DefaultAnimations animType);

    public void Death()
    {
        onDeath?.Invoke();
        Destroy(hpBattleSlider.gameObject);
        Destroy(gameObject);
    }

    public void DamageTakenTrigger()
    {
        damageTrigger?.Invoke();
        damageTrigger = null;
    }

    private void AnimationStart(string name)
    {
        anim.Play(name);
    }

    public virtual void StartTurn()
    {
        entityData.ResolveDebuffs(); 
    }

    public abstract void EndTurn();

    protected void OnEndTurn()
    {
        onEndTurn?.Invoke();
    }


}
