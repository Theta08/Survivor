using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField] 
    private int _spawnMonseter;
    
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        SceneType = Define.Scene.Game;
        _spawnMonseter = 3;
        
        // 카메라 커트롤 바이딩
        Camera.main.gameObject.GetOrAddComponent<CameraController>();

        Spawner spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        spawner.SetKeepMonsterCount(_spawnMonseter);

        Managers.UI.ShowSceneUI<UI_HUD>();
        Managers.UI.ShowSceneUI<UI_FollowHpBar>();
        return true;
    }

    private void Update()
    {
        Managers.Game.GameTime += Time.deltaTime;
        
        if (Managers.Game.GameTime >= Managers.Game.MaxGameTime)
        {
            Managers.Game.GameTime = Managers.Game.MaxGameTime;
            Managers.UI.ShowPopupUI<UI_Dead_Popup>();
        }
    }
}
