using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager
{
    // 플레이어 사망 확인
    private bool _isLive = true;
    private int _selectId;
    private float _gameTime;
    private float _maxGameTime = 2 * 10f * 60;
    
    private PlayerController _player;
    // private GameObject _player;
    private HashSet<GameObject> _monsters = new HashSet<GameObject>();
    private GameData _gameData = new GameData();
    
    public Action<int> OnSpawnEvent;
    public GameData SaveData { get {return _gameData;} set { _gameData = value; } }
    public PlayerController GetPlayer{ get {return _player;} set { _player = value; } }
    public bool IsLive { get { return _isLive;} set { _isLive = value; } }
    public float GameTime { get { return _gameTime;} set { _gameTime = value; } }
    public float MaxGameTime { get { return _maxGameTime;} set { _maxGameTime = value; } }
    
    public int SelectId { get { return _selectId;}
        set
        {
            SelectIdEvent?.Invoke(value);
            _selectId = value;
        } 
    }

    public delegate void SelectIdSocket(int id);
    public static event SelectIdSocket SelectIdEvent;
    
    public void Init()
    {
        if (SelectIdEvent != null) SelectIdEvent(SelectId);
    }

    #region Exp
    public void GetExp()
    {
        Managers.Game.SaveData.exp++;
        Managers.Game.SaveData.kill++;
        
        int exp = Managers.Game.SaveData.exp;
        int level = Managers.Game.SaveData.level;
        int nextExp = Managers.Game.SaveData.nextExp[level];

        if (exp == nextExp)
        {
            Managers.Game.SaveData.level++;
            Managers.Game.SaveData.exp = 0;

            // 주석 풀어야함
            Managers.UI.ShowPopupUI<UI_Select_Item>();
        }
    }
    #endregion
    
    #region Timer Stop or reStart

    public void Stop()
    {
        _isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        _isLive = true;
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
