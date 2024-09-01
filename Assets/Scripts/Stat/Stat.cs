using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField]
    protected int id;
    [Space(10f)]
    [SerializeField]
    protected float _maxHp;
    [SerializeField]
    protected float _hp;
    [SerializeField]
    protected int _atk;
    [SerializeField]
    protected float _atkSpd;
    [SerializeField]
    protected float _spd;
    
    public int Id { get; set; }
    public float MaxHp { get { return _maxHp;} set { _maxHp = value; } }
    public virtual float Hp { 
        get { return _hp;}
        set
        {
            _hp = value;

            if (_hp > 0)
            {
                // Live
                // gameObject.GetComponent<BaseController>().Animator.SetTrigger("Hit");
            }
      
        } 
    }
    public virtual int Atk { get { return _atk;} set { _atk = value; } }
    
    public float AtkSpd { get { return _atkSpd;} set { _atkSpd = value; } }
    public float Spd { get { return _spd;} set { _spd = value; } }
    
    
}
