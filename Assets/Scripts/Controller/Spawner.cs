using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private int _monsterCount;
    private int _reserveCount;
    [SerializeField]
    private int _keepMonsterCount;
    
    private float timer;
    private float _spwanTime = 0.7f;
    private int _level;
    
    private Transform[] spawnPoint;

    public void AddMonsterCount(int value) { _monsterCount += value;}
    public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }
    
    // Start is called before the first frame update
    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
        
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        _level = Mathf.FloorToInt((Managers.Game.GameTime / 10f));
        // timer > _spwanTime &&
        if ( timer >  _spwanTime && _reserveCount + _monsterCount < _keepMonsterCount)
        {
            StartCoroutine("ReserveSpawn");
            timer = 0;
        }
    }

    IEnumerator ReserveSpawn()
    {
        // int rag = Random.Range(65, 67);
        int rag = 65;
        char cRag = (char)rag;
        string monsterName = $"Enemy/Enemy {cRag}";
        
        _reserveCount++;
        
        yield return new WaitForSeconds(_spwanTime); 
        
        GameObject go = Managers.Game.Spawn(Define.ObjectType.Monster, monsterName);
        
        _reserveCount--;
        
        // GameObject go = Managers.Resource.Instantiate(monsterName);
        go.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].transform.position;
    }
}
