using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterData
{
    public int id;
    public int hp;
    public int atk;
    public float spd;
}

public class MonsterDataLoad : ILoader<int, MonsterData>
{
    public List<MonsterData> Monsters = new List<MonsterData>();
    
    public Dictionary<int, MonsterData> MakeDict()
    {
        Dictionary<int, MonsterData> dic = new Dictionary<int, MonsterData>();

        foreach (MonsterData stat in Monsters)
        {
            dic.Add(stat.id, stat);
        }

        return dic;
    }
}
