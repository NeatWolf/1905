using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using XLua;




public class ExploreInAnimation : MonoBehaviour
{
    
    GameObject Img_Troops, copy, copyTop, group, package, listBG, packagePos, cameraRotate;
    Button btnBack, listBack;
    /// <summary>
    /// 采集界面入场Action
    /// </summary>
    public UnityAction Action_ExploreInEnterAnimate;
    /// <summary>
    /// 采集界面出场Action
    /// </summary>
    public UnityAction Action_ExploreInExitAnimate;
    /// <summary>
    /// 背包界面入场动画Action
    /// </summary>
    public UnityAction Action_ExploreInPackageEnterAnimate;
    /// <summary>
    /// 背包界面出场界面Action
    /// </summary>
    public UnityAction Action_ExploreInPackageExitAnimate;

   
   
    
    private void Awake()
    {
        Img_Troops = GetComponent<UISubObject>().go[0];
        copy = GetComponent<UISubObject>().go[1];
        copyTop = GetComponent<UISubObject>().go[2];
        group = GetComponent<UISubObject>().go[3];
        package = GetComponent<UISubObject>().go[4];
        btnBack = GetComponent<UISubObject>().buttons[0];
        listBG = GetComponent<UISubObject>().go[5];
        listBack = GetComponent<UISubObject>().buttons[1];
        packagePos = GetComponent<UISubObject>().go[6];
        cameraRotate = GameObject.Find("ExploreIn/CameraRotate");





    }

    [LuaCallCSharp]
    public void Init(int sceneIndex)
    {
        
        
        ExploreInEnterAnimate();
        //背包界面入场前设置
        package.transform.localScale = Vector3.zero;
        package.transform.position = btnBack.transform.position;




         //相机动画
        
        switch (sceneIndex)
        {
            //1: 0度
            case 1:
                cameraRotate.transform.DOLocalRotate(new Vector3(0, 0, 0), 2,RotateMode.FastBeyond360);
                cameraRotate.transform.GetChild(0).GetComponent<Camera>().DOOrthoSize(20, 1).OnComplete(() =>
                {
                    cameraRotate.transform.GetChild(0).GetComponent<Camera>().DOOrthoSize(7.5f, 1).SetEase(Ease.InOutBack);
                });
                break;
            //2: 90度
            case 2:
                cameraRotate.transform.DOLocalRotate(new Vector3(0, 90, 0), 2,RotateMode.FastBeyond360);

                cameraRotate.transform.GetChild(0).GetComponent<Camera>().DOOrthoSize(20, 1).OnComplete(() =>
                {
                    cameraRotate.transform.GetChild(0).GetComponent<Camera>().DOOrthoSize(7.5f, 1).SetEase(Ease.InOutBack);
                });
                break;
            //3:180度
            case 3:
                cameraRotate.transform.DOLocalRotate(new Vector3(0, 180, 0), 2,RotateMode.FastBeyond360);
                cameraRotate.transform.DOLocalMove(new Vector3(16.2f, 0, -6.1f), 2);
                cameraRotate.transform.GetChild(0).GetComponent<Camera>().DOOrthoSize(20, 1).OnComplete(() =>
                {
                    cameraRotate.transform.GetChild(0).GetComponent<Camera>().DOOrthoSize(7.5f, 1).SetEase(Ease.InOutBack);
                });
                break;
            //4:270度
            case 4:
                cameraRotate.transform.DOLocalRotate(new Vector3(0, 270, 0), 2,RotateMode.FastBeyond360);
                cameraRotate.transform.DOLocalMove(new Vector3(-7.4f, 0, -14), 2);
                cameraRotate.transform.GetChild(0).GetComponent<Camera>().DOOrthoSize(20, 1).OnComplete(() =>
                {
                    cameraRotate.transform.GetChild(0).GetComponent<Camera>().DOOrthoSize(7.5f, 1).SetEase(Ease.InOutBack);
                });
                break;

        }


    }

    private void Start()
    {
        btnBack.onClick.AddListener(() =>
        {
            ExploreInPackageEnterAnimate();
        });
        listBack.onClick.AddListener(() =>
        {
            ExploreInPackageExitAnimate();
        });


       

    }


    /// <summary>
    /// 采集界面入场动画
    /// </summary>
    void ExploreInEnterAnimate()
    {




        group.GetComponent<RectTransform>().DOAnchorPosX(-1000, 1).From().SetEase(Ease.InOutBack);




        for (int i = 0; i < 4; i++)
        {


            Img_Troops.transform.GetChild(i + 1).DOMove(copy.transform.GetChild(i + 1).transform.position, 0.5f + 0.2f * (5 - i)).SetEase(Ease.InOutBack);
            Tweener t = Img_Troops.transform.GetChild(i + 1).DORotate(new Vector3(0, 0, 360), 1 + 0.2f * (5 - i), RotateMode.FastBeyond360);
            if (i == 3)
            {
                t.onComplete = () =>
                {
                    if (Action_ExploreInEnterAnimate != null) Action_ExploreInEnterAnimate();

                };

            }
        }


    }

    /// <summary>
    /// 采集界面出场动画
    /// </summary>
    void ExploreInExitAnimate()
    {
        for (int i = 0; i < 4; i++)
        {
            Img_Troops.transform.GetChild(i + 1).DOMove(copyTop.transform.GetChild(i + 1).transform.position, 0.5f + 0.2f * (5 - i)).SetEase(Ease.InOutBack);
            Tweener t = Img_Troops.transform.GetChild(i + 1).DORotate(new Vector3(0, 0, 360), 1 + 0.2f * (5 - i), RotateMode.FastBeyond360);
            if (i == 3)
            {
                t.onComplete = () =>
                {
                    if (Action_ExploreInExitAnimate != null) Action_ExploreInExitAnimate();

                };

            }
        }
    }
    /// <summary>
    /// 信息列表刷新动画,添加子物体前手动调用
    /// </summary>
    void ExploreInInfoEnterAnimate()
    {
        //列表刷新动画
        
        
        
        
        for (int i = 0; i < group.transform.childCount; i++)
        {
            group.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPosY(group.transform.GetChild(group.transform.childCount - 1).GetComponent<RectTransform>().anchoredPosition.y, 1).From().SetEase(Ease.InOutBack).SetDelay(0.5f) ;
            
        }



    }

    /// <summary>
    /// 背包界面入场动画
    /// </summary>
    void ExploreInPackageEnterAnimate()
    {
        package.SetActive(true);

        package.GetComponent<Image>().DOColor(new Color(1, 1, 1, 1), 1f).SetEase(Ease.InOutBack);

        package.transform.DOScale(1, 1).SetEase(Ease.InOutBack);
        package.transform.DOMove(packagePos.transform.position, 1).SetEase(Ease.InOutBack).onComplete = () =>
        {
            if (Action_ExploreInPackageEnterAnimate != null) Action_ExploreInPackageEnterAnimate();
        };

    }
    /// <summary>
    /// 背包界面出场动画
    /// </summary>
    void ExploreInPackageExitAnimate()
    {
        package.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 1f).SetEase(Ease.InOutBack);

        package.transform.DOScale(0, 1).SetEase(Ease.InOutBack);
        package.transform.DOMove(btnBack.transform.position, 1).onComplete = () =>
        {
            package.SetActive(false);
            if (Action_ExploreInPackageExitAnimate != null) Action_ExploreInPackageExitAnimate();
        };
    }

   


}
