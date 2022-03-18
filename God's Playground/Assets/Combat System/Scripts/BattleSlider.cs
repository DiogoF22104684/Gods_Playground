using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BattleSlider : MonoBehaviour
{

    private Slider slider;
    private bool inValueChange;
    private float valueToChange;
    [SerializeField]
    private float amountOfChange;

    private void Awake()
    {
        slider = GetComponent<Slider>();
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

    public void Config(float maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;
    }

    public void ChangeValue(float value, bool animated = true)
    {
        inValueChange = true;
        valueToChange = value;
        //ActivateAnimation
    }

}
