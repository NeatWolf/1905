using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public static class AnimateManager
{
    /// <summary>
    /// 为多个Cavas添加相机
    /// </summary>
    public static void AddCams(GameObject[] canvas)
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            if (canvas[i].GetType() == typeof(Canvas) && canvas[i].GetComponent<Canvas>().worldCamera == null)
            {
                canvas[i].GetComponent<Canvas>().worldCamera =
                    GameObject.Find("UICamera").GetComponent<Camera>();
            }

        }
    }
    /// <summary>
    /// 为一个Canvas添加相机
    /// </summary>
    /// <param name="canva"></param>
    public static void AddCam(GameObject canva)
    {
        if (canva.GetType() == typeof(Canvas) && canva.GetComponent<Canvas>().worldCamera == null)
        {
            canva.GetComponent<Canvas>().worldCamera =
                GameObject.Find("UICamera").GetComponent<Camera>();
        }

    }
    /// <summary>
    /// 为多个Button添加默认动画
    /// </summary>
    /// <param name="btns"></param>
    public static void AddButtonsAnimate(Button[] btns)
    {

        for (int i = 0; i < btns.Length; i++)
        {
            if (btns[i].GetComponent<BtnAnimate>() != null) return;
            btns[i].gameObject.AddComponent<BtnAnimate>();
        }
    }
    /// <summary>
    /// 为一个Button添加默认动画
    /// </summary>
    /// <param name="btn"></param>
    public static void AddButtonAnimate(Button btn)
    {
        if (btn.GetComponent<BtnAnimate>() != null) return;
        btn.gameObject.AddComponent<BtnAnimate>();
    }



    /**
    场景UI入场动画
     */
    public static void AddScenenAnimate()
    {

    }

    /**
    RoleMan出售记录入场动画
    records--出售记录列表
    content--卷轴内的content
     */
    public static void RecordEnterAnimate(GameObject records, GameObject content)
    {
        records.gameObject.SetActive(true);

        records.transform.DOLocalMoveY(-2000, 1, true).SetEase(Ease.InOutBack);
        //records.transform.DOLocalMoveY(-2000,1,true);


        for (int i = 0; i < content.transform.childCount; i++)
        {
            float x = Mathf.Pow(-1, i);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(content.transform.GetChild(i).transform.DORotate(new Vector3(60 * x, 0, 0), 0.3f)).SetEase(Ease.InOutBack);
            sequence.Append(content.transform.GetChild(i).transform.DORotate(new Vector3(-50 * x, 0, 0), 0.3f)).SetEase(Ease.InOutBack);
            sequence.Append(content.transform.GetChild(i).transform.DORotate(new Vector3(40 * x, 0, 0), 0.3f)).SetEase(Ease.InOutBack);
            sequence.Append(content.transform.GetChild(i).transform.DORotate(new Vector3(-30 * x, 0, 0), 0.3f)).SetEase(Ease.InOutBack);
            sequence.Append(content.transform.GetChild(i).transform.DORotate(new Vector3(20 * x, 0, 0), 0.3f)).SetEase(Ease.InOutBack);
            sequence.Append(content.transform.GetChild(i).transform.DORotate(new Vector3(-10 * x, 0, 0), 0.3f)).SetEase(Ease.InOutBack);

            sequence.Append(content.transform.GetChild(i).transform.DORotate(new Vector3(0, 0, 0), 0.1f));

            if (content.transform.GetChild(i).transform.localPosition == new Vector3(0, 1000, 0))
            {
                sequence.Pause();
            }
        }


    }
    /**
    RoleMan出售记录失活状态下位置设置
     */
    public static void RecordPreviousAnimate(GameObject records)
    {
        records.gameObject.SetActive(false);
        records.transform.localPosition += new Vector3(0, 2000, 0);
    }


    /*
    Lottery UI 入场动画

     */
    public static void LotteryEnterAnimate(GameObject left, GameObject right, GameObject led, GameObject halo)
    {
        left.gameObject.SetActive(true);
        right.gameObject.SetActive(true);
        led.gameObject.SetActive(true);
        halo.gameObject.SetActive(true);
        led.GetComponent<Image>().color=new Color(1,1,1,0);
        

        //left动画
        left.transform.DOLocalMoveX(-290, 1).SetEase(Ease.InOutBack);
        Sequence seqLeft=DOTween.Sequence(); 
        seqLeft.Append(left.transform.DORotate(new Vector3(-60, 0, 0), 0.5f));
        seqLeft.Append(left.transform.DORotate(new Vector3(40, 0, 0), 0.3f));
        seqLeft.Append(left.transform.DORotate(new Vector3(-20, 0, 0), 0.3f));
        seqLeft.Append(left.transform.DORotate(new Vector3(0, 0, 0), 0.1f));
       
        //right动画
        right.transform.DOLocalMoveX(300, 1).SetEase(Ease.InOutBack);
        Sequence seqRight=DOTween.Sequence(); 
        seqRight.Append(right.transform.DORotate(new Vector3(-60, 0, 0), 0.5f));
        seqRight.Append(right.transform.DORotate(new Vector3(40, 0, 0), 0.3f));
        seqRight.Append(right.transform.DORotate(new Vector3(-20, 0, 0), 0.3f));
        seqRight.Append(right.transform.DORotate(new Vector3(0, 0, 0), 0.1f));

        //LED动画
        //var alpha=Mathf.PingPong(Time.time/5,1);
        //led.GetComponent<Image>().color=new Color(1,1,1,0.4f);


    }

    /*
    Lottery失活状态下位置设置
     */
    public static void LotteryPreviousAnimate(GameObject left, GameObject right, GameObject led, GameObject halo)
    {
        //  left.gameObject.SetActive(false);
        //  right.gameObject.SetActive(false);
        led.gameObject.SetActive(false);
        halo.gameObject.SetActive(false);
        left.transform.localPosition = new Vector3(-700, left.transform.localPosition.y, left.transform.localPosition.z);
        right.transform.localPosition = new Vector3(700, right.transform.localPosition.y, right.transform.localPosition.z);
        

    }

    /*
    探索界面的编队界面入场动画
     */
    public static void TroopsEnterAnimate(){}

    /* 
    探索界面的编队界面失活状态下位置设置
    */
    public static void TroopsPreviousnimate(GameObject troops,GameObject bg,GameObject title){
        troops.SetActive(false);
        bg.GetComponent<Image>().color=new Color(0.1803922f,0.01568628f,0.2666667f,0);
        // title.transform.localPosition=new Vector3(1000,);

    }


}
