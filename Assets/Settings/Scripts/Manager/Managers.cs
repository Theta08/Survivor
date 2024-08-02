using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers s_instance;
    public static Managers Instance { get { Init();  return s_instance; } }
    //
    // private GameManager s_gameManager = new GameManager();
    // public static GameManager Game { get { Init(); return Instance.s_gameManager; } }
    //
    // // SceneManagerEx _scene = new SceneManagerEx();
    //
    // private ResourceManager _resource = new ResourceManager();
    // private InputManager _input = new InputManager();
    // private PoolManager _pool = new PoolManager();
    // private UIManager _uiManager = new UIManager();
    //
    // public static ResourceManager Resource { get { Init(); return Instance._resource; } }
    // public static InputManager Input { get { Init(); return Instance._input; } }
    // public static PoolManager Pool { get { Init(); return Instance._pool; } }
    // public static UIManager UI { get { Init(); return Instance._uiManager; } }
    
    void Start()
    {
        Init();
    }

    private static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
                go = new GameObject { name = "@Managers" };

            DontDestroyOnLoad(go);
            // s_instance = Utils.GetOrAddComponent<Managers>(go);
            //
            // // 다른 매니저 Init
            // s_instance._pool.Init();
            // s_instance._resource.Init();
        }
    }
    void Update()
    {
        // _input.OnUpdate();
    }
}
