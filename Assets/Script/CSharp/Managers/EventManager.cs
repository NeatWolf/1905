using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager:MonoBehaviour
{
    private static EventManager instance;
    public Dictionary<int, UnityEvent> EventListerDict = new Dictionary<int, UnityEvent>();

    internal static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventManager();
            }
            return instance;
        }
    }

    //注册事件
    public void Register(int key, UnityEvent eventMgr)
    {

        if (EventListerDict.ContainsKey(key))
        {
            Debug.LogError("Key:" + key + "已存在！");
        }
        else
        {
            if (eventMgr == null)
            {
                Debug.Log(key);
            }
            EventListerDict.Add(key, eventMgr);
            Debug.Log("Key" + key + "注册成功！");
        }
    }

    //注销事件
    public void UnRegister(int key)
    {

        if (EventListerDict != null && EventListerDict.ContainsKey(key))
        {
            EventListerDict.Remove(key);
            Debug.Log("移除事件：" + key);
        }
        else
        {
            Debug.LogError("Key:" + key + "不存在！");
        }
    }
    
    //绑定事件
    public void BindDing(int key, UnityAction call)
    {

        if (call == null)
        {
            Debug.LogError("Call函数未空");
        }

        if (EventListerDict.ContainsKey(key))
        {
            EventListerDict[key].AddListener(call);
            Debug.Log("绑定的Key" + key + "成功");
        }
        else
        {
            Debug.LogError("要绑定的Key" + key + "不存在");
        }



    }
    //解绑
    public void UnBinding(int key, UnityAction call)
    {
        if (!EventListerDict.ContainsKey(key))
        {
            Debug.LogError("Key:" + key + "不存在！");
        }
        else
        {
            EventListerDict[key].RemoveListener(call);
        }

    }
    ///解绑所有事件
    public void ClearAll()
    {
        if (EventListerDict != null)
        {
            EventListerDict.Clear();
            Debug.Log("清空注册事件！");
        }


    }


    ///key值是否被注册
    public bool IsRegisterName(int key)
    {
        if (EventListerDict[key] != null && EventListerDict.ContainsKey(key))
        {
            EventListerDict.Remove(key);
            Debug.Log("事件：" + key + "已注册！");
            return true;
        }
        Debug.Log("事件：" + key + "未注册！");
        return false;
    }

    ///调用
    public void Invoke(int key)
    {


        if (EventListerDict.ContainsKey(key))
        {
            EventListerDict[key].Invoke();
        }
        else
        {
            Debug.LogError("事件：" + key + "未注册！");
        }


    }


}
