using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIPanelBase : MonoBehaviour
{
    public void Show()
    {
        OnInit();//初始化
        OnShow();//显示UI
        gameObject.SetActive(true);//游戏物体激活
    }
    public void Hide()
    {
        OnHide();//隐藏UI
        gameObject.SetActive(false);//游戏物体失活
    }
    public void Dispose()
    {
        OnDispose();
    }



    protected abstract void OnInit();
    protected abstract void OnShow();
    protected abstract void OnHide();
    protected abstract void OnDispose();



}