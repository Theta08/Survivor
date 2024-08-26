using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/**
 * 타일 위치 지정
 */
public class RePosition : MonoBehaviour
{
    private Collider2D _collider;

    private void Start()
    {
        _collider = gameObject.GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
     
        if (!other.CompareTag("Area"))
            return;

        Vector3 playerPos = Managers.Game.GetPlayer.transform.position;
        Vector3 myPos = transform.position;

        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = Managers.Game.GetPlayer.InputVec;
        
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            // case "Ground":
            //     if (diffY < diffX)
            //         // 40 : 타일 크기 20 * 20 
            //         transform.Translate(Vector3.right * dirX * 40);
            //     else if (diffX < diffY)
            //         transform.Translate(Vector3.up * dirY * 40);
            //     break;
            case "Enemy":
                if (_collider.enabled)
                    transform.Translate(playerDir * 20 + 
                                        new Vector3(Random.Range(-3f, 3f),Random.Range(-3f, 3f), 0));
                break;
        }
    }
}
