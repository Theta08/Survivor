using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradeData 
{
    public int hp;
    public int atk;
    public int atkSpd;
    public int spd;
}
[Serializable]
public class CharacterUpgrade : ILoader<int, UpgradeData>
{
    public UpgradeData UpgradeStat = new UpgradeData();
    
    public Dictionary<int, UpgradeData> MakeDict()
    {
        Dictionary<int, UpgradeData> dic = new Dictionary<int, UpgradeData>();
        
            dic.Add(0, UpgradeStat);
        

        return dic;
    }
}
