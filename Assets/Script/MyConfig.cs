using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;

public class MyConfig
{
    public static readonly string[] LuaPaths = {
        Application.dataPath + "/AssetBundle/Script/",
        Application.dataPath + "/StreamingAssets/Lua/",
        Application.persistentDataPath + "/assets/"
        };
}