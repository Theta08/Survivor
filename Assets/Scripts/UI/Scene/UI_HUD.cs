using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HUD : UI_Scene //UI_Scene
{
    enum Texts
    {
        SliderText,
    }
    enum Objects
    {
        SliderBar,
        Text,
    }

    [SerializeField] 
    private float _maxValue = 1;
    [SerializeField] 
    private float _value;

    private Define.InfoType _infoType;
    public float Value { get { return _value; } set { _value = value; } }
    public float MaxValue { get { return _maxValue; } set { _maxValue = value; } }

    private Slider _slider;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        Bind<GameObject>(typeof(Objects));
        
        _slider = GetObject((int)Objects.SliderBar).GetComponent<Slider>();
        _text = GetObject((int)Objects.Text).GetComponent<TextMeshProUGUI>();
    }
    
    void LateUpdate()
    {
        Type();
    }

    public void Init(Define.InfoType type)
    {
        _infoType = type;
    }

    // type별로 표시
    void Type()
    {
      
        MaxValue = Managers.Game.SaveData.NextExp[Managers.Game.SaveData.Level];
        Value = Managers.Game.SaveData.Exp;
        
        SetHpRatio(Value / MaxValue);

        // float timer = Managers.Game.MaxGameTime - Managers.Game.GameTime;
        float timer = Managers.Game.GameTime;
        int min = Mathf.FloorToInt(timer / 60);
        int sec = Mathf.FloorToInt(timer % 60);
        _text.text = string.Format($"{min :D2} : {sec :D2}");
        
    }
    
    public void SetHpRatio(float ratio)
    {
        _slider.value = ratio;
    }
}
