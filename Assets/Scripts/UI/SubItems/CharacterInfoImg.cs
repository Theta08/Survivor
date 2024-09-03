using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        // RefreshUI();
        GameManager.SelectIdEvent += SelectInfo;
        return true;
    }
    
    void RefreshUI()
    {
        GameManager.SelectIdEvent -= SelectInfo;
        GameManager.SelectIdEvent += SelectInfo;
    }
    
    
    void SelectInfo(int id)
    {
        string name = "Stand 0";
        GetImage((int)Images.Image).sprite = Utils.FindSprite($"Farmer {id}", $"{name}");
        GameManager.SelectIdEvent -= SelectInfo;
        // Sprite[]sprites = Resources.LoadAll<Sprite>($"Sprites/Farmer {id}");
        //
        // foreach (Sprite sprite in sprites)
        // {
        //     if (sprite.name == name)
        //     {
        //       GetImage((int)Images.Image).sprite = sprite;
        //         break;
        //     }
        // }
        // error 
        // 다른 팝업 들어갔다가 다시 시도하면 오류 
        // GameManager.SelectIdEvent -= SelectInfo;
    }
}
