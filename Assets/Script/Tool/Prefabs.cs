using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Prefabs
{
    public static GameObject Load(string path)
    {
        //加载登陆预制体
        GameObject prefab = Resources.Load<GameObject>(path);
        //实例化
        GameObject page = Object.Instantiate<GameObject>(prefab);
        //避免游戏对象产生Clone
        page.name = prefab.name;
        //设置Canvas为父物体
        page.transform.SetParent(GameObject.Find("/UI/Canvas").transform);
        //position初始化
        page.transform.localPosition = Vector3.zero;
        page.transform.localRotation = Quaternion.identity;
        page.transform.localScale = Vector3.one;
        //设置为最后一个物体
        page.transform.SetAsLastSibling();

        //四锚点的四个外边距归零
        RectTransform rect = page.transform as RectTransform;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        return page;
    }

    public static GameObject LoadCell(string path, Transform parent)
    {
        //加载登陆预制体
        GameObject prefab = Resources.Load<GameObject>(path);
        //实例化
        GameObject page = Object.Instantiate<GameObject>(prefab);
        //避免游戏对象产生Clone
        page.name = prefab.name;
        //设置Canvas为父物体
        page.transform.SetParent(parent);
        //position初始化
        page.transform.localPosition = Vector3.zero;
        page.transform.localRotation = Quaternion.identity;
        page.transform.localScale = Vector3.one;
        //设置为最后一个物体
        page.transform.SetAsLastSibling();
        

        return page;
    }

    //加载并改变旋转
    //public static GameObject LoadChange(string path)
    //{
    //    //加载登陆预制体
    //    GameObject prefab = Resources.Load<GameObject>(path);
    //    //实例化
    //    GameObject page = Object.Instantiate<GameObject>(prefab);
    //    //避免游戏对象产生Clone
    //    page.name = prefab.name;
    //    //设置Canvas为父物体
    //    page.transform.SetParent(GameObject.Find("/UI/Canvas").transform);
    //    //position初始化
    //    page.transform.localPosition = Vector3.zero;
    //    page.transform.localRotation = Quaternion.identity;
    //    page.transform.localScale = Vector3.one;
    //    //设置为最后一个物体
    //    page.transform.SetAsLastSibling();

    //    //四锚点的四个外边距归零
    //    RectTransform rect = page.transform as RectTransform;
    //    rect.offsetMin = Vector2.zero;
    //    rect.offsetMax = Vector2.zero;

    //    return page;
    //}

}
