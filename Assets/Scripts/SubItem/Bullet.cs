using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _damage;
    [SerializeField]
    private int _per;
    
    private Rigidbody2D _rigidbody;
    public float Damage { get { return _damage; } set { _damage = value; } }
    public int Per { get { return _per; } set { _per = value; } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    // -1 is Infinity Per
    public void Init(float damage, int per, Vector3 dir)
    {
        _damage = damage;
        _per = per;
        if (Per > -1)
        {
            // 속력
            _rigidbody.velocity = dir * 15f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy") || Per == -1)
            return;

        Per--;
        
        //
        if (Per == -1)
        {
            _rigidbody.velocity = Vector2.zero;
            Managers.Game.Despawn(gameObject);
        }

    }
}
