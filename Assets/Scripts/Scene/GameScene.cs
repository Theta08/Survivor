using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField] 
    private int _spawnMonseter;
    private bool _stop = false;
    
    private void Awake()
    {
        // 카메라 커트롤 바이딩
        Camera.main.gameObject.GetOrAddComponent<CameraController>();
    }

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        SceneType = Define.Scene.Game;
        _spawnMonseter = 8;

        Managers.Game.GameTime = 0;
        Managers.Game.Resume();
        
        Spawner spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        spawner.SetKeepMonsterCount(_spawnMonseter);

        Managers.UI.ShowSceneUI<UI_HUD>();
        Managers.UI.ShowSceneUI<UI_FollowHpBar>();
        return true;
    }

    private void Update()
    {
        Managers.Game.GameTime += Time.deltaTime;
        // 승리시 팝업 변경해야함
        if (Managers.Game.GameTime >= Managers.Game.MaxGameTime && !_stop)
        {
            Managers.Game.GameTime = Managers.Game.MaxGameTime;
            // 승리 팝업 변경 필요
            Managers.UI.ShowPopupUI<UI_Dead_Popup>();
            _stop = true;
        }
    }
}
