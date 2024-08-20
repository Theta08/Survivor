using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamera : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachine;
    void Start()
    {
        cinemachine = GetComponent<CinemachineVirtualCamera>();
        Managers.Game.Camera = this;
    }
    
    public void VirtualCameraSetting(Transform transform)
    {
        cinemachine.Follow = transform;
    }
}
