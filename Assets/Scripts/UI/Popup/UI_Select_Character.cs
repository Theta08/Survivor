using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Select_Character : UI_Popup
{
    enum Objects
    {
        CharacterPanel,
    }
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
        Bind<CharacterPanel>(typeof(Objects));
        
        GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnStartButton);
        GetButton((int)Buttons.BackButton).gameObject.BindEvent(OnBackButton);

        Get<CharacterPanel>((int)Objects.CharacterPanel).GetOrAddComponent<CharacterPanel>();
    }

    void OnStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    void OnBackButton()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_TitlePopup>();
    }
}
