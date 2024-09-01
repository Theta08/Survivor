using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft = false;
    public SpriteRenderer _sprite;

    // public SpriteRenderer Sprtie { get { return _sprite; } set { _sprite = value; } }

    private SpriteRenderer _player;

    private Vector3 rightPos = new Vector3(0.2f, -0.25f, 0);
    private Vector3 rightPosReverse = new Vector3(0.0f, -0.25f, 0);
    
    Quaternion leftRot = Quaternion.Euler(0, 0, -35);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);
    private void Awake()
    {
        if (name.Contains("Left"))
            isLeft = true;
        
        _sprite = GetComponent<SpriteRenderer>();
        _player = GetComponentsInParent<SpriteRenderer>()[1];
    }
    
    private void LateUpdate()
    {
        bool isReverse = _player.flipX;
        // 근접
        if (isLeft)
        {
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            _sprite.flipY = isReverse;
            _sprite.sortingOrder = isReverse ? 2 : 4;
        }
        else
        {
            // 원거리
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            _sprite.flipX = isReverse;
            _sprite.sortingOrder = isReverse ? 4 : 2;
        }
    }
}
