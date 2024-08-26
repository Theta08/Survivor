using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    
    [SerializeField]
    private Rigidbody2D target;
    private WaitForFixedUpdate _wait;
    // 데이터메니저나 다른 스크립트로 빼야함
    private RuntimeAnimatorController[] animCon = new RuntimeAnimatorController[4];
    
    // 물리기반이라 FixedUpdate를 씀
    private void FixedUpdate()
    {
        if (!_isLive || _animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;
        
        Vector2 dirVec = target.position - _rigidbody.position;
        Vector2 nextVec = dirVec.normalized * _stat.Spd * Time.fixedDeltaTime;
        
        _rigidbody.MovePosition(_rigidbody.position + nextVec);
        _rigidbody.velocity = Vector2.zero;

    }

    private void LateUpdate()
    {
        if (!_isLive)
            return;
        
        _sprite.flipX = target.position.x < _rigidbody.position.x;
        
        // if(3 < _testDestorytimer)
        //     Managers.Resource.Destroy(gameObject);
    }

    private void OnEnable()
    {
        target = Managers.Game.GetPlayer.GetComponent<Rigidbody2D>();
        _isLive = true;
        _collider2D.enabled = true;
        _rigidbody.simulated = true;
        _sprite.sortingOrder = 2;
        _animator.SetBool("Dead", false);
        // _stat.Hp = _stat.MaxHp;
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        ObjectType = Define.ObjectType.Monster;

        _wait = new WaitForFixedUpdate();
        target = Managers.Game.GetPlayer.GetComponent<Rigidbody2D>();
        _stat = gameObject.GetOrAddComponent<MonsterStat>();
        
        // 빼야함
        animCon[0] = Managers.Resource.Load<RuntimeAnimatorController>("Animations/Enemy/AcEnemy 2");
        // 미완
        _animator.runtimeAnimatorController = animCon[0];
        return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet"))
            return;

        StartCoroutine("KnockBack");
        _stat.Hp -= (int)other.GetComponent<Bullet>().Damage;

        if (_stat.Hp <= 0)
        {
            _isLive = false;
            // 비활성화
            _collider2D.enabled = false;
            _rigidbody.simulated = false;
            _sprite.sortingOrder = 1;
            _animator.SetBool("Dead", true);
        }

    }

    IEnumerator KnockBack()
    {
        // 다음 하나의 물리 프레임 딜레이
        yield return _wait;
        Vector3 playerPos = target.transform.position;
        Vector3 dir = transform.position - playerPos;
        
        _rigidbody.AddForce(dir.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        Managers.Game.Despawn(gameObject);
    }
}
