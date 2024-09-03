using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class GameData
{
    public UpgradeData CharacterUpgrade = new UpgradeData();
    public List<Character> Characters = new List<Character>();
    public int money;
    
    private int _level;
    private int _kill;
    private int _exp;
    private int[] _nextExp = {2, 3, 3, 3, 3, 5, 7, 10, 13, 17, 20, 25, };
    // 임시
    // private int[] _nextExp = {3, 5, 7, 12, 15, 20};
    
    
    public int Level { get { return _level;} set { _level = value; } }
    public int Kill { get { return _kill;} set { _kill = value; } }
    public int Exp { get { return _exp;} set { _exp = value; } }
    public int[] NextExp { get { return _nextExp;} set { _nextExp = value; } }
}
