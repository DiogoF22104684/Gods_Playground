using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BattleSlider : MonoBehaviour, IConfigurable
{

    private Slider slider;
    private bool inValueChange;
    private float valueToChange;
    [SerializeField]
    private float amountOfChange;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inValueChange) 
        {
            slider.value = Mathf.Lerp(slider.value, valueToChange, amountOfChange);
            if (slider.value == valueToChange)
                inValueChange = false;
        }
    }

    public void ChangeValue(float value)
    {
        inValueChange = true;
        valueToChange = value;
        //ActivateAnimation
    }

    public void Config(BattleEntity entity)
    {
        if (slider == null) slider = GetComponent<Slider>();
        slider.maxValue = entity.Hp;
        slider.value = entity.Hp;
    }
}
