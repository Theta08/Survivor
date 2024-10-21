using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        
        float timer = Managers.Game.GameTime;
        int min = Mathf.FloorToInt(timer / 60);
        int sec = Mathf.FloorToInt(timer % 60);
        
        // 결과 보기 및 정지
        Managers.Game.ShowResult();
        
        GetText((int)Texts.KillText).text = $"x {Managers.Game.SaveData.Kill}";
        GetText((int)Texts.TimeText).text = string.Format($"시간 : {min :D2} : {sec :D2}");
        GetText((int)Texts.MoneyText).text = $"<sprite=16> {Managers.Game.GetMoney}";
        
        
        return true;
    }

    void OnBackButton()
    {
        Managers.Game.SaveGame();
        Managers.Sound.Play(Define.Sound.Effect, "Select");
        Managers.Sound.Clear();
        SceneManager.LoadScene("LoginScene");
    }
}
