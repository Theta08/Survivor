using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterStat
{
    public int id;
    public int hp;
    public int atk;
    public float atkSpd;
    public float spd;
}

[Serializable]
public class CharacterData : ILoader<int, CharacterStat>
{
    public List<CharacterStat> Stat = new List<CharacterStat>();
    
    public Dictionary<int, CharacterStat> MakeDict()
    {
        Dictionary<int, CharacterStat> dic = new Dictionary<int, CharacterStat>();

        foreach (CharacterStat stat in Stat)
        {
            dic.Add(stat.id, stat);
        }

        return dic;
    }
}
