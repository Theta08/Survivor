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

        // Managers.Game.SelectId = -1;
    }

    void OnStartButton()
    {
        if (Managers.Game.SelectId != -1)
        {
            Managers.Sound.Play(Define.Sound.Effect, "Select");
            SceneManager.LoadScene("GameScene");
        }
    }
    
    void OnBackButton()
    {
        Managers.Sound.Play(Define.Sound.Effect, "Select");
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_TitlePopup>();
    }
}
