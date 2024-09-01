using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FollowHpBar : UI_Scene
{
    
    enum Objects
    {
        Health,
        HpSliderBar,
    }
    private RectTransform _rect;
    private Slider _slider;

    [SerializeField]
    private float _maxValue;
    [SerializeField]
    private float _value;
    
    private void Start()
    {
        Bind<GameObject>(typeof(Objects));
        _rect = GetObject((int)Objects.HpSliderBar).GetComponent<RectTransform>();
        _slider = GetObject((int)Objects.HpSliderBar).GetComponent<Slider>();
        
    }

    void LateUpdate()
    {
        _maxValue = Managers.Game.GetPlayer.Stat.MaxHp;
        _value = Managers.Game.GetPlayer.Stat.Hp;

        _slider.value = _value / _maxValue;
    }

    private void FixedUpdate()
    {
        _rect.position = Camera.main.WorldToScreenPoint(
            Managers.Game.GetPlayer.transform.position + new Vector3(0, -1, 0));
    }
}
