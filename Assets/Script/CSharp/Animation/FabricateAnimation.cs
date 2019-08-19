using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FabricateAnimation : MonoBehaviour
{
    GameObject rightList, leftList, select, word, bg, transfer, transferCopy, transfer2, transferCopy2;
    private void Awake()
    {
        rightList = GetComponent<UISubObject>().go[0];
        leftList = GetComponent<UISubObject>().go[1];
        select = GetComponent<UISubObject>().go[2];
        word = GetComponent<UISubObject>().go[3];
        bg = GetComponent<UISubObject>().go[4];
        transfer = GetComponent<UISubObject>().go[5];
        transferCopy = GetComponent<UISubObject>().go[6];
        transfer2 = GetComponent<UISubObject>().go[7];
        transferCopy2 = GetComponent<UISubObject>().go[8];

    }
    private void OnEnable()
    {
        FabricateEnterAnimate();
    }
    /// <summary>
    /// 制造界面入场动画
    /// </summary>
    public void FabricateEnterAnimate()
    {
        rightList.GetComponent<RectTransform>().DOAnchorPosX(rightList.GetComponent<RectTransform>().anchoredPosition.x - 5, 1).SetEase(Ease.InOutBack);
        rightList.transform.DORotate(new Vector3(0, 385, 0), 1, RotateMode.FastBeyond360);
        leftList.GetComponent<RectTransform>().DOAnchorPosX(leftList.GetComponent<RectTransform>().anchoredPosition.x + 2.5f, 1).SetEase(Ease.InOutBack);
        leftList.transform.DORotate(new Vector3(0, -390, 0), 1, RotateMode.FastBeyond360);
        select.GetComponent<RectTransform>().DOAnchorPosX(rightList.GetComponent<RectTransform>().anchoredPosition.x - 9, 1).SetEase(Ease.InOutBack);
        select.transform.DORotate(new Vector3(0, 385, 0), 1, RotateMode.FastBeyond360);
        word.transform.DOLocalRotate(new Vector3(0, -30, 0), 1, RotateMode.FastBeyond360);

        bg.GetComponent<RectTransform>().DOAnchorPosX(100, 1f).From();


    }
    /// <summary>
    /// 制造界面出场动画
    /// </summary>
    public void FabricateExitAnimate()
    {
        rightList.GetComponent<RectTransform>().DOAnchorPosX(rightList.GetComponent<RectTransform>().anchoredPosition.x + 5, 1).SetEase(Ease.InOutBack);
        rightList.transform.DORotate(new Vector3(0, 385, 0), 1, RotateMode.FastBeyond360);
        leftList.GetComponent<RectTransform>().DOAnchorPosX(leftList.GetComponent<RectTransform>().anchoredPosition.x - 2.5f, 1).SetEase(Ease.InOutBack);
        leftList.transform.DORotate(new Vector3(0, -390, 0), 1, RotateMode.FastBeyond360);
        select.GetComponent<RectTransform>().DOAnchorPosX(rightList.GetComponent<RectTransform>().anchoredPosition.x + 9, 1).SetEase(Ease.InOutBack);
        select.transform.DORotate(new Vector3(0, 385, 0), 1, RotateMode.FastBeyond360);
        word.transform.DOLocalRotate(new Vector3(-90, -30, 0), 1, RotateMode.FastBeyond360);
    }
    private void Update()
    {
        transfer.transform.Translate(0, 0.1f, 0);
        transfer2.transform.Translate(0, 0.1f, 0);

        if (transfer.transform.position.x <= transferCopy2.transform.position.x)
        {
            transfer.transform.position = transferCopy.transform.position;
        }
        if (transfer2.transform.position.x <= transferCopy2.transform.position.x)
        {
            transfer2.transform.position = transferCopy.transform.position;
        }

       //transfer.GetComponent<Image>().material.mainTextureOffset += Vector2.up * 1 * Time.deltaTime;
    }

}
