using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum UIEvent
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
    }
    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }
    public enum KeyActionEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }
    public enum Sound
    {
        Bgm,
        Effect,
        Speech,
        Max,
    }
    public enum Layer
    {
        Monster = 8,
        Ground = 9,
        Player = 10,
        Block = 11,
    }
    public enum Scene
    {
        Unknown,
        Login,
        Game,
    }

    public enum ObjectType
    {
        Unknown,
        Player,
        Monster,
    }
    
    public enum State
    {
        Idle,
        Moving,
        Die,
        Hit,
        Attack,
    }
    
    // public enum 
}
