using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XLua;
using System.IO;
using DG.Tweening;

[GCOptimize]
public struct CallLua
{
    [CSharpCallLua]
    public delegate void GameObjectEvent(GameObject go);

    [CSharpCallLua]
    public delegate void TouchEvent(System.Object obj);

    public UnityAction start;
    public UnityAction update;
    public UnityAction SecondUpdate;
    public GameObjectEvent mouseEnter;
    public GameObjectEvent upgrade;
    public TouchEvent touchEvent;
}

public class Core : MonoBehaviour
{
    public static Core Instance;
    LuaTable table;
    CallLua callLua;
    float timer = 0;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(GameObject.Find("UI"));
        DontDestroyOnLoad(GameObject.Find("Core"));
        table = Luax.Instance.DoString("require('Core.lua.txt')").Get<LuaTable>("Core");
        callLua.start = table.Get<UnityAction>("Start");
        callLua.update = table.Get<UnityAction>("Update");
        callLua.SecondUpdate = table.Get<UnityAction>("SecondUpdate");
        callLua.mouseEnter = table.Get<CallLua.GameObjectEvent>("MouseEnter");
        callLua.upgrade = table.Get<CallLua.GameObjectEvent>("Upgrade");
        callLua.touchEvent = table.Get<CallLua.TouchEvent>("GalTouch");


    }

    void Start()
    {
        callLua.start();
    }

    void Update()
    {
        if (Time.time - timer > 1)
        {
            callLua.SecondUpdate();
            timer = Time.time;
        }
        callLua.update();

    }

    void OnDestroy()
    {
        callLua.start = null;
        callLua.update = null;
        callLua.SecondUpdate = null;
        callLua.mouseEnter = null;
        callLua.touchEvent = null;
        callLua.upgrade = null;
        Luax.Instance.Dispose();
    }

    public void MouseEnter(GameObject gameObject)
    {
        callLua.mouseEnter(gameObject);
    }

    public void TouchEvent(int touchType)
    {
        callLua.touchEvent(touchType);
    }

    public void Upgrade(GameObject go)
    {
        callLua.upgrade(go);
    }
}
