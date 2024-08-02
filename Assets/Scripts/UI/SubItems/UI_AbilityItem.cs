using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AbilityItem : UI_Base
{
    enum Texts
    {
        Text_Title,
        Text_Level,
    }

    enum Images
    {
        Image,
    }

    private int _type;
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));
        
        RefreshUI();
        
        gameObject.BindEvent(OnButtonEvent);
        return true;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnButtonEvent()
    {
        Debug.Log($"{gameObject.name} button click!");
        Managers.Game.Resume();
        
        Managers.Resource.Destroy(transform.parent.gameObject.transform.parent.parent.gameObject);
    }

    public void SetInfo(int type)
    {
        _type = type;
        
    }

    void RefreshUI()
    {
        GetText((int)Texts.Text_Title).text = $"{_type} Title";
        GetText((int)Texts.Text_Level).text = $"{_type} Level";
    }
}
