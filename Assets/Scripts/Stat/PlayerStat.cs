using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat 
{
    // public virtual int Atk { get { return _atk;} set { _atk = value; } }
    
    void Start()
    {
        Init();
    }
    
    void Init()
    {
        CharacterStat characterStat = Managers.Data.CharacterStatsDic[Managers.Game.SelectId];
        UpgradeData upgradeData = Managers.Data.UpgradeData;
        
        Hp = characterStat.hp + upgradeData.hp;
        MaxHp = Hp;
        Spd = characterStat.spd + upgradeData.spd;
        Atk = characterStat.atk + upgradeData.atk;
        AtkSpd = characterStat.atkSpd + upgradeData.atkSpd;

    }
}
