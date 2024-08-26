using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    private int _monsterCount = 0;
    private int _reserveCount = 0;
    private int _keepMonsterCount = 0;

    private float _spawnTime = 0.7f;
    
    public void AddMonsterCount(int value) { _monsterCount += value; }
    public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }
    // Start is called before the first frame update
    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (_reserveCount + _monsterCount < _keepMonsterCount)
            StartCoroutine("ReserveSpawn");
    }
    
    IEnumerator ReserveSpawn()
    {
        string monsterName = "Enemies/Goblin";
        _reserveCount++;
        
        yield return new WaitForSeconds(_spawnTime); 
        
        Managers.Game.Spawn(Define.ObjectType.Monster, monsterName);
        
        _reserveCount--;
    }
}
