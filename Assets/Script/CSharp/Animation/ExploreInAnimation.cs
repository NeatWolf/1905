using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ExploreInAnimation : MonoBehaviour
{
    GameObject Img_Troops,copy,copyTop;
    private void Awake() {
        Img_Troops=GetComponent<UISubObject>().go[0].gameObject;
        copy=GetComponent<UISubObject>().go[1].gameObject;
        copyTop=GetComponent<UISubObject>().go[2].gameObject;
    }
    private void OnEnable() {
        ExploreInEnterAnimate();
    }

    /// <summary>
    /// 采集界面入场动画
    /// </summary>
     void ExploreInEnterAnimate(){
        // Img_Troops.transform.DORotate(new Vector3(0,0,0),1).SetEase(Ease.InOutBack);
       
        for (int i = 0; i < 4; i++)
        {
            Img_Troops.transform.GetChild(i+1).DOMove(copy.transform.GetChild(i+1).transform.position,0.5f+0.2f*(5-i)).SetEase(Ease.InOutBack);
             Img_Troops.transform.GetChild(i+1).DORotate(new Vector3(0,0,360),1+0.2f*(5-i),RotateMode.FastBeyond360);
        }

     }

    /// <summary>
    /// 采集界面出场动画
    /// </summary>
     void ExploreInExitAnimate(){
         for (int i = 0; i < 4; i++)
        {
            Img_Troops.transform.GetChild(i+1).DOMove(copyTop.transform.GetChild(i+1).transform.position,0.5f+0.2f*(5-i)).SetEase(Ease.InOutBack);
            Img_Troops.transform.GetChild(i+1).DORotate(new Vector3(0,0,360),1+0.2f*(5-i),RotateMode.FastBeyond360);
        }
     }
    
}
