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
    
    private VirtualCamera _camera;
    private PlayerController _player;
    // private GameObject _player;

    private GameData _gameData = new GameData();
    
    public Action<int> OnSpawnEvent;
    public GameData SaveData { get {return _gameData;} set { _gameData = value; } }
    public VirtualCamera Camera{ get {return _camera;} set { _camera = value; } }
    public PlayerController GetPlayer{ get {return _player;} set { _player = value; } }
    public bool IsLive { get { return _isLive;} set { _isLive = value; } }
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
}
