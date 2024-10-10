using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
   Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
   public Dictionary<int, Character> CharacterDic { get; set; } = new Dictionary<int, Character>();
   public Dictionary<int, CharacterStat> CharacterStatsDic { get; set; } = new Dictionary<int, CharacterStat>();
   public Dictionary<int, MonsterData> MonsterStatsDic { get; set; } = new Dictionary<int, MonsterData>();
   public Dictionary<int, Item> ItemDatasDic { get; set; } = new Dictionary<int, Item>();
   public UpgradeData UpgradeData { get; set; } = new UpgradeData();

   public List<MonsterData> MonsterDataList = new List<MonsterData>();

   public void Init()
   {
      CharacterDic = LoadJson<CharactersDataLoad, int, Character>("Characters").MakeDict();
      MonsterStatsDic = LoadJson<MonsterDataLoad, int, MonsterData>("Monsters").MakeDict();
      CharacterStatsDic = LoadJson<CharacterData, int, CharacterStat>("Stat").MakeDict();
      ItemDatasDic = LoadJson<ItemsDatasLoad, int, Item>("Items").MakeDict();
      UpgradeData = LoadJson<UpgradeData>("UpgradeStat");

      MonsterDataList = DictionaryToList(MonsterStatsDic);
   }
   
   Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
   {
      TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
      return JsonUtility.FromJson<Loader>(textAsset.text);
   }

   public T LoadJson<T>(string path)
   {
      TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
      return JsonUtility.FromJson<T>(textAsset.text);
   }
   
   public List<T> DictionaryToList<T>(Dictionary<int, T> dictionary)
   {
      List<T> list = new List<T>();
      
      foreach (var data in dictionary)
         list.Add(data.Value);
      return list;
   }
}
