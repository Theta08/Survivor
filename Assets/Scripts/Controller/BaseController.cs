using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    
    protected bool _isLive = false;
    protected bool _init = false;
    
    protected Stat _stat;
    protected Rigidbody2D _rigidbody;
    protected Collider2D _collider2D;
    protected SpriteRenderer _sprite;
    protected Animator _animator;
    // 데이터메니저나 다른 스크립트로 빼야함
    protected RuntimeAnimatorController[] _animCon = new RuntimeAnimatorController[4];
    
    public Define.ObjectType ObjectType { get; protected set; } = Define.ObjectType.Unknown;
    public Animator Animator { get { return _animator;} set { _animator = value; } }
    // public bool IsLive { get { return isLive;} set { isLive = value; } }
    public Stat Stat { get { return _stat; } set { _stat = value; } }
    private void Awake()
    {
        Init();
    }
    
    public virtual bool Init()
    {
        if (_init)
            return false;
        
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();
        
        return _init = true;
        
    }
}
