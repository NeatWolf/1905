using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;

public class Luax
{
    static Luax _instance;
    LuaEnv _luaEnv;
    public static Luax Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Luax();
            }
            return _instance;
        }
    }

    public LuaTable Global
    {
        get
        {
            return _luaEnv.Global;
        }
    }

    Luax()
    {
        _luaEnv = new LuaEnv();
        _luaEnv.AddLoader(myLoder);
    }

    public LuaTable DoString(string code)
    {
        _luaEnv.DoString(code);
        return _luaEnv.Global;
    }

    //自定义加载器
    byte[] myLoder(ref string name)
    {
        for (int i = 0; i < MyConfig.LuaPaths.Length; i++)
        {
            if (File.Exists(MyConfig.LuaPaths[i] + name))
            {
                //Debug.Log("加载脚本"+MyConfig.LuaPaths[i] + name);
                return File.ReadAllBytes(MyConfig.LuaPaths[i] + name);
            }
        }

        Debug.LogWarning("MyConfig.LuaPath所有路径中找不到Lua脚本：" + name);
        return null;
    }

    public void Dispose()
    {
        _luaEnv.Dispose();
        _instance = null;
    }

}