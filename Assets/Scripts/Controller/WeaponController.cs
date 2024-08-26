using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float _damage;
    public float speed;
    public int _count;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0;
                    Fire();
                }
                break;
        }
        // test
        if(Input.GetButtonDown("Jump"))
            LevelUp(20, 4);
    }
    
    public void LevelUp(float damage, int count)
    {
        _damage = damage;
        _count += count;
        
        if( id == 0)
            Bacth();
    }
    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Bacth();
                break;
            default:
                speed = 0.5f;
                break;
        }
    }

    private void Bacth()
    {
        for (int i = 0; i < _count; i++)
        {
            Transform go;
            if (i < transform.childCount)
                go = transform.GetChild(i);
            
            else
            {
                go = Managers.Resource.Instantiate("Weapon/Bullet 3").transform;
                go.parent = transform;
                // Managers.Game.Spawn(Define.ObjectType.Unknown, go);
            }
            
            // 초기화
            go.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            
            Vector3 rotVec = Vector3.forward * 360 * i / _count;
            
            go.Rotate(rotVec);
            go.Translate(go.transform.up * 1.5f, Space.World);
            go.GetComponent<Bullet>().Init(_damage, -1, Vector3.zero); // -1 is Infinity Per
        }
    }
    
    private void Fire()
    {
        // 방향 설정
        Vector3 mousePos = Input.mousePosition;
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos + new Vector3(0, 0, 10));
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;
        
        GameObject go = Managers.Resource.Instantiate("Weapon/Bullet 3");
        // 위치
        go.transform.position = transform.position;
        // 회전
        go.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        
        go.GetComponent<Bullet>().Init(_damage, 0, dir); 
    }
}
