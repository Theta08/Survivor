using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Item : UI_Popup
{
    enum Objects
    {
        Button,
        Image,
    }
    enum Images
    {
        Image,
    }
    
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        // BindText(typeof(Texts));
        BindImage(typeof(Images));
        
        Bind<Object>(typeof(Objects));
        
        gameObject.BindEvent(OnClickEvent);
        
        return true;
    }

    private void OnClickEvent()
    {
        Debug.Log("Button Click!");
    }
}
