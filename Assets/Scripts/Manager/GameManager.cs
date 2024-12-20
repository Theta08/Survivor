using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager
{
    // 플레이어 사망 확인
    // private bool _isLive = true;
    private int _selectId;
    private float _gameTime;
    private float _maxGameTime = 1 * 10f * 60f;
    private float _getMoney = 0;
    
    private PlayerController _player;
    // private GameObject _player;
    private HashSet<GameObject> _monsters = new HashSet<GameObject>();
    private GameData _gameData = new GameData();
    
    public Action<int> OnSpawnEvent;
    public GameData SaveData { get {return _gameData;} set { _gameData = value; } }
    public PlayerController GetPlayer{ get {return _player;} set { _player = value; } }
    public float GameTime { get { return _gameTime;} set { _gameTime = value; } }
    public float MaxGameTime { get { return _maxGameTime;} set { _maxGameTime = value; } }
    public float GetMoney { get { return _getMoney;} set { _getMoney = value; } }
    public int SelectId { get { return _selectId;}
        set
        {
            if (value == -1)
            {
                _selectId = value;
                return;
            }
            
            if (!Managers.Data.CharacterDic[value].isOn)
                return;
            
            SelectIdEvent?.Invoke(value);
            _selectId = value;
        } 
    }

    public delegate void SelectIdSocket(int id);
    public static event SelectIdSocket SelectIdEvent;

    
    public void Init()
    {
        if (SelectIdEvent != null) SelectIdEvent(-1);
    }

    #region Exp
    public void GetExp()
    {
        Managers.Game.SaveData.Exp++;
        Managers.Game.SaveData.Kill++;
        
        int exp = Managers.Game.SaveData.Exp;
        int level = Managers.Game.SaveData.Level;
        int nextExp = Managers.Game.SaveData.NextExp[level];

        if (exp == nextExp)
        {
            Managers.Game.SaveData.Level++;
            Managers.Game.SaveData.Exp = 0;

            // 주석 풀어야함
            Managers.UI.ShowPopupUI<UI_Select_Item>();
        }
    }
    #endregion

    #region Dead -> Money

    public void ShowResult()
    {
        Stop();
        GetMoney = SaveData.Kill * 1 + (int)GameTime;
        SaveData.money += SaveData.Kill * 1 + (int)GameTime;
    }

    #endregion
    
    #region Timer Stop or reStart

    public void Stop()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    #endregion

    #region Save & Load
    public string _path = Application.persistentDataPath + "/SaveData.json";
    
    public void SaveGame()
    {
        string jsonStr = JsonUtility.ToJson(Managers.Game.SaveData);
        // string jsonStr = JsonConvert.SerializeObject(Managers.Game.SaveData);
        File.WriteAllText(_path, jsonStr);
        Debug.Log($"Save Game Completed {_path}");
    }
    
    public bool LoadGame()
    {
        if (File.Exists(_path) == false)
            return false;
    
        string fileStr = File.ReadAllText(_path);
        GameData data = JsonUtility.FromJson<GameData>(fileStr);
    
        if (data != null)
            Managers.Game.SaveData = data;
        
        Debug.Log($"Save Game Load {_path}");
    
        return true;
    }
    #endregion

    #region Monster Spwan

    public Define.ObjectType GetObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.ObjectType.Unknown;
    
        return bc.ObjectType;
    }
    
    public GameObject Spawn(Define.ObjectType type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Define.ObjectType.Monster:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.ObjectType.Player:
                // _player = go;
                break;
            default:
                break;
        }

        return go;
    }

    public void Despawn(GameObject go)
    {
        Define.ObjectType type = GetObjectType(go);
    
        switch (type)
        {
            case Define.ObjectType.Monster:
            {
                if (_monsters.Contains(go))
                {
                    _monsters.Remove(go);
                    if(OnSpawnEvent != null)
                        OnSpawnEvent.Invoke(-1);
                }
            }
                break;
            case Define.ObjectType.Player:
            {
                if (_player == go)
                    _player = null;
            }
                break;
        }
        
        Managers.Resource.Destroy(go);
    }
    
    #endregion
}
