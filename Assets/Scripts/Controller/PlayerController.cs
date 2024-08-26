using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    [SerializeField]
    private Vector2 _inputVec;
    
    public Vector2 InputVec { get { return _inputVec; } set { _inputVec = value; } }
    
    void Start()
    {
        Init();
    }
    
    private void FixedUpdate()
    {
        Vector2 nextVec = InputVec * _stat.Spd * Time.fixedDeltaTime;
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
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _stat = gameObject.GetOrAddComponent<Stat>();
        
        ObjectType = Define.ObjectType.Player;
        Managers.Game.GetPlayer = this;
        // Managers.Game.Camera.VirtualCameraSetting(gameObject.transform);
        
        Debug.Log($"atk {_stat.Atk}");

        return true;
    }

    void OnMove(InputValue value)
    {
        // normalized 사용중...
        _inputVec = value.Get<Vector2>();
    }
}
