using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class WeaponController : MonoBehaviour
{
    public int id;
    public float speed;
    
    private float _damage;
    private int _count;
    private float _timer;
    private Item _item;
    // void Start()
    // {
    //     Item item = new Item();
    //     Init(item);
    // }

    void Update()
    {
        switch (id)
        {
            case 0:
            case 2:
                _timer += Time.deltaTime;

                if (_timer > speed)
                {
                    _timer = 0;
                    if(id == 0)
                        Fire();
                    else
                        ScanFire();
                }
                break;
            case 1:
                transform.Rotate(Vector3.forward * speed * Time.deltaTime);
                break;
            case 4:
                break;
            default:
                break;
        }
    }
    
    public void LevelUp(float damage, int count)
    {
        _damage = damage;
        _count += count;
        
        if( id == 1)
            Bacth();
      
        Managers.Game.GetPlayer.
            BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }
    public void Init(Item item)
    {
        
        gameObject.name = "Weapon_" + item.id; // data.itemId;
        transform.parent = Managers.Game.GetPlayer.transform;
        transform.localPosition = Vector3.zero;
        
        _item = item;
        id = item.id;
        _damage = item.damage;
        _count = item.count;
        
        
        switch (id)
        {
            case 0:
                speed = Managers.Game.GetPlayer.Stat.AtkSpd;
                break;
            case 1:
                speed = 150 * Managers.Game.GetPlayer.Stat.AtkSpd;
                Bacth();
                break;
            case 2:
                speed = 0.5f;
                // ScanFire();
                break;

        }
        // Hand Set
        Hand hand = Managers.Game.GetPlayer.Hands[(int)item.itemType];
        item.handSprite = Utils.FindSprite("Props", item.hand);
        hand.gameObject.SetActive(true);
        hand._sprite.sprite = item.handSprite;
        
        // 특정함수를 자식들에게 방송
        Managers.Game.GetPlayer.
            BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    private void Bacth()
    {
        
        for (int i = 0; i < _count; i++)
        {
            Transform bullet;
            if (i < transform.childCount)
                bullet = transform.GetChild(i);
            else
            {
                
                bullet = Managers.Resource.Instantiate($"Weapon/{_item.hand}").transform;
                bullet.parent = transform;
                // Managers.Game.Spawn(Define.ObjectType.Unknown, go);
            }
            
            // 초기화
            bullet.localPosition = Vector3.zero;
            bullet.transform.localRotation = Quaternion.identity;
            
            Vector3 rotVec = Vector3.forward * 360 * i / _count;
            
            // rigidbody 사용 했는지 확인 rigidbody사용 했으면 이상함 
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.transform.up * 1.2f, Space.World);
            bullet.GetComponent<Bullet>().Init(_damage, -1, Vector3.zero); // -1 is Infinity Per
        }
    }
    private void ScanFire()
    {
        if (!Managers.Game.GetPlayer.Scanner.nearestTarget)
            return;

        Vector3 targetPos = Managers.Game.GetPlayer.Scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;
        
        GameObject go = Managers.Resource.Instantiate("Weapon/Bullet 4");
        // 위치
        go.transform.position = transform.position;
        // 회전
        go.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        go.GetComponent<Bullet>().Init(_damage, _count, dir); 
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
        
        _damage = Managers.Game.GetPlayer.Stat.Atk;
        
        go.GetComponent<Bullet>().Init(_damage, _count, dir); 
        go.GetComponent<TimerDestory>().Init(1.5f);
    }
    
}
