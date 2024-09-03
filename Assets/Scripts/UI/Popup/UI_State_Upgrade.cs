using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_State_Upgrade : UI_Popup
{
    enum Buttons
    {
        BackButton,
    }
    enum Texts
    {
        MoneyText,
    }
    private GameObject _panel;
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        SetInfo();
        
        return true;
    }

    public void Refresh()
    {
        GetText((int)Texts.MoneyText).text = $"<sprite=16>{Managers.Game.SaveData.money}";
    }
    void SetInfo()
    {
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        
        // GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnStartButton);
        GetButton((int)Buttons.BackButton).gameObject.BindEvent(OnBackButton);
        GetText((int)Texts.MoneyText).text = $"<sprite=16>{Managers.Game.SaveData.money}";
    }
    
    void OnBackButton()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_TitlePopup>();
    }
}
