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
        Debug.Log($"id = {Managers.Game.SelectId}");
        
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
        if (Managers.Game.SelectId != -1)
        {
            Managers.Sound.Play(Define.Sound.Effect, "Select");
            SceneManager.LoadScene("GameScene");
        }
    }
    
    void OnBackButton()
    {
        // 델리게이트 이벤트 삭제
        // 삭제x시 오브젝트가 파괴 되었는데도 이벤트가 있어 
        // 다시 캐릭창 들어 왔을때 선택시 에러가 난다.
        GameObject go = gameObject.transform.Find("InfoPanel").gameObject;
        go.transform.Find("CharacterInfoImg").GetComponent<CharacterInfoImg>().DeleteEvent();
        go.transform.Find("CharacterStatPanel").GetComponent<CharacterStatPanel>().DeleteEvent();
        
        Managers.Sound.Play(Define.Sound.Effect, "Select");
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_TitlePopup>();
        
    }
}
