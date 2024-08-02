using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    // 플레이어 사망 확인
    private bool _isLive = true;
    private GameObject _player;
    
    public Action<int> OnSpawnEvent;
    public GameObject GetPlayer{ get {return _player;} set { _player = value; } }
    public bool IsLive { get { return _isLive;} set { _isLive = value; } }
    
    public void Init() { }

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

}
