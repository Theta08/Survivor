using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private WeaponController _weaponController;
    [SerializeField]
    private Vector2 _inputVec;

    public Hand[] Hands;
    public Vector2 InputVec { get { return _inputVec; } set { _inputVec = value; } }
    
    void Awake()
    {
        Init();
    }
    
    // void Start()
    // {
    //     Init();
    // }
    
    private void FixedUpdate()
    {
        Vector2 nextVec = InputVec * Stat.Spd * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + nextVec);
    }

    private void LateUpdate()
    {
        _animator.SetFloat("Speed", InputVec.magnitude);
        if (InputVec.x != 0)
            _sprite.flipX = InputVec.x < 0;
        
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        // Init 적용 후 Stat.Init 적용
        Stat = gameObject.GetOrAddComponent<PlayerStat>();
        _isLive = true;
        
        ObjectType = Define.ObjectType.Player;
        Managers.Game.GetPlayer = this;

        Hands = GetComponentsInChildren<Hand>(true);
        
        // 기본공격 테스트
        Item item = new Item();
        GameObject newWeapon = new GameObject();
        
        item.id = -1;
        item.itemType = Item.ItemType.Range;
        item.hand = "Weapon 3";
        item.handSprite = Utils.FindSprite("Props", item.hand);
        
        _weaponController = newWeapon.AddComponent<WeaponController>();
        _weaponController.speed = Stat.AtkSpd;
        _weaponController.Init(item);
        return true;
    }

    void OnMove(InputValue value)
    {
        // normalized 사용중...
        _inputVec = value.Get<Vector2>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(!Managers.Game.IsLive)
            return;

        Stat.Hp -= Time.deltaTime * other.gameObject.GetOrAddComponent<Stat>().Atk;

        if (Stat.Hp <= 0)
        {
            
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            
            _animator.SetBool("Dead", true);
            Managers.Game.IsLive = false;
        }
    }
}
