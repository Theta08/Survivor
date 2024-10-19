using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    
    [SerializeField]
    private Rigidbody2D _target;
    private WaitForFixedUpdate _wait;
    
    // 물리기반이라 FixedUpdate를 씀
    private void FixedUpdate()
    {
        if (!_isLive || _animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;
        
        Vector2 dirVec = _target.position - _rigidbody.position;
        Vector2 nextVec = dirVec.normalized * Stat.Spd * Time.fixedDeltaTime;
        
        _rigidbody.MovePosition(_rigidbody.position + nextVec);
        _rigidbody.velocity = Vector2.zero;

    }

    private void LateUpdate()
    {
        if (!_isLive)
            return;
        
        _sprite.flipX = _target.position.x < _rigidbody.position.x;
        
        // if(3 < _testDestorytimer)
        //     Managers.Resource.Destroy(gameObject);
    }

    private void OnEnable()
    {
        _target = Managers.Game.GetPlayer.GetComponent<Rigidbody2D>();
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
        _target = Managers.Game.GetPlayer.GetComponent<Rigidbody2D>();
        Stat = gameObject.GetOrAddComponent<MonsterStat>();
        
        // 몬스터 애니메이션 타입 설정 및 분당 몹 변경
        EnemyType();
        
        return true;
    }

    // 피격
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet") || !_isLive)
            return;

        StartCoroutine("KnockBack");
        
        Managers.Sound.Play(Define.Sound.Effect, "Hit0");
        Stat.Hp -= (int)other.GetComponent<Bullet>().Damage;

        if (Stat.Hp <= 0)
        {
            _isLive = false;
            Managers.Sound.Play(Define.Sound.Effect, "Dead");
            // 비활성화
            _collider2D.enabled = false;
            _rigidbody.simulated = false;
            _sprite.sortingOrder = 1;
            _animator.SetBool("Dead", true);
            
            Managers.Game.GetExp();
        }

    }

    IEnumerator KnockBack()
    {
        // 다음 하나의 물리 프레임 딜레이
        yield return _wait;
        Vector3 playerPos = _target.transform.position;
        Vector3 dir = transform.position - playerPos;
        
        _rigidbody.AddForce(dir.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        Managers.Game.Despawn(gameObject);
    }

    void EnemyType()
    {
        //
        // enemy 소환시 타입 바인딩 
        // 1. monsterData json연결 해야하나??
        // 2. 4개 만들기
        // 3. 분 당 바꾸기
        
        int selectId = 0;
        int min = Mathf.FloorToInt(Managers.Game.GameTime / 10f) + 1;
        List<MonsterData> test = Managers.Data.MonsterDataList;
 
        
        for(int i = 0; i < test.Count; i++)
            _animCon[i] = Managers.Resource.Load<RuntimeAnimatorController>($"Animations/Enemy/AcEnemy {test[i].id}");

        if (min != 0)
            selectId = min % test.Count;
        
        _animator.runtimeAnimatorController = _animCon[selectId];
    }
}
