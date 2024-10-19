using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
/**
 * 무기 업그레이드 
 */
public class UI_AbilityItem : UI_Base
{
    enum Texts
    {
        LvText,
        NameText,
        DescText
    }

    enum Buttons
    {
        Button,
    }

    enum Images
    {
        Image,
    }
    
    private Item _item;
    private Sprite _sprite;
    private WeaponController _weapon;
    private GearController _gearController;
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));
        
        GetButton((int)Buttons.Button).gameObject.BindEvent(OnButtonEvent);
        return true;
    }
    
    
    void OnButtonEvent()
    {
        if (!GetButton((int)Buttons.Button).interactable)
            return;
        // 아이템 획득시 업그래드 및 생성
        switch (_item.type)
        {
            case "weapon":
            case "range":
                // id == 0 : 기본 무기 처음에 장착함
                if (_item.level == 0 && _item.id != 0)
                {
                    GameObject newWeapon = new GameObject {name = $"Weapon_{_item.id}l"};
                    
                    _weapon = newWeapon.AddComponent<WeaponController>();
                    _weapon.Init(_item);
                    _item.WeaponController = _weapon;
                }
                else
                {
                    float nextDamage = _item.damage;
                    int nextCount = 0;

                    nextDamage += _item.damage * _item.damages[_item.level];
                    nextCount += _item.counts[_item.level];

                    _weapon = _item.WeaponController;
                    _weapon.LevelUp(nextDamage, nextCount);
                }
                break;
            case "glove":
            case "shoe":
                if (_item.level == 0)
                {
                    GameObject newGear = new GameObject {name = $"Ggear_{_item.id}l"};
                    _gearController = newGear.AddComponent<GearController>();
                    _gearController.Init(_item);
                    
                    _item.GearController = _gearController;
                }
                else
                {
                    float nextRate = _item.damages[_item.level];
                    _gearController = _item.GearController;
                    _gearController.LevelUp(nextRate);
                }
                break;
            case "heal":
                Managers.Game.GetPlayer.Stat.Hp = Managers.Game.GetPlayer.Stat.MaxHp;
                break;
        }
        
        if(_item.type != "heal")
            _item.level++;
        
        if (_item.level >= _item.damages.Length)
        {
            GetButton((int)Buttons.Button).interactable = false;
            _item.maxLevel = true;
            _item.level = _item.damages.Length;
        }
        else
            RefreshUI();   
        
        //  주석 풀어야함
        Managers.Game.Resume();
        Managers.Resource.Destroy(transform.parent.parent.gameObject);
    }

    public void SetInfo(Item item)
    {
        _item = item;
        Init();
        RefreshUI();
    }

    void RefreshUI()
    {
        if (_item.itemType != Item.ItemType.Heal)
            GetText((int)Texts.LvText).text =$"Lv. {_item.level + 1}";    
        else
            GetText((int)Texts.LvText).text =$"";
        
        GetText((int)Texts.NameText).text = $"{_item.name}";
        GetText((int)Texts.DescText).text = $"{_item.damages[_item.level]}% 증가";
        // GetText((int)Texts.DescText).text = string.Format($"{_item.level: D2}");
        OnImgSeting();
    }

    void OnImgSeting()
    {
        string name = _item.icon;
        Sprite icon = Utils.FindSprite("UI", name);
        
        GetImage((int)Images.Image).sprite = icon;
    }
}
