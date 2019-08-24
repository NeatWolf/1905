using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FabricateROLEAnimation : MonoBehaviour
{
    GameObject sroll, choice, bg,window;
    private void Awake()
    {
        sroll = GetComponent<UISubObject>().go[1];
        choice = GetComponent<UISubObject>().go[2];
        bg = GetComponent<UISubObject>().go[3];
        window=GetComponent<UISubObject>().go[4];
    }
   

    public void ROLEEnterAnimate()
    {
        gameObject.SetActive(true);
        
        bg.GetComponent<Image>().DOFade(0.4f,0.5f);
        window.GetComponent<RectTransform>().DOAnchorPosX(window.GetComponent<RectTransform>().anchoredPosition.x+200, 0.7f).From();
        
    }
    public void ROLEExitAnimate()
    {
        bg.GetComponent<Image>().DOFade(0f, 0.5f);
        window.GetComponent<RectTransform>().DOAnchorPosX(window.GetComponent<RectTransform>().anchoredPosition.x+200,0.7f).onComplete=()=>{
            gameObject.SetActive(false);
            window.transform.localPosition-=new Vector3(200,0,0);
            
            
        };
        
        
    }
}
