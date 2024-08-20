using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StateAbilityItem : UI_Base
{
    enum Buttons
    {
        Box,
    }

    enum Images
    {
        Icon,
    }
    enum SliderInfo
    {
        SliderBar,
    }
    enum Texts
    {
        SliderText,
    }
    
    private TextMeshProUGUI _text;
    private SliderBar _sliderBar;
    
    [SerializeField]
    private int _value;
    [SerializeField]
    private int _MaxValue;
    [SerializeField]
    private Define.UserUpgradeStat _userUpgradeStat;
    public int Value { get { return _value; } set { _value = value; } }
    public int Maxvalue { get { return _MaxValue; } set { _MaxValue = value; } }
    public Define.UserUpgradeStat UserUpgradeStat { get { return _userUpgradeStat; } set { _userUpgradeStat = value; } }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));
        Bind<SliderBar>(typeof(SliderInfo));
        BindText(typeof(Texts));
        
        SetInfo();
        
        return true;
    }

    void SetInfo()
    {

        GetButton((int)Buttons.Box).gameObject.BindEvent(OnClickEvent);
        
        _text =GetText((int)Texts.SliderText);
        _sliderBar = Get<SliderBar>((int)SliderInfo.SliderBar);

        StatSetting();
        
        Maxvalue = 10;
        
        _sliderBar.MaxValue = Maxvalue;
        _sliderBar.Value = Value;
    }

    void OnClickEvent()
    {
        if (Maxvalue > _sliderBar.Value)
        {
            _sliderBar.Value += 1;
            _text.text = _sliderBar.Value.ToString();
            
            switch (_userUpgradeStat)
            {
                case Define.UserUpgradeStat.HPPanel:
                    Managers.Game.SaveData.CharacterUpgrade.hp += (int)_sliderBar.Value;
                    break;
                case Define.UserUpgradeStat.SpdPanel:
                    Managers.Game.SaveData.CharacterUpgrade.spd += (int)_sliderBar.Value;
                    break;
                case Define.UserUpgradeStat.AtkPanel:
                    Managers.Game.SaveData.CharacterUpgrade.atk += (int)_sliderBar.Value;
                    break;
                case Define.UserUpgradeStat.AtkSpdPanel:
                    Managers.Game.SaveData.CharacterUpgrade.atkSpd += (int)_sliderBar.Value;
                    break;
            }
            
        }
        else
            Debug.Log("Maxvalue");
    }

    void StatSetting()
    {
        Sprite sprties = null;
        
        switch (_userUpgradeStat)
        {
            case Define.UserUpgradeStat.HPPanel:
                Value = Managers.Game.SaveData.CharacterUpgrade.hp;
                sprties = Resources.Load<Sprite>("Sprites/Icon_HP");
                break;
            case Define.UserUpgradeStat.SpdPanel:
                Value = Managers.Game.SaveData.CharacterUpgrade.spd;
                sprties = Resources.Load<Sprite>("Sprites/Icon_Spd");
                break;
            case Define.UserUpgradeStat.AtkPanel:
                Value = Managers.Game.SaveData.CharacterUpgrade.atk;
                sprties = Resources.Load<Sprite>("Sprites/Icon_Atk");
                break;
            case Define.UserUpgradeStat.AtkSpdPanel:
                Value = Managers.Game.SaveData.CharacterUpgrade.atkSpd;
                sprties = Resources.Load<Sprite>("Sprites/Icon_AtkSpd");
                break;
        }

        GetImage((int)Images.Icon).sprite = sprties;
        _text.text = Value.ToString();
    }
    
}
