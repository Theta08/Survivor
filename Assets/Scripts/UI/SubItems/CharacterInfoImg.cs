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
        RefreshUI();
       
        return true;
    }
    
    void RefreshUI()
    {
        GameManager.SelectIdEvent -= SelectInfo;
        GameManager.SelectIdEvent += SelectInfo;
    }
    
    void SelectInfo(int id)
    {
        if (id == -1)
            return;
        
        string name = "Stand 0";
        // 캐릭터 스프라이트 세팅
        GetImage((int)Images.Image).sprite = Utils.FindSprite($"Farmer {id}", $"{name}");
   
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
    }

    public void DeleteEvent()
    {
        GameManager.SelectIdEvent -= SelectInfo;
    }
}
