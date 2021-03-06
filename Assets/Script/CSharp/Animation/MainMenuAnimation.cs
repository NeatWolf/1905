﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class MainMenuAnimation : MonoBehaviour
{
    Button btn_Active, btn_RoleMan, btn_Fabricate,
        btn_Warehouse, btn_Lottery, btn_AV, btn_Explore;
    Camera mainCam, UICam;

    Color color, btn_Explore_Color;
    GameObject avChoice, info;

    private void Awake()
    {

        UISubObject subObjs = transform.GetComponent<UISubObject>();
        btn_Active = subObjs.buttons[2];
        btn_RoleMan = subObjs.buttons[3];

        btn_Fabricate = subObjs.buttons[4];
        btn_Warehouse = subObjs.buttons[5];
        btn_Lottery = subObjs.buttons[0];
        btn_AV = subObjs.buttons[1];
        avChoice = subObjs.go[0];
        info = subObjs.go[1];

        btn_Explore = GameObject.Find("Main Scene/Btn_Explore/P_Explore/Explore").GetComponent<Button>();
        UICam = GameObject.Find("UICamera").GetComponent<Camera>();


        color = btn_RoleMan.GetComponent<Image>().color;
        btn_Explore_Color = btn_Explore.GetComponent<Image>().color;
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        //添加普通按钮动画
       // AnimateManager.AddButtonsAnimate(GetComponent<UISubObject>().buttons);

    }


    private void OnEnable()
    {
        mainCam.gameObject.SetActive(true);

        SetButtonLeave();
        StartCoroutine("ButtonEnter");
        btn_Explore.transform.localScale = Vector3.one;
        btn_Explore.GetComponent<Image>().color = btn_Explore_Color;
        avChoice.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 0.001f);
        avChoice.transform.localScale = Vector3.zero;


        //相机动画
        CameraAnimate();
        mainCam.transform.localPosition = new Vector3(-0.5848389f, -40.74725f, -63.32324f);
    }

    private void Start()
    {




        //添加点击动画
        btn_Active.onClick.AddListener(() =>
        {

            btnActive_Move();

        });
        btn_RoleMan.onClick.AddListener(() =>
        {
            btnActive_Move();

        });
        btn_Fabricate.onClick.AddListener(() =>
        {
            btnActive_Move();

        });
        btn_Warehouse.onClick.AddListener(() =>
        {

            btnActive_Move();

        });
        btn_Lottery.onClick.AddListener(() =>
        {
            btnActive_Move();
        });
        btn_AV.onClick.AddListener(() =>
        {
            avChoice.SetActive(true);
            avChoice.GetComponent<Image>().DOColor(new Color(1, 1, 1, 1), 1f).SetEase(Ease.InOutBack);
            avChoice.transform.DOScale(0.03735288f, 1).SetEase(Ease.InOutBack);
            avChoice.transform.DOMove(btn_AV.transform.position, 1).From();
        });

        //探索按钮
        btn_Explore.onClick.AddListener(() =>
        {
            btn_Explore.transform.DOScale(0.8f, 0.2f);
            btn_Explore.transform.DOScale(1.2f, 0.3f);
            btnActive_Move();


        });


    }
    IEnumerator _ActiveBlack()
    {
        yield return new WaitForSeconds(0.5f);
        ActiveBlack();
    }
    public void ActiveBlack()
    {
        GameObject go = transform.parent.Find("P_Active").gameObject;
        go.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1f), 0.5f);
    }


    /// <summary>
    /// Button离场动画
    /// </summary>
    public void btnActive_Move()
    {

        btn_Active.transform.DOLocalMoveX(500, 0.3f);
        btn_Active.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 0.2f);
        btn_RoleMan.gameObject.transform.DOLocalMoveX(500, 0.5f);
        btn_RoleMan.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 0.4f);
        btn_Fabricate.transform.DOLocalMoveX(500, 0.7f);
        btn_Fabricate.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 0.6f);
        btn_Warehouse.transform.DOLocalMoveX(500, 1);
        btn_Warehouse.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 0.8f);

        btn_Lottery.transform.DOLocalMoveX(-1000, 0.5f);
        btn_Lottery.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 0.8f);

        info.transform.DOLocalMoveX(-1000, 0.5f);


        btn_AV.transform.DOScale(0, 0.5f);
        btn_AV.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 0.5f);



        //DestroyButton();

    }
    /// <summary>
    /// 销毁Button
    /// </summary>
    public void DestroyButton()
    {
        Destroy(btn_Active.gameObject, 2);
        Destroy(btn_RoleMan.gameObject, 2);
        Destroy(btn_Fabricate.gameObject, 2);
        Destroy(btn_Warehouse.gameObject, 2);
        Destroy(btn_Lottery.gameObject, 2);
        Destroy(btn_AV.gameObject, 2);
    }
    /// <summary>
    /// 设置位置到屏幕外
    /// </summary>
    void SetButtonLeave()
    {
        btn_Active.transform.localPosition += new Vector3(1000, 0, 0);
        btn_Active.GetComponent<Image>().color = color;
        btn_RoleMan.transform.localPosition += new Vector3(1000, 0, 0);
        btn_RoleMan.GetComponent<Image>().color = color;
        btn_Fabricate.transform.localPosition += new Vector3(1000, 0, 0);
        btn_Fabricate.GetComponent<Image>().color = color;
        btn_Warehouse.transform.localPosition += new Vector3(1000, 0, 0);
        btn_Warehouse.GetComponent<Image>().color = color;
        btn_Lottery.transform.localPosition += new Vector3(-1000, 0, 0);
        btn_Lottery.GetComponent<Image>().color = color;
        info.transform.localPosition += new Vector3(-1000, 0, 0);
        // info.GetComponent<Image>().color = color;

    }

    /// <summary>
    /// Button入场动画
    /// </summary>
    /// <returns></returns>
    IEnumerator ButtonEnter()
    {
        yield return new WaitForSeconds(1.5f);
        btn_Active.transform.DOLocalMoveX(-447, 0.3f);
        btn_RoleMan.transform.DOLocalMoveX(-447, 0.5f);
        btn_Fabricate.transform.DOLocalMoveX(-447, 0.5f);
        btn_Warehouse.transform.DOLocalMoveX(-447, 0.5f);
        btn_Lottery.transform.DOLocalMoveX(300, 0.5f);
        info.transform.DOLocalMoveX(540, 0.5f);

        btn_AV.transform.DOScale(3, 0.5f);
        btn_AV.GetComponent<Image>().DOColor(new Color(1, 1, 1, 1), 0.5f);

    }

    /// <summary>
    /// 相机动画，场景中的主相机与UI相机
    /// </summary>
    void CameraAnimate()
    {
        mainCam.transform.DOLocalMove(new Vector3(-0.018534f, -29.87421f, -41.47861f), 2).From();
        // UICam.transform.DOLocalMove(new Vector3(-0.018534f,-29.87421f,-41.47861f),2).From();
    }



}
