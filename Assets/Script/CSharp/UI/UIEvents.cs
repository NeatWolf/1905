using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public UnityAction OnPointerClick;
    public UnityAction OnPointerDown;
    public UnityAction OnPointerEnter;
    public UnityAction OnPointerExit;
    public UnityAction OnPointerUp;
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (OnPointerClick != null) OnPointerClick();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (OnPointerDown != null) OnPointerDown();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (OnPointerEnter != null) OnPointerEnter();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerExit != null) OnPointerExit();
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (OnPointerUp != null) OnPointerUp();
    }
}
