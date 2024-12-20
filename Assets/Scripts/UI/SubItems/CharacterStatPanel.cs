using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatPanel : UI_Base
{
    enum Texts
    {
        HpText,
        SpeedText,
        AtkSpeedText,
        AtkText,
    }
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        RefreshUI();
        
        return true;
    }

    void SelectInfo(int id)
    {
        if (id == -1)
            return;
        
        GetText((int)Texts.HpText).text = Managers.Data.CharacterStatsDic[id].hp.ToString();
        GetText((int)Texts.SpeedText).text = Managers.Data.CharacterStatsDic[id].spd.ToString();
        GetText((int)Texts.AtkText).text = Managers.Data.CharacterStatsDic[id].atk.ToString();
        GetText((int)Texts.AtkSpeedText).text = Managers.Data.CharacterStatsDic[id].atkSpd.ToString();
    }
    void RefreshUI()
    {
        GameManager.SelectIdEvent -= SelectInfo;
        GameManager.SelectIdEvent += SelectInfo;
    }
    
    public void DeleteEvent()
    {
        GameManager.SelectIdEvent -= SelectInfo;
    }
}
