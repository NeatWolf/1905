using System.Collections;
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

        table = Luax.Instance.DoString("require('Lua/Core.lua.txt')").Get<LuaTable>("Core");
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

    public void CopyFileFromStreamingAsset(string fileName)
    {
        string from_path = Application.streamingAssetsPath + "/" + fileName;
        string to_path = Application.persistentDataPath + "/" + fileName;
        WWW www = new WWW(from_path);
        while (!www.isDone) { }
        if (www.error == null)
        {
            File.WriteAllBytes(to_path, www.bytes);
        }
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


        if (Application.platform == RuntimePlatform.Android)
        { }
        else
        {
            //将streamingAssets下所有文件写入Config文件
            WriteInConfig(path);
        }


        CopyDir(path, writePath);
    }

    //复制目录下所有文件 
    void CopyDir(string readPath, string writePath)
    {
        //读取配置文件
        WWW config = new WWW(readPath + "/Config.txt");
        while (!config.isDone) { }
        string data = config.text;
        config.Dispose();
        string[] names = data.Split(';');

        for (int i = 0; i < names.Length; i++)
        {
            //创建目录(仅第一层)
            if (names[i].Contains("/"))
            {
                Directory.CreateDirectory(writePath + "/" + names[i].Split('/')[0]);
            }
            WWW www = new WWW(readPath + "/" + names[i]);
            while (!www.isDone) { }
            if (www.error == null)
            {
                File.WriteAllBytes(writePath + "/" + names[i], www.bytes);
            }
        }
    }

    /// <summary>
    /// 将streamingAssetsPath下所有文件和第一层文件下的子文件的文件名用；分割写入Config.txt文件
    /// </summary>
    /// <param name="readPath"></param>
    void WriteInConfig(string readPath)
    {
        print("开始写配置文件");
        string[] paths = Directory.GetFiles(readPath);//获取每个文件的完整路径
        string[] dirPaths = Directory.GetDirectories(readPath);// 每个文件夹目录
        string config = "";
        for (int i = 0; i < paths.Length; i++)
        {
            string[] filePath = paths[i].Split('/');
            string[] fileNames = filePath[filePath.Length - 1].Split('\\');
            string fileName = fileNames[fileNames.Length - 1];//文件名
            config += fileName + ";";
        }

        for (int i = 0; i < dirPaths.Length; i++)
        {
            print("检测到文件夹");

            string[] _dirPaths = dirPaths[i].Split('/');
            string[] dirNames = _dirPaths[_dirPaths.Length - 1].Split('\\');
            string dirName = dirNames[dirNames.Length - 1];//文件夹名
            string[] subFilePaths = Directory.GetFiles(dirPaths[i]);

            for (int j = 0; j < subFilePaths.Length; j++)
            {
                string[] _subDirPaths = subFilePaths[j].Split('/');
                string[] subDirNames = _subDirPaths[_subDirPaths.Length - 1].Split('\\');
                string subFileName = subDirNames[subDirNames.Length - 1];//文件名
                config += dirName + "/" + subFileName + ";";
            }
        }

        File.WriteAllText(readPath + "/Config.txt", config);
        Debug.Log("配置文件：" + readPath + "/Config.txt");
    }



}
