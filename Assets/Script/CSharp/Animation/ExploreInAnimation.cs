using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;



public class ExploreInAnimation : MonoBehaviour
{
    public int sceneIndex = 0;
    GameObject Img_Troops, copy, copyTop, group, package, listBG, packagePos;
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



    }
    private void OnEnable()
    {

        ExploreInEnterAnimate();
        //背包界面入场前设置
        package.transform.localScale = Vector3.zero;
        package.transform.position = btnBack.transform.position;

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
    /// 信息列表刷新动画,待完善
    /// </summary>
    void ExploreInInfoEnterAnimate()
    {
        //列表刷新动画
        // GridLayoutGroup glg = group.GetComponent<GridLayoutGroup>();
        //  UnityEditorInternal.ComponentUtility.CopyComponent(glg);
        // DestroyImmediate(group.GetComponent<GridLayoutGroup>());
        // for (int i = 0; i < group.transform.childCount; i++)
        // {
        //     group.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPosY(group.transform.GetChild(group.transform.childCount - 1).GetComponent<RectTransform>().anchoredPosition.y, 1).From().SetEase(Ease.InOutBack).SetDelay(0.5f).onComplete = () =>
        //     {

        //         // UnityEditorInternal.ComponentUtility.PasteComponentAsNew(group);
        //         group.GetComponent<GridLayoutGroup>().enabled = true;
        //     };
        // }

        

    }

    /// <summary>
    /// 背包界面入场动画
    /// </summary>
    void ExploreInPackageEnterAnimate()
    {
        package.SetActive(true);

        package.GetComponent<Image>().DOColor(new Color(1, 1, 1, 1), 1f).SetEase(Ease.InOutBack);

        package.transform.DOScale(1, 1).SetEase(Ease.InOutBack);
        package.transform.DOMove(packagePos.transform.position, 1).SetEase(Ease.InOutBack).onComplete=()=>{
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
