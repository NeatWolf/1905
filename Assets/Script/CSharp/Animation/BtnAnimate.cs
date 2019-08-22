using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnAnimate : MonoBehaviour,
IPointerDownHandler, IPointerUpHandler,
IPointerEnterHandler, IPointerExitHandler,
IDragHandler, IBeginDragHandler

{
    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log("按钮按下");
        transform.DOScale(1, 0.1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(0.9f, 0.1f));
        seq.Append(transform.DOScale(1.2f, 0.1f)).SetEase(Ease.Flash);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, 0.1f);

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    private void LateUpdate()
    {
        //防止鼠标位置检测精度不够而造成的物体缩放失准
        Vector2 offset = transform.GetComponent<RectTransform>().offsetMax - transform.GetComponent<RectTransform>().anchoredPosition;

        if (Mathf.Abs(Input.mousePosition.x - GameObject.Find("UICamera").GetComponent<Camera>().WorldToScreenPoint(transform.position).x) > offset.x
        || Mathf.Abs(Input.mousePosition.y - GameObject.Find("UICamera").GetComponent<Camera>().WorldToScreenPoint(transform.position).y) > offset.y)
        {
            transform.DOScale(1, 0.1f);
        }
    }
}
