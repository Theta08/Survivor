using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    private void Start()
    {
        Init();
    }
    private void OnEnable()
    {
        Init();
    }

    void Init()
    {
        MonsterData monster = Managers.Data.MonsterStatsDic[0];
        
        if (monster == null)
            return;
        
        Id = monster.id;
        Hp = monster.hp;
        Spd = monster.spd;
        Atk = monster.atk;

    }
}
