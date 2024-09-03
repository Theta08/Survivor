using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour
{
    private string _type;
    private float _rate;
    private Item _item;
    
    public void Init(Item item)
    {
        name = "Gear_" + item.id;
        transform.parent = Managers.Game.GetPlayer.transform;
        transform.localPosition = Vector3.zero;

        _type = item.type;
        _rate = item.damages[0];
        
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        _rate = rate;
        ApplyGear();
    }
    
    //호출 담당
    void ApplyGear()
    {
        switch (_type)
        {
            case "glove":
                RateUp();
                break;
            case "shoe":
                SpeedUp();
                break;
        }
    }
    
    
    // 연사력
    void RateUp()
    {
        // 플레이어 모든 무기
        WeaponController[] weapons = transform.parent.GetComponentsInChildren<WeaponController>();

        foreach (WeaponController weapon in weapons)
        {
            switch (weapon.id)
            {
                case 1:
                    weapon.speed = 150 + (150 * _rate);
                    break;
                case 0:
                    weapon.speed *= (1f - _rate);
                    break;
                default:
                    weapon.speed *= (1f - _rate);
                    break;
            }
        }
    }

    // 이동속도
    void SpeedUp()
    {
        float speed = 1;
        Managers.Game.GetPlayer.Stat.Spd += (speed * _rate);
    }
}
