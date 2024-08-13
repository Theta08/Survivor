using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoImg : UI_Base
{
    enum Images
    {
        Image,
    }
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        
        RefreshUI();
        
        return true;
    }
    
    void RefreshUI()
    {
        GameManager.SelectIdEvent += SelectInfo;
    }
    
    
    void SelectInfo(int id)
    {
        string name = "Stand 0";
        
        Sprite[] sprites = Resources.LoadAll<Sprite>($"Sprites/Farmer {id}");

        foreach (Sprite sprite in sprites)
        {
            if (sprite.name == name)
            {
                GetImage((int)Images.Image).sprite = sprite;
                break;
            }
        }
        // error 
        // 다른 팝업 들어갔다가 다시 시도하면 오류 
        // GameManager.SelectIdEvent -= SelectInfo;
    }
}
