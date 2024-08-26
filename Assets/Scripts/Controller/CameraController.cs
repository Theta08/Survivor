using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    // private CinemachineVirtualCamera cinemachine;

    private Vector3 _cameraPosition = new Vector3(0, 0, -10);
    [SerializeField]
    private float _cameraMoveSpeed = 0.7f;

    [SerializeField]
    Vector2 center = Vector2.zero;
    [SerializeField]
    Vector2 mapSize = new Vector2(20, 20);
    
    private float _height;
    private float _width;
    
    void Start()
    {
        _height = Camera.main.orthographicSize;
        _width = _height * Screen.width / Screen.height;
        // cinemachine = GetComponent<CinemachineVirtualCamera>();
        // Managers.Game.Camera = this;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, 
            Managers.Game.GetPlayer.transform.position + _cameraPosition, Time.deltaTime * _cameraMoveSpeed);

        float lx = mapSize.x - _width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);
        
        float ly = mapSize.y - _height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }
    // public void VirtualCameraSetting(Transform transform)
    // {
        // cinemachine.Follow = transform;
    // }
}
