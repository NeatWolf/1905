using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntrepotAnimation : MonoBehaviour
{
   
    Button[] btns;
    GameObject[] canvas;
    
    private void Awake()
    {

        btns = GetComponent<UISubObject>().buttons;
        canvas = GetComponent<UISubObject>().go;

        for (int i = 0; i < canvas[1].transform.GetChild(0).childCount; i++)
        {
            canvas[1].transform.GetChild(0).GetChild(i).gameObject.AddComponent<BtnAnimate>();

        }


    }
    private void OnEnable()
    {
        //添加相机
        AnimateManager.AddCams(canvas);
        //添加Button动画
        AnimateManager.AddButtonsAnimate(btns);
       

    }
    






}
