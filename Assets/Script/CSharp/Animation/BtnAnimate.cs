using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnAnimate : MonoBehaviour,
IPointerDownHandler, IPointerUpHandler,
IPointerEnterHandler, IPointerExitHandler,
IDragHandler,IBeginDragHandler

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
        transform.DOScale(1,0.1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(0.9f,0.1f));
        seq.Append(transform.DOScale(1.2f,0.1f)).SetEase(Ease.Flash);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
         transform.DOScale(1, 0.1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
