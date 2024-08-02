using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_State_Upgrade : UI_Popup
{
    enum Buttons
    {
        BackButton,
    }
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Debug.Log("UI_State_Upgrade");
        
        SetInfo();
        
        return true;
    }
    
    void SetInfo()
    {
        BindButton(typeof(Buttons));
        //
        // GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnStartButton);
        GetButton((int)Buttons.BackButton).gameObject.BindEvent(OnBackButton);
    }
    
    void OnBackButton()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_TitlePopup>();
    }
}