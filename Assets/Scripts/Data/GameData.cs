using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class GameData
{
    public UpgradeData CharacterUpgrade = new UpgradeData();
    public List<Character> Characters = new List<Character>();
    
    // public int hp;
    // public int atk;
    // public float atkSpd;
    // public float spd;
}
