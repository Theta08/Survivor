using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Stat _stat;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private Animator _animator;
    [SerializeField]
    private Vector2 _inputVec;
    
    public Vector2 InputVec { get { return _inputVec; } set { _inputVec = value; } }
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void Init()
    {
        // Init 적용 후 Stat.Init 적용
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        
        _stat = gameObject.GetOrAddComponent<Stat>();

        Managers.Game.Camera.VirtualCameraSetting(gameObject.transform);
        Managers.Game.GetPlayer = this;
        
        Debug.Log($"atk {_stat.Atk}");
        Debug.Log($"hp {_stat.Hp}");
    }

    void OnMove(InputValue value)
    {
        // normalized 사용중...
        _inputVec = value.Get<Vector2>();
    }
}
