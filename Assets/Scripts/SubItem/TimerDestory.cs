using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class TimerDestory : MonoBehaviour
{
    [SerializeField]
    private float _timer;
    [SerializeField]
    private float _destoryTime;
    [SerializeField]
    private bool _startTimer = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_startTimer)
            _timer += Time.deltaTime;
        
        if(_timer > _destoryTime && _destoryTime != 0 )
            Managers.Game.Despawn(gameObject);
    }

    private void OnEnable()
    {
        _startTimer = false;
        _timer = 0;
    }

    public void Init(float time)
    {
        _startTimer = true;
        _destoryTime = time;
    }
}
