using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TitlePopup : UI_Popup
{
    enum Buttons
    {
        StartButton,
        StateButton,
        GachaButton,
    }
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Debug.Log("UI_TitlePopup");

        SetInfo();
        
        return true;
    }

    void SetInfo()
    {
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnStartButton);
        GetButton((int)Buttons.StateButton).gameObject.BindEvent(OnItemButton);
        GetButton((int)Buttons.GachaButton).gameObject.BindEvent(OnGachaButton);
    }

    void OnStartButton()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_Select_Character>();
    }
    
    void OnItemButton()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_State_Upgrade>();
    }
    
    void OnGachaButton()
    {
        Debug.Log("GachaButton click!");
    }
}