using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class LotteryAnimation : MonoBehaviour
{
    public float normalSpeed = 0.5f;
    public float moveSpeed = 10f;
    public UnityAction lotteryOutEvent;
    Button leftButton, rightButton;

    //抽一次与抽十次按钮,LED halo
    GameObject leftWord, rightWord, LED, halo;
    private void Awake()
    {
        leftWord = GetComponent<UISubObject>().go[0];
        rightWord = GetComponent<UISubObject>().go[1];
        LED = GetComponent<UISubObject>().go[2];
        halo = GetComponent<UISubObject>().go[3];

        AnimateManager.LotteryPreviousAnimate(leftWord, rightWord, LED, halo);

        leftButton = GetComponent<UISubObject>().buttons[1];
        leftButton.onClick.AddListener(ClickLottery);
        rightButton = GetComponent<UISubObject>().buttons[2];
        rightButton.onClick.AddListener(ClickLottery);
    }

    private void OnEnable()
    {
        AnimateManager.LotteryEnterAnimate(leftWord, rightWord, LED, halo);
    }
    private void Update()
    {
        //LED透明度变化
        var alpha = Mathf.PingPong(Time.time / 3, 0.4F);
        LED.GetComponent<Image>().color = new Color(1, 1, 1, alpha);
        halo.GetComponent<Image>().color = new Color(1, 1, 1, alpha);

    }

    //点击抽奖
    void ClickLottery()
    {
        leftButton.interactable = false;
        rightButton.interactable = false;
        //粒子加速
        for (int i = 0; i < GetComponent<UISubObject>().fx.Length; i++)
        {
            ParticleSystem.MainModule main = GetComponent<UISubObject>().fx[i].main;
            main.simulationSpeed = moveSpeed;
        }
        //延迟3秒掉出道具
        Invoke("Fall", 3);
    }

    void Fall()
    {
        //粒子速度还原
        for (int i = 0; i < GetComponent<UISubObject>().fx.Length; i++)
        {
            ParticleSystem.MainModule main = GetComponent<UISubObject>().fx[i].main;
            main.simulationSpeed = normalSpeed;
        }
        //扭蛋掉出
        if (lotteryOutEvent != null)
            GetComponent<UISubObject>().images[0].transform.DOLocalMoveX(-150, 0.3f).onComplete = () =>
            {
                leftButton.interactable = true;
                rightButton.interactable = true;
                lotteryOutEvent();
            };
        else
            print("扭出蛋事件为空");
    }
}
