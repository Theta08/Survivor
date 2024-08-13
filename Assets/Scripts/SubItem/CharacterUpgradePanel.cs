using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUpgradePanel : UI_Base
{
    enum Objects
    {
        HPPanel,
        SpdPanel,
        AtkPanel,
        AtkSpdPanel,
    }
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<StateAbilityItem>(typeof(Objects));

        Get<StateAbilityItem>((int)Objects.HPPanel).UserUpgradeStat = Define.UserUpgradeStat.HPPanel;
        Get<StateAbilityItem>((int)Objects.SpdPanel).UserUpgradeStat = Define.UserUpgradeStat.SpdPanel;
        Get<StateAbilityItem>((int)Objects.AtkPanel).UserUpgradeStat = Define.UserUpgradeStat.AtkPanel;
        Get<StateAbilityItem>((int)Objects.AtkSpdPanel).UserUpgradeStat = Define.UserUpgradeStat.AtkSpdPanel;
        
        return true;
    }
}
