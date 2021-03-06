﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WarehouseAnimation : MonoBehaviour
{
    GameObject[] canvas;
    private void Awake()
    {
        canvas = GetComponent<UISubObject>().go;
        canvas[0].GetComponent<Canvas>().worldCamera =
                GameObject.Find("UICamera").GetComponent<Camera>();
        canvas[1].GetComponent<Canvas>().worldCamera =
                GameObject.Find("UICamera").GetComponent<Camera>();

        for (int i = 0; i < canvas[2].transform.childCount; i++)
        {
            if (canvas[2].transform.GetChild(i).GetComponent<BtnAnimate>() != null)
            {
                return;
            }
            canvas[2].transform.GetChild(i).gameObject.AddComponent<BtnAnimate>();
        }

    }
    
    /// <summary>
    /// 仓库入场动画
    /// </summary>
    public void WarehouseEnterAnimate(){
        canvas[0].transform.GetChild(0).GetComponent<Image>().color = new Vector4(1,1,1,0);
        canvas[0].transform.GetChild(0).gameObject.SetActive(true);
        canvas[0].transform.GetChild(0).GetComponent<Image>().DOFade(1f,0.5f);
        canvas[1].GetComponent<RectTransform>().DOAnchorPosX( canvas[1].GetComponent<RectTransform>().anchoredPosition.x-7f,1).SetEase(Ease.InOutBack);
        canvas[1].transform.DORotate(new Vector3(0,379,0),1,RotateMode.FastBeyond360);
    }
    /// <summary>
    /// 仓库出场动画
    /// </summary>
    public void WarehouseExitAnimate(){
        canvas[1].GetComponent<RectTransform>().DOAnchorPosX( canvas[1].GetComponent<RectTransform>().anchoredPosition.x+7f,1).SetEase(Ease.InOutBack);
        canvas[1].transform.DORotate(new Vector3(0,379,0),1,RotateMode.FastBeyond360);
        
        canvas[0].transform.GetChild(0).GetComponent<Image>().DOFade(0f,0.5f).onComplete = () => canvas[0].transform.GetChild(0).gameObject.SetActive(false);
        
    }


    /// <summary>
    /// 数量选择入场动画
    /// </summary>
    public void CountChoiceEnterAnimate(){
        canvas[3].SetActive(true);
    }
    /// <summary>
    /// 数量选择出场动画
    /// </summary>
    public void CountChoiceExitAnimate(){
        canvas[3].SetActive(false);
    }
}
