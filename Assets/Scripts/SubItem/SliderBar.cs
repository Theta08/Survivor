using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : UI_Base
{
    enum Texts
    {
        SliderText,
    }

    [SerializeField] 
    private float _maxValue = 1;
    [SerializeField] 
    private float _value;
    
    public float Value { get { return _value; } set { _value = value; } }
    public float MaxValue { get { return _maxValue; } set { _maxValue = value; } }

    private Slider _slider;
    private TextMeshProUGUI _text;
    void Start()
    {
        // Init();
    }

    void Update()
    {
        float ratio = Value / MaxValue;
        SetHpRatio(ratio);
    }

    // void Init(){ }
    
    public void SetHpRatio(float ratio)
    {
        gameObject.GetComponent<Slider>().value = ratio;
    }
}
