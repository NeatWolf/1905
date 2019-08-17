using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FabricateROLEAnimation : MonoBehaviour
{
    GameObject sroll, choice, bg;
    private void Awake()
    {
        sroll = GetComponent<UISubObject>().go[1];
        choice = GetComponent<UISubObject>().go[2];
        bg = GetComponent<UISubObject>().go[3];
    }
    private void OnEnable()
    {
        ROLEEnterAnimate();
    }
    public void ROLEEnterAnimate()
    {
        bg.GetComponent<Image>().DOFade(0.4f, 1);
        sroll.SetActive(true);
        choice.SetActive(true);
        sroll.GetComponent<RectTransform>().DOAnchorPosX(100, 0.5f).From();

        choice.GetComponent<RectTransform>().DOAnchorPosX(100, 0.5f).From();
    }
    public void ROLEExitAnimate()
    {
        bg.GetComponent<Image>().DOFade(0f, 1);
        sroll.GetComponent<RectTransform>().DOAnchorPosX(100, 0.5f);
        choice.GetComponent<RectTransform>().DOAnchorPosX(100,0.5f).onComplete = () =>
        {
            sroll.SetActive(false);
            choice.SetActive(false);
            sroll.GetComponent<RectTransform>().DOAnchorPosX(-100, 0.1f);
            choice.GetComponent<RectTransform>().DOAnchorPosX(-100, 0.1f);
            
        };
    }
}
