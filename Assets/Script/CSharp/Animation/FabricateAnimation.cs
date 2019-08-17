using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FabricateAnimation : MonoBehaviour
{
    GameObject rightList,leftList,select,word;
    private void Awake() {
        rightList=GetComponent<UISubObject>().go[0];
        leftList=GetComponent<UISubObject>().go[1];
        select=GetComponent<UISubObject>().go[2];
        word=GetComponent<UISubObject>().go[3];
        
    }
    private void OnEnable() {
        FabricateEnterAnimate();
    }
    /// <summary>
    /// 制造界面入场动画
    /// </summary>
    public void FabricateEnterAnimate(){
        rightList.GetComponent<RectTransform>().DOAnchorPosX(rightList.GetComponent<RectTransform>().anchoredPosition.x-5,1).SetEase(Ease.InOutBack);
        rightList.transform.DORotate(new Vector3(0,385,0),1,RotateMode.FastBeyond360);
        leftList.GetComponent<RectTransform>().DOAnchorPosX(leftList.GetComponent<RectTransform>().anchoredPosition.x+2.5f,1).SetEase(Ease.InOutBack);
        leftList.transform.DORotate(new Vector3(0,-390,0),1,RotateMode.FastBeyond360);
        select.GetComponent<RectTransform>().DOAnchorPosX(rightList.GetComponent<RectTransform>().anchoredPosition.x-9,1).SetEase(Ease.InOutBack);
        select.transform.DORotate(new Vector3(0,385,0),1,RotateMode.FastBeyond360);
        word.transform.DOLocalRotate(new Vector3(0,-30,0),1,RotateMode.FastBeyond360);
        
    }
    /// <summary>
    /// 制造界面出场动画
    /// </summary>
    public void FabricateExitAnimate(){
        rightList.GetComponent<RectTransform>().DOAnchorPosX(rightList.GetComponent<RectTransform>().anchoredPosition.x+5,1).SetEase(Ease.InOutBack);
        rightList.transform.DORotate(new Vector3(0,385,0),1,RotateMode.FastBeyond360);
        leftList.GetComponent<RectTransform>().DOAnchorPosX(leftList.GetComponent<RectTransform>().anchoredPosition.x-2.5f,1).SetEase(Ease.InOutBack);
        leftList.transform.DORotate(new Vector3(0,-390,0),1,RotateMode.FastBeyond360);
        select.GetComponent<RectTransform>().DOAnchorPosX(rightList.GetComponent<RectTransform>().anchoredPosition.x+9,1).SetEase(Ease.InOutBack);
        select.transform.DORotate(new Vector3(0,385,0),1,RotateMode.FastBeyond360);
        word.transform.DOLocalRotate(new Vector3(-90,-30,0),1,RotateMode.FastBeyond360);
    }
}
