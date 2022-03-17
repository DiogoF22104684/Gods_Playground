using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleEntityProper : MonoBehaviour
{
    public BattleEntity entityData { get; private set; }
    private Animator anim;
    public Action<DefaultAnimations> attackTrigger;
    public Action damageTrigger;

    //A Entity devia instanciar o slider
    [SerializeField]
    private BattleSlider hpSlider;

    internal void ChangeValue(string propName, float value)
    {
        switch (propName)
        {
            case "hp":
                hpSlider.ChangeValue(value);
                break;
        }
    }

   


    private void Awake()
    {
        anim = GetComponent<Animator>();
        entityData = new BattleEntity(30, this);
    }

    private void Start()
    {
        if(hpSlider != null)
            hpSlider.Config(entityData.Hp);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }

    public void AttackTriggerAnimation(DefaultAnimations animType)
    {
        attackTrigger?.Invoke(animType);
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
}
