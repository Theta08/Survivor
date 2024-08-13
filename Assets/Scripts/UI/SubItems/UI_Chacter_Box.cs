using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Chacter_Box : UI_Base
{
 
    enum Images
    {
        Icon,
    }
    
    private Sprite _icon;
    private Image _image;
    private int type = -1;
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        
        RefreshUI();
        
        
        gameObject.BindEvent(OnCharacterClick);
        
        return true;
    }
    
    public void Setting(int num)
    {
        type = num;
        
        // 오류
        // RefreshUI();
    }

    void RefreshUI()
    {
        if (type == -1)
            return;
        
        OnImage();
        
        GetImage((int)Images.Icon).sprite = _icon;
    }

    // 이미지  세팅
    void OnImage()
    {
        string name = "Stand 0";
        
        // Data에서 값이 있으면 화면 보이게 하기
        // type과 data 인덱스는 동일해야함
        if (!Managers.Data.CharacterDic[type].isOn || type == -1)
            return;
        
        Sprite[] sprites = Resources.LoadAll<Sprite>($"Sprites/Farmer {type}");

        foreach (Sprite sprite in sprites)
        {
            if (sprite.name == name)
            {
                _icon = sprite;
                break;
            }
        }
    }

    void OnCharacterClick()
    {
        Managers.Game.SelectId = type;
        
        // Debug.Log($"select Id {Managers.Game.SelectId}");
    }
    
}
