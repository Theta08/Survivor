using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Dead_Popup : UI_Popup
{
    enum Texts
    {
        KillText,
        TimeText,
        MoneyText,
    }
    enum Buttons
    {
        TitleButton,
    }
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        
        GetButton((int)Buttons.TitleButton).gameObject.BindEvent(OnBackButton);
        
        GetText((int)Texts.KillText).text = $"x {Managers.Game.SaveData.kill}";
        GetText((int)Texts.TimeText).text = $"{Managers.Game.GameTime}";
        GetText((int)Texts.MoneyText).text = $"{Managers.Game.GameTime}";
        // SetInfo();
        
        return true;
    }

    void OnBackButton()
    {
        Debug.Log("back");
    }
}
