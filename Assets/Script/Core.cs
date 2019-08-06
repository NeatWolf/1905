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

    public UnityAction start;
    public UnityAction update;
    public UnityAction SecondUpdate;
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
        DontDestroyOnLoad(GameObject.Find("Bootstrap"));

        table = Luax.Instance.DoString("require('Lua/Core.lua.txt')").Get<LuaTable>("Core");
        callLua.start = table.Get<UnityAction>("Start");
        callLua.update = table.Get<UnityAction>("Update");
        callLua.SecondUpdate = table.Get<UnityAction>("SecondUpdate");


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

        Luax.Instance.Dispose();
    }

}
