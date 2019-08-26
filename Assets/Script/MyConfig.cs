using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;

public static class MyConfig
{
    public static readonly string[] LuaPaths = {
        Application.dataPath + "/AssetBundle/Script/",
        Application.dataPath + "/StreamingAssets/Lua/",
        Application.persistentDataPath + "/assets/Lua/"
        };

    public const string AssetBundlesSavePathName = "/ab";

    //打完AB包自动复制到StreamingAssets并生成Config文件
    public static readonly bool CopyToStreamingAssets = true;

    public static readonly string ABCopyPath = Application.streamingAssetsPath + "";
    public static readonly string ABConfigName = Application.streamingAssetsPath + "/Config.txt";

    //运行时要加载的AB包根目录
    public static readonly string ABRootPath = Application.persistentDataPath + "/assets/";
}