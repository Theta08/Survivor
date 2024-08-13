using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Cha : MonoBehaviour
{
    private AllData data;
    public Dictionary<int, Character> CharacterDic { get; set; }
    
    private void Awake()
    {
        // string _path = $"{Application.dataPath}/Resources/Data/Characters.json";
        // string _path = $"{Application.dataPath}/Resources/Data/Stat.json";
        // string fileStr = File.ReadAllText(_path);
        
        // data = JsonUtility.FromJson<AllData>(fileStr);
        
        // AllData data = JsonUtility.FromJson<AllData>(fileStr);
        //
        //
        // foreach (var datas in data.Stat)
        // {
        //     Debug.Log($"id 1 : {datas.hp}");
        // }

    }

    // private void Start()
    // {
    //     Dictionary<int, CharacterStat> dictionary = Managers.Data.CharacterStatsDic;
    //     CharacterStat stat = dictionary[0];
    //     
    //     Debug.Log($"atk :{stat.atk} hp : {stat.hp}");
    // }
}

[Serializable]
public class AllData
{
    public MapData[] Characters;
    public Stat[] Stat;
}

[Serializable]
public class MapData
{
    public int id;
    public int count;
}

[Serializable]
public class Stat
{
    public int hp;
    public int atk;
    public float atkSpd;
    public float spd;
}