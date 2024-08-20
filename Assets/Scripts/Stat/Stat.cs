using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    private int id;
    [SerializeField]
    private int _hp;
    [SerializeField]
    private int _atk;
    [SerializeField]
    private float _atkSpd;
    [SerializeField]
    private float _spd;
    
    public int Id { get; set; }
    public int Hp { get { return _hp;} set { _hp = value; } }
    public int Atk { get { return _atk;} set { _atk = value; } }
    
    public float AtkSpd { get { return _atkSpd;} set { _atkSpd = value; } }
    public float Spd { get { return _spd;} set { _spd = value; } }
    
    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    void Init()
    {
        CharacterStat characterStat = Managers.Data.CharacterStatsDic[Managers.Game.SelectId];
        UpgradeData upgradeData = Managers.Data.UpgradeData;
        
        Hp = characterStat.hp + upgradeData.hp;
        Spd = characterStat.spd + upgradeData.spd;
        Atk = characterStat.atk + upgradeData.atk;
        AtkSpd = characterStat.atkSpd + upgradeData.atkSpd;

    }
}
