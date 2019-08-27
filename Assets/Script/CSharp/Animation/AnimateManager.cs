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


    /// <summary>
    /// RoleMan出售记录入场动画
    /// </summary>
    /// <param name="records">出售记录列表</param>
    /// <param name="content">卷轴内的content</param>
    public static void RecordEnterAnimate(GameObject records, GameObject content)
    {
        records.transform.parent.gameObject.SetActive(true);

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

        records.transform.GetChild(0).GetComponent<ScrollRect>().verticalScrollbar.value = 0;
    }

    /// <summary>
    /// RoleMan出售记录失活状态下位置设置
    /// </summary>
    /// <param name="records"></param>
    public static void RecordPreviousAnimate(GameObject records)
    {
        records.transform.parent.gameObject.SetActive(false);
        records.transform.localPosition += new Vector3(0, 2000, 0);
    }



    /// <summary>
    /// Lottery UI 入场动画
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="led"></param>
    /// <param name="halo"></param>
    public static void LotteryEnterAnimate(GameObject left, GameObject right, GameObject led, GameObject halo)
    {
        left.gameObject.SetActive(true);
        right.gameObject.SetActive(true);
        led.gameObject.SetActive(true);
        halo.gameObject.SetActive(true);
        led.GetComponent<Image>().color = new Color(1, 1, 1, 0);


        //left动画
        left.transform.DOLocalMoveX(-290, 1).SetEase(Ease.InOutBack);
        Sequence seqLeft = DOTween.Sequence();
        seqLeft.Append(left.transform.DORotate(new Vector3(-60, 0, 0), 0.5f));
        seqLeft.Append(left.transform.DORotate(new Vector3(40, 0, 0), 0.3f));
        seqLeft.Append(left.transform.DORotate(new Vector3(-20, 0, 0), 0.3f));
        seqLeft.Append(left.transform.DORotate(new Vector3(0, 0, 0), 0.1f));

        //right动画
        right.transform.DOLocalMoveX(300, 1).SetEase(Ease.InOutBack);
        Sequence seqRight = DOTween.Sequence();
        seqRight.Append(right.transform.DORotate(new Vector3(-60, 0, 0), 0.5f));
        seqRight.Append(right.transform.DORotate(new Vector3(40, 0, 0), 0.3f));
        seqRight.Append(right.transform.DORotate(new Vector3(-20, 0, 0), 0.3f));
        seqRight.Append(right.transform.DORotate(new Vector3(0, 0, 0), 0.1f));

        //LED动画
        //var alpha=Mathf.PingPong(Time.time/5,1);
        //led.GetComponent<Image>().color=new Color(1,1,1,0.4f);


    }


    /// <summary>
    /// Lottery失活状态下位置设置
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="led"></param>
    /// <param name="halo"></param>
    public static void LotteryPreviousAnimate(GameObject left, GameObject right, GameObject led, GameObject halo)
    {
        //  left.gameObject.SetActive(false);
        //  right.gameObject.SetActive(false);
        led.gameObject.SetActive(false);
        halo.gameObject.SetActive(false);
        left.transform.localPosition = new Vector3(-700, left.transform.localPosition.y, left.transform.localPosition.z);
        right.transform.localPosition = new Vector3(700, right.transform.localPosition.y, right.transform.localPosition.z);


    }




    /// <summary>
    /// 探索界面的编队界面入场动画
    /// </summary>
    /// <param name="troops"></param>
    /// <param name="bg"></param>
    /// <param name="title"></param>
    /// <param name="heng"></param>
    /// <param name="heng2"></param>
    /// <param name="heng3"></param>
    /// <param name="toggleSwitch"></param>
    /// <param name="scroll"></param>
    /// <param name="heng4"></param>
    /// <param name="troopsGroup"></param>
    /// <param name="slider"></param>
    /// <param name="mainTitle"></param>
    /// <param name="info"></param>
    /// <param name="icon"></param>
    public static void TroopsEnterAnimate(GameObject troops, GameObject bg, GameObject title, GameObject heng, GameObject heng2,
     GameObject heng3, GameObject toggleSwitch, GameObject scroll, GameObject heng4,
     GameObject troopsGroup, GameObject slider, GameObject mainTitle, GameObject info, GameObject icon)
    {

        mainTitle.transform.DOBlendableLocalMoveBy(new Vector3(-300, 0, 0), 1).SetEase(Ease.InOutBack);
        slider.transform.DOBlendableLocalMoveBy(new Vector3(300, 0, 0), 1).SetEase(Ease.InOutBack);
        troopsGroup.transform.DOLocalRotate(new Vector3(0, 0, -90), 0.5f).SetEase(Ease.InOutBack);
        troops.SetActive(true);
        // troops.transform.DOScale(Vector3.one,0.5f);
        //troops.transform.DOLocalMove(Vector3.one,0.5f);
        //bg.GetComponent<Image>().color=new Color(0.1803922f,0.01568628f,0.2666667f,0.4f);
        bg.GetComponent<Image>().DOFade(0.4f, 1);
        title.transform.DOLocalMoveX(170f, 1).SetEase(Ease.InOutBack);
        heng.transform.DOLocalMoveX(0, 2).SetEase(Ease.InOutBack);
        heng2.transform.DOLocalMoveX(0, 1.5f).SetEase(Ease.InOutBack);
        heng3.transform.DOLocalMoveX(0, 1.5f).SetEase(Ease.InOutBack);
        toggleSwitch.transform.DOLocalMoveX(0, 1.5f).SetEase(Ease.InOutBack);
        scroll.transform.DOLocalMoveX(0, 2).SetEase(Ease.InOutBack);
        heng4.transform.DOLocalMoveX(0, 2).SetEase(Ease.InOutBack);
        info.transform.DOLocalMoveX(0, 1.2f).SetEase(Ease.InOutBack);
        icon.transform.DOLocalMoveX(0, 1.5f).SetEase(Ease.InOutBack);

    }


    /// <summary>
    /// 探索界面的编队界面失活状态下位置设置
    /// </summary>
    /// <param name="troops"></param>
    /// <param name="bg"></param>
    /// <param name="title"></param>
    /// <param name="heng"></param>
    /// <param name="heng2"></param>
    /// <param name="heng3"></param>
    /// <param name="btnTroops"></param>
    /// <param name="toggleSwitch"></param>
    /// <param name="scroll"></param>
    /// <param name="heng4"></param>
    /// <param name="info"></param>
    /// <param name="icon"></param>
    public static void TroopsPreviousnimate(GameObject troops, GameObject bg, GameObject title, GameObject heng, GameObject heng2, GameObject heng3, Button btnTroops, GameObject toggleSwitch, GameObject scroll, GameObject heng4, GameObject info, GameObject icon)
    {

        troops.SetActive(false);


        bg.GetComponent<Image>().color = new Color(0.1803922f, 0.01568628f, 0.2666667f, 0);
        bg.transform.localScale = new Vector3(1000, 1000, 1000);

        title.transform.localPosition = new Vector3(1000, 699.1f, 0);
        heng.transform.localPosition = new Vector3(1500, heng.transform.localPosition.y, heng.transform.localPosition.z);
        heng2.transform.localPosition = new Vector3(1500, heng2.transform.localPosition.y, heng2.transform.localPosition.z);
        heng3.transform.localPosition = new Vector3(1500, heng3.transform.localPosition.y, heng3.transform.localPosition.z);
        heng4.transform.localPosition = new Vector3(1500, heng4.transform.localPosition.y, heng4.transform.localPosition.z);
        toggleSwitch.transform.localPosition = new Vector3(1500, toggleSwitch.transform.localPosition.y, toggleSwitch.transform.localPosition.z);

        scroll.transform.localPosition = new Vector3(1500, scroll.transform.localPosition.y, scroll.transform.localPosition.z);
        info.transform.localPosition = new Vector3(1500, info.transform.localPosition.y, info.transform.localPosition.z);
        icon.transform.localPosition = new Vector3(1500, icon.transform.localPosition.y, icon.transform.localPosition.z);
    }
    /// <summary>
    /// 探索界面编队界面退出动画
    /// </summary>
    /// <param name="troops"></param>
    /// <param name="bg"></param>
    /// <param name="title"></param>
    /// <param name="heng"></param>
    /// <param name="heng2"></param>
    /// <param name="heng3"></param>
    /// <param name="toggleSwitch"></param>
    /// <param name="scroll"></param>
    /// <param name="heng4"></param>
    /// <param name="troopsGroup"></param>
    /// <param name="slider"></param>
    /// <param name="mainTitle"></param>
    /// <param name="info"></param>
    /// <param name="icon"></param>
    public static void TroopsExitAnimate(GameObject troops, GameObject bg, GameObject title, GameObject heng, GameObject heng2,
     GameObject heng3, GameObject toggleSwitch, GameObject scroll, GameObject heng4,
     GameObject troopsGroup, GameObject slider, GameObject mainTitle, GameObject info, GameObject icon)
    {

        troopsGroup.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InOutBack).SetDelay(1.5f);

        bg.GetComponent<Image>().DOFade(0f, 1);
        title.transform.DOLocalMoveX(1000, 1).SetEase(Ease.InOutBack);
        heng.transform.DOMoveX(1500, 2).SetEase(Ease.InOutBack);
        heng2.transform.DOMoveX(1500, 1.5f).SetEase(Ease.InOutBack);
        heng3.transform.DOMoveX(1500, 1.5f).SetEase(Ease.InOutBack);
        toggleSwitch.transform.DOLocalMoveX(1500, 1.5f).SetEase(Ease.InOutBack);
        scroll.transform.DOLocalMoveX(1500, 2).SetEase(Ease.InOutBack);
        heng4.transform.DOLocalMoveX(1500, 2).SetEase(Ease.InOutBack);
        info.transform.DOLocalMoveX(1500, 1.2f).SetEase(Ease.InOutBack);
        icon.transform.DOLocalMoveX(1500, 1.5f).SetEase(Ease.InOutBack);


    }


}
