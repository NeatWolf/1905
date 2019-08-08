using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class AnimateManager 
{
    /// <summary>
    /// 为多个Cavas添加相机
    /// </summary>
    public static void AddCams(GameObject[] canvas)
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].GetComponent<Canvas>().worldCamera =
                GameObject.Find("UICamera").GetComponent<Camera>();
        }
    }
    /// <summary>
    /// 为一个Canvas添加相机
    /// </summary>
    /// <param name="canva"></param>
    public static void AddCam(GameObject canva) {
        canva.GetComponent<Canvas>().worldCamera =
                GameObject.Find("UICamera").GetComponent<Camera>();
    }
    /// <summary>
    /// 为多个Button添加默认动画
    /// </summary>
    /// <param name="btns"></param>
    public static void AddButtonsAnimate(Button[] btns) {

        for (int i = 0; i < btns.Length; i++)
        {
            if (btns[i].GetComponent<BtnAnimate>() != null) return;
            btns[i].gameObject.AddComponent<BtnAnimate>();
        }
    }
    /// <summary>
    /// 为一个Button添加默认动画
    /// </summary>
    /// <param name="btn"></param>
    public static void AddButtonAnimate(Button btn) {
        if (btn.GetComponent<BtnAnimate>() != null) return;
        btn.gameObject.AddComponent<BtnAnimate>();
    }

    

}
