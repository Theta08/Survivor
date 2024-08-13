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

        SetInfo();
        LoadDataCheack();
        
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

    void LoadDataCheack()
    {
        if (!Managers.Game.LoadGame())
        {
            Managers.Game.SaveData.Characters = 
                Managers.Data.DictionaryToList(Managers.Data.CharacterDic);
        
            Managers.Game.SaveData.CharacterUpgrade = 
                Managers.Data.UpgradeData;
            
            Managers.Game.SaveGame();
        }
        else
        {
            Managers.Game.LoadGame();
        }
        
    }
}
