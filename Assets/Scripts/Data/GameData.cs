using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class GameData
{
    public UpgradeData CharacterUpgrade = new UpgradeData();
    public List<Character> Characters = new List<Character>();
    
    public int level;
    public int kill;
    public int exp;
    // 임시
    // public int[] nextExp = {3, 5, 7, 12, 15, 20};
    public int[] nextExp = {2, 3, 3, 3, 3, 3};
}
