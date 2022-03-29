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
    private GameObject hpSliderPREFAB;
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

        //Maybe try to find the canvas component and add to that gameObject instead
        GameObject slider = Instantiate(hpSliderPREFAB, transform.position,         
            Quaternion.identity, GameObject.Find("HealthBars").transform);

        hpBattleSlider = slider.GetComponent<BattleSlider>();
        hpBattleSlider.Config(entityData.Hp);
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        RectTransform sliderRect = slider.GetComponent<RectTransform>();

        sliderRect.ScaleWithTarget(transform, 1.7f);

        Vector3 topPos = 
            Camera.main.WorldToScreenPoint(
                GetComponent<Collider>().bounds.center +
                Vector3.zero.y(GetComponent<Collider>().bounds.extents.y + 0.3f));
       

        sliderRect.position = topPos; 

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

    public abstract void StartTurn();
    public abstract void EndTurn();

    protected void OnEndTurn()
    {
        onEndTurn?.Invoke();
    }


}
