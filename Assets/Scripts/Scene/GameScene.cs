using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        SceneType = Define.Scene.Game;
        
        Debug.Log($"{ Managers.Game.SelectId}");
        Debug.Log($"atk { Managers.Game.SaveData.CharacterUpgrade.atk}");
       
        return true;
    }
}
