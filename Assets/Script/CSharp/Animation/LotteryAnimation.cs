using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class LotteryAnimation : MonoBehaviour
{
    //抽一次与抽十次按钮,LED halo
    GameObject leftWord,rightWord,LED,halo;
    private void Awake() {
        leftWord=GetComponent<UISubObject>().go[0];
        rightWord=GetComponent<UISubObject>().go[1];
        LED=GetComponent<UISubObject>().go[2];
        halo=GetComponent<UISubObject>().go[3];

        AnimateManager.LotteryPreviousAnimate(leftWord,rightWord,LED,halo);
    }
    
    private void OnEnable() {
        AnimateManager.LotteryEnterAnimate(leftWord,rightWord,LED,halo);
    }
    private void Update() {
        //LED透明度变化
        var alpha=Mathf.PingPong(Time.time/3,0.4F);
        LED.GetComponent<Image>().color=new Color(1,1,1,alpha);
        halo.GetComponent<Image>().color=new Color(1,1,1,alpha);
       
    }

}
