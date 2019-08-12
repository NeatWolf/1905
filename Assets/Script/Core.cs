﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using XLua;
using System.IO;
using DG.Tweening;
using UnityEngine.Networking;
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

        //初始化AB包，复制到可写目录下
        ABInit();

        DontDestroyOnLoad(GameObject.Find("UI"));
        DontDestroyOnLoad(GameObject.Find("Bootstrap"));

        table = Luax.Instance.DoString("require('Core.lua.txt')").Get<LuaTable>("Core");
        callLua.start = table.Get<UnityAction>("Start");
        callLua.update = table.Get<UnityAction>("Update");
        callLua.SecondUpdate = table.Get<UnityAction>("SecondUpdate");


        //Image bar = GameObject.Find("Bar").GetComponent<Image>();

        
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

        // Luax.Instance.Dispose();
    }

    void ABInit()
    {
        string writePath = Application.persistentDataPath + "/assets";
        string path = Application.streamingAssetsPath;
        Debug.Log("ABInt.CS:writePath:" + writePath);
        Debug.Log("ABInt.CS:path:" + path);

        if (!Directory.Exists(writePath))
        {
            Directory.CreateDirectory(writePath);
        }

        CopyDir(path, writePath);
    }

    //复制目录下所有文件 
    void CopyDir(string readPath, string writePath)
    {
        //读取配置文件
        WWW config = new WWW(MyConfig.ABConfigName);
        while (!config.isDone) { }
        string data = config.text;
        config.Dispose();
        string[] names = data.Split(';');

        for (int i = 0; i < names.Length; i++)
        {
            //创建目录
            string filePath = writePath + "/" + names[i];
            string dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            WWW www = new WWW(readPath + "/" + names[i]);
            while (!www.isDone) { }
            if (www.error == null)
            {
                File.WriteAllBytes(filePath, www.bytes);
            }
        }
    }
}
