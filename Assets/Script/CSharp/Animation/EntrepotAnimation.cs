using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntrepotAnimation : MonoBehaviour
{

    Button[] btns;
    GameObject[] canvas;
    //出售记录列表
    GameObject Records;
    //ScrollView中的content
    GameObject content;

    private void Awake()
    {

        btns = GetComponent<UISubObject>().buttons;
        canvas = GetComponent<UISubObject>().go;

        for (int i = 0; i < canvas[1].transform.GetChild(0).childCount; i++)
        {
            canvas[1].transform.GetChild(0).GetChild(i).gameObject.AddComponent<BtnAnimate>();

        }

        Records = canvas[2];
        content = canvas[3];

        //设为最后一个
        content.transform.GetChild(0).SetAsLastSibling();


    }
    private void OnEnable()
    {
        //添加相机
        AnimateManager.AddCams(canvas);
        //添加Button动画
        AnimateManager.AddButtonAnimate(btns[0]);
        //设置Records失活状态时的位置
        AnimateManager.RecordPreviousAnimate(Records);

    }

    //btns[1]---出售记录按钮
    private void Start()
    {
        btns[1].onClick.AddListener(() =>
        {
            AnimateManager.RecordEnterAnimate(Records, content);

        });

    }








}
