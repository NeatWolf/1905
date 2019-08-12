using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;

[LuaCallCSharp]
public static class ChangeType
{

    public static Texture2D ToTexture2D(Object obj)
    {
        Debug.Log(obj.GetType());
        if(obj is Texture2D)
        {
            return obj as Texture2D;
        }
        else
        {
            Debug.LogError("obj can't turn sprite");
            return null;
        }

    }

}
