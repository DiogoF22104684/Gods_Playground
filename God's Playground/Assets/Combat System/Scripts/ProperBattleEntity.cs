using UnityEngine;

public class ProperBattleEntity: MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnimation(DefaultAnimations animation)
    {
        switch (animation)
        {
            case DefaultAnimations.BasicAttack:
                AnimationStart("BasicAnimation");
                break;
            case DefaultAnimations.SpecialAttack:
                AnimationStart("SpecialAttack");
                break;
            case DefaultAnimations.DamageTaken:
                AnimationStart("DamageTaken");
                break;
        }
    }

    private void AnimationStart(string name)
    {
        anim.Play(name);
    }

    //Maybe mas se calhar é melhor fazer uma cena no inspetor
    //public void PlayAnimation(string name)
    //{

    //}

}

public enum DefaultAnimations
{
    BasicAttack,
    SpecialAttack,
    DamageTaken
}