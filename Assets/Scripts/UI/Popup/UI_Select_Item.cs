using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Select_Item : UI_Popup
{
    enum AbilityItems
    {
        UI_Abilibty_item_1,
        UI_Abilibty_item_2,
        UI_Abilibty_item_3,
    }

    enum Buttons
    {
        ButtonTest,
    }
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        Bind<UI_AbilityItem>(typeof(AbilityItems));
        // BindButton(typeof(Buttons));
        
        Managers.Game.Stop();

        Get<UI_AbilityItem>((int)AbilityItems.UI_Abilibty_item_1).SetInfo(0);
        Get<UI_AbilityItem>((int)AbilityItems.UI_Abilibty_item_2).SetInfo(1);
        Get<UI_AbilityItem>((int)AbilityItems.UI_Abilibty_item_3).SetInfo(2);
        // Get<UI_AbilityItem>((int)AbilityItems.UI_AbilityItem_MaxHp).SetInfo(Define.StatType.MaxHp);

        // GetButton((int)Buttons.ButtonTest).gameObject.BindEvent(Test);
        
        return true;
    }
    
}
