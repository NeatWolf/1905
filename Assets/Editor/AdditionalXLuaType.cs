using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public static class AdditionalXLyaType 
{
    [CSharpCallLua]
    public static List<Type> CSharpCallLuaList = new List<Type>()
    {
        typeof(UnityEngine.Events.UnityAction<bool>),

    };
}
