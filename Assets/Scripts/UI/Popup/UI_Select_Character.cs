using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Select_Character : UI_Popup
{
    enum Buttons
    {
        StartButton,
        BackButton,
    }
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Debug.Log("UI_Select_Character");
        
        SetInfo();
        
        return true;
    }
    
    
    void SetInfo()
    {
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnStartButton);
        GetButton((int)Buttons.BackButton).gameObject.BindEvent(OnBackButton);
    }

    void OnStartButton()
    {
        Debug.Log("StartButton");
    }
    
    void OnBackButton()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_TitlePopup>();
    }
}
