using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action<Define.KeyActionEvent> KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;
    
    private bool _pressed = false;
    private float _pressedTime = 0;

    public void OnUpdate()
    {
        if (Input.anyKey && KeyAction != null)
        {
            // 이동 상태설정
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (!_pressed)
                {
                    KeyAction.Invoke(Define.KeyActionEvent.PointerDown);
                    _pressedTime = Time.time;
                }
                KeyAction.Invoke(Define.KeyActionEvent.Press);
                _pressed = true;
            }
        }
        else
        {
            if (_pressed)
            {
                if(Time.time < _pressedTime + 0.2f)
                    KeyAction.Invoke(Define.KeyActionEvent.Click);
                    
                KeyAction.Invoke(Define.KeyActionEvent.PointerUp);
            }
            _pressed = false;
            _pressedTime = 0;
        }
    }
    
    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
