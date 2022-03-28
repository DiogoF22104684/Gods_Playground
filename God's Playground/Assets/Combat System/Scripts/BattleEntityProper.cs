using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BattleEntityProper : MonoBehaviour
{
    public BattleEntity entityData { get; protected set; }
    private Animator anim;

    public Action<DefaultAnimations> attackTrigger;
    public Action damageTrigger;
    public Action onDeath;
    public Action onEndTurn;

    //A Entity devia instanciar o slider
    [SerializeField]
    protected BattleSlider hpSlider;



    private void Awake()
    {
        anim = GetComponent<Animator>();
        
        
    }

    public void Config(BattleEntity entityData)
    {
        this.entityData = entityData;
    }

 
    // Update is called once per frame
    void Update()
    {
        
    }



    internal void ChangeValue(string propName, float value)
    {
        switch (propName)
        {
            case "hp":
                hpSlider.ChangeValue(value);
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

    public void AttackTriggerAnimation(DefaultAnimations animType)
    {
        attackTrigger?.Invoke(animType);
    }

    public void Death()
    {
        onDeath?.Invoke();
        Destroy(hpSlider.gameObject);
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

    public abstract void StartTurn();
    public abstract void EndTurn();

    protected void OnEndTurn()
    {
        onEndTurn?.Invoke();
    }


}
