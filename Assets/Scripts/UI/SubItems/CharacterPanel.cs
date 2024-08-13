using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : UI_Base
{
    enum CharterBox
    {
        CharterBox_1,
        CharterBox_2,
        CharterBox_3,
        CharterBox_4,
    }
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<UI_Chacter_Box>(typeof(CharterBox));

        RefreshUi();
        
        return true;
    }

    void RefreshUi()
    {
        Get<UI_Chacter_Box>((int)CharterBox.CharterBox_1).
            Setting((int)CharterBox.CharterBox_1);
        Get<UI_Chacter_Box>((int)CharterBox.CharterBox_2).
            Setting((int)CharterBox.CharterBox_2);
        Get<UI_Chacter_Box>((int)CharterBox.CharterBox_3).
            Setting((int)CharterBox.CharterBox_3);
        Get<UI_Chacter_Box>((int)CharterBox.CharterBox_4).
            Setting((int)CharterBox.CharterBox_4);
    }
}

