using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WarehouseAnimation : MonoBehaviour
{
    GameObject[] canvas;
    Button[] btns;
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
    private void OnEnable()
    {
        WarehouseEnterAnimate();
    }
    
    /// <summary>
    /// 仓库入场动画
    /// </summary>
    public void WarehouseEnterAnimate(){
        canvas[1].GetComponent<RectTransform>().DOAnchorPosX( canvas[1].GetComponent<RectTransform>().anchoredPosition.x-7f,1).SetEase(Ease.InOutBack);
        canvas[1].transform.DORotate(new Vector3(0,379,0),1,RotateMode.FastBeyond360);
    }
    /// <summary>
    /// 仓库出场动画
    /// </summary>
    public void WarehouseExitAnimate(){
        canvas[1].GetComponent<RectTransform>().DOAnchorPosX( canvas[1].GetComponent<RectTransform>().anchoredPosition.x+7f,1).SetEase(Ease.InOutBack);
        canvas[1].transform.DORotate(new Vector3(0,379,0),1,RotateMode.FastBeyond360);
    }
}
