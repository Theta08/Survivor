using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    public override float Hp { 
        get { return _hp;}
        set
        {
            _hp = value;

            if (_hp > 0)
            {
                // Live
                gameObject.GetComponent<BaseController>().Animator.SetTrigger("Hit");
            }
      
        } 
    }
    private void Awake()
    {
        Init();
    }
    private void OnEnable()
    {
        Init();
    }

    public void Init(int seledId = 0)
    {
        MonsterData monster = Managers.Data.MonsterStatsDic[seledId];
        
        if (monster == null)
            return;
        
        Id = monster.id;
        MaxHp = monster.hp;
        Hp = monster.hp;
        Spd = monster.spd;
        Atk = monster.atk;

    }
}
