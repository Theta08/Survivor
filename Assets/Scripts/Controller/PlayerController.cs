using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private WeaponController _weaponController;
    private Vector2 _inputVec;

    public Hand[] Hands;
    public Vector2 InputVec { get { return _inputVec; } set { _inputVec = value; } }
    public Scanner Scanner;
    void Awake()
    {
        Managers.Game.GetPlayer = this;
        Stat = gameObject.GetOrAddComponent<PlayerStat>();
        Scanner = GetComponent<Scanner>();
        
        // 빼야함
        for (int i = 0; i < 4; i++)
        {
            _animCon[i] = Managers.Resource.Load<RuntimeAnimatorController>($"Animations/AcPlayer {i}");
        }

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
        
        _isLive = true;
        
        ObjectType = Define.ObjectType.Player;

        Hands = GetComponentsInChildren<Hand>(true);
        
        // 기본공격 테스트
        Item item = Managers.Data.ItemDatasDic[0];
        GameObject newWeapon = new GameObject();
        
        _weaponController = newWeapon.AddComponent<WeaponController>();
        _weaponController.Init(item);
        // 미완
        _animator.runtimeAnimatorController = _animCon[Managers.Game.SelectId];
        return true;
    }

    void OnMove(InputValue value)
    {
        if(!Managers.Game.IsLive)
            return;
        
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
                transform.GetChild(i).gameObject.SetActive(false);
            
            _animator.SetBool("Dead", true);
            // popup 호출
            Managers.UI.ShowPopupUI<UI_Dead_Popup>();
        }
    }
}
