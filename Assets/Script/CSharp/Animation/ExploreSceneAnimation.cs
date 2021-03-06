﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ExploreSceneAnimation : MonoBehaviour
{
    public AnimationCurve curve;
    Button[] btns;
    GameObject mainCam;
    GameObject[] fatherObj;
    GameObject canvas, troops;
    UISubObject uISub;

    private void Start()
    {
        canvas = GameObject.Find("UI/ExploreUI/Canvas");
        troops = canvas.GetComponent<UISubObject>().go[0];
        uISub = canvas.GetComponent<UISubObject>();
        fatherObj = new GameObject[4];

        btns = GetComponent<UISubObject>().buttons;
        mainCam = GetComponent<UISubObject>().go[0];
        for (int i = 0; i < 4; i++)
        {
            fatherObj[i] = GetComponent<UISubObject>().go[i + 1];
        }

    }


    private void Update()
    {

        //按钮点击场景中的图标
        if (Input.GetMouseButtonDown(0) && !troops.activeSelf)
        {

            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition + Vector3.forward * 50);
            RaycastHit hitInfo;
            if (Physics.Raycast(r, out hitInfo))
            {
                if (hitInfo.transform.tag == "button")
                {
                    
                    hitInfo.transform.GetChild(0).DOScale(20f, 1f);
                    hitInfo.transform.GetChild(0).DOMove(GetComponent<UISubObject>().go[5].transform.position, 1.5f).onComplete = () =>
                    {
                        DOTween.KillAll();
                        hitInfo.transform.GetChild(0).DORotate(new Vector3(0, 0, 0), 0.5f);
                    };


                    uISub.go[11].GetComponent<RectTransform>().DOAnchorPosX(uISub.go[2].GetComponent<RectTransform>().anchoredPosition.x - 1500, 1).SetEase(Ease.InOutBack);
                    uISub.go[10].GetComponent<RectTransform>().DOAnchorPosX(uISub.go[10].GetComponent<RectTransform>().anchoredPosition.x + 500, 1).SetEase(Ease.InOutBack);
                    uISub.go[9].GetComponent<RectTransform>().DOAnchorPosY(uISub.go[9].GetComponent<RectTransform>().anchoredPosition.x - 500, 1).SetEase(Ease.InOutBack);
                    uISub.buttons[1].GetComponent<RectTransform>().DOAnchorPosY(uISub.go[9].GetComponent<RectTransform>().anchoredPosition.x + 500, 1).SetEase(Ease.InOutBack);
                    uISub.go[18].GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 1).onComplete = () =>
                    {
                        GameObject.Find("UI/ExploreUI").SetActive(false);
                        transform.parent.parent.gameObject.SetActive(false);

                    };

                    //调用按钮点击事件
                    hitInfo.transform.GetChild(0).GetComponent<Button>().onClick.Invoke();



                }
            }
        }


    }
}
