using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public float longPress = 1;
    public float longPressFixedUpdateTime = 0.15f;
    float timer = -1;
    float fixedTimer = 0;

    //长按开始事件
    public UnityAction longPressStart;

    //长按每帧事件
    public UnityAction longPressUpdate;

    //长按固定间隔事件
    public UnityAction longPressFixedUpdate;

    //长按结束事件
    public UnityAction longPressEnd;

    public UnityAction onPointerClick;
    public UnityAction onPointerDown;
    public UnityAction onPointerEnter;
    public UnityAction onPointerExit;
    public UnityAction onPointerUp;
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (onPointerClick != null) onPointerClick();
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (onPointerDown != null) onPointerDown();
        timer = 0;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (onPointerEnter != null) onPointerEnter();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (onPointerExit != null) onPointerExit();
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (onPointerUp != null) onPointerUp();
        if (timer >= longPress)
        {
            timer = -1;
            fixedTimer = 0;
            //结束长按
            if (longPressEnd != null)
                longPressEnd();
        }
    }

    private void Update()
    {
        if (timer >= longPress)
        {
            //每帧刷新 
            if (longPressUpdate != null)
                longPressUpdate();
            fixedTimer += Time.deltaTime;
            if (fixedTimer >= longPressFixedUpdateTime)
            {
                fixedTimer = 0;
                //固定刷新
                if (longPressFixedUpdate != null)
                    longPressFixedUpdate();
            }
        }

    }

    private void FixedUpdate()
    {
        if (timer >= 0 && timer < longPress)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= longPress)
            {
                //开始长按
                if (longPressStart != null)
                    longPressStart();
            }
        }

    }
}
