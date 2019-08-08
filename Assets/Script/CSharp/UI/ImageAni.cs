using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ImageAni : EventTrigger, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    private DOTweenAnimation ImageAnim;
    void Awake()
    {
        ImageAnim = GetComponent<DOTweenAnimation>();
    }
    void Start()
    {
        ImageAnim.DOPause();
    }
}
