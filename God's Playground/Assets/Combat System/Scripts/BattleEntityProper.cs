using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleEntityProper : MonoBehaviour
{
    public BattleEntity entityData { get; private set; }
    private Animator anim;
    public Action<DefaultAnimations> animTrigger;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        entityData = new BattleEntity(30, this);
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

    public void TriggerAnimation(DefaultAnimations animType)
    {
        animTrigger?.Invoke(animType);
    }

    private void AnimationStart(string name)
    {
        anim.Play(name);
    }
}
