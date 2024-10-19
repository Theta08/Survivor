using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    CameraController _camera;
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
        
        // 
        if (Camera.main != null) 
            _camera = Camera.main.gameObject.GetComponent<CameraController>();
        
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
        if (Mathf.Abs(transform.position.x) > _camera.MapSize.x - 0.5)
        {
            if (transform.position.x < 0)
                transform.position = new Vector2(-_camera.MapSize.x + 0.5f, transform.position.y);
            else
                transform.position = new Vector2(_camera.MapSize.x - 0.5f, transform.position.y);
            
            return;
        }
        else if (Mathf.Abs(transform.position.y) > _camera.MapSize.x - 0.5)
        {
            if (transform.position.y < 0)
                transform.position = new Vector2(transform.position.x, -_camera.MapSize.x + 0.5f);
            else
                transform.position = new Vector2(transform.position.x, _camera.MapSize.x - 0.5f);
            
            return;
        }
        
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
        
        // _isLive = true;
        _isLive = true;
        ObjectType = Define.ObjectType.Player;
        Hands = GetComponentsInChildren<Hand>(true);
        
        // 기본공격
        Item item = Managers.Data.ItemDatasDic[0];
        GameObject newWeapon = new GameObject();
        
        _weaponController = newWeapon.AddComponent<WeaponController>();
        _weaponController.Init(item);
        item.WeaponController = _weaponController;
        
        // 미완
        _animator.runtimeAnimatorController = _animCon[Managers.Game.SelectId];
        return true;
    }

    void OnMove(InputValue value)
    {
        if(!_isLive)
            return;
        // normalized 사용중...
        _inputVec = value.Get<Vector2>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(!_isLive)
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
