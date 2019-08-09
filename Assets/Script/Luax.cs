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
        string path = Application.persistentDataPath + "/assets/" + name;
        if (!File.Exists(path))
        {       //*用Lua 的 AB包管理器从Assets中读取Scripts包中的对应脚本
            Debug.Log("不存在");
            return null;
        }
        return File.ReadAllBytes(path);
    }

    public void Dispose()
    {
        _luaEnv.Dispose();
        _instance = null;
    }

}