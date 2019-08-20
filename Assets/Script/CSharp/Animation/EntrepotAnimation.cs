using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EntrepotAnimation : MonoBehaviour
{

    Button[] btns;
    GameObject[] canvas;
    //出售记录列表
    GameObject Records;
    //ScrollView中的content
    GameObject content;
    GameObject list,glass;
    
    private void Awake()
    {

        btns = GetComponent<UISubObject>().buttons;
        Records=GetComponent<UISubObject>().go[2];
        content=GetComponent<UISubObject>().go[3];
        list=GetComponent<UISubObject>().go[4];
        glass=GetComponent<UISubObject>().go[5];
        
        

        

        //设为最后一个
        content.transform.GetChild(0).SetAsLastSibling();


    }
    private void OnEnable()
    {
        
        glass.GetComponent<Image>().color=new Color(1,1,1,0);
        //添加Button动画
       // AnimateManager.AddButtonAnimate(btns[0]);
        //设置Records失活状态时的位置
        AnimateManager.RecordPreviousAnimate(Records);
        //货架入场动画
        EntrepotEnterAnimate();

    }

    //btns[1]---出售记录按钮
    private void Start()
    {
        btns[1].onClick.AddListener(() =>
        {
            AnimateManager.RecordEnterAnimate(Records, content);

        });
        

    }
    /// <summary>
    /// 货架界面进场动画
    /// </summary>
    public void EntrepotEnterAnimate(){
        list.GetComponent<RectTransform>().DOAnchorPosX(list.GetComponent<RectTransform>().anchoredPosition.x-1050,1).SetEase(Ease.InOutBack);
        glass.GetComponent<Image>().DOFade(1,0.3f).SetDelay(1);
        
    }

    /// <summary>
    /// 货架界面出场动画
    /// </summary>
    public void EntrepotExitAnimate(){
        glass.GetComponent<Image>().DOFade(0,0.3f);
        list.GetComponent<RectTransform>().DOAnchorPosX(list.GetComponent<RectTransform>().anchoredPosition.x+1050,1).SetEase(Ease.InOutBack).SetDelay(0.3f);
        
    }
    
    






}
