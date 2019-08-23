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
   

    public void ROLEEnterAnimate()
    {
        gameObject.SetActive(true);
        gameObject.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosX(gameObject.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x+10, 0.7f).From().onComplete=()=>{
            bg.GetComponent<Image>().DOFade(0.4f, 0.5f);
        };
        
    }
    public void ROLEExitAnimate()
    {
        bg.GetComponent<Image>().DOFade(0f, 0.5f);
        gameObject.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosX(gameObject.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x+10,1f).onComplete=()=>{
            gameObject.SetActive(false);
            gameObject.transform.GetChild(0).transform.localPosition-=new Vector3(10,0,0);
        };
        
        
    }
}
