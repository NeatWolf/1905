using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.DemiLib;
using UnityEngine.EventSystems;

public class ExploreAnimate : MonoBehaviour, IDragHandler
{
    //是否开始滑动屏幕，true则取消silider与mainCam的关联
    [HideInInspector]
    public bool isDarp;
    [HideInInspector]
    public Slider sld;
    //场景中的相机
    Camera mainCam;
    //原主相机
    Camera oldCam;
    //场景中的区域选择按钮
    GameObject[] SceneBtn = new GameObject[4];
    //编队按钮，点击后弹出编队界面
    Button btnTroops, btnBack;
    //编队界面,编队背景，编队标题。。。
    GameObject Troops, BGTroops, TextTitle, heng, heng2, heng3, toggleSwitch,
    Scroll, heng4, TroopsGroup, Slider, mainTitle, info, icon, hengTop, hengBottom, circle;
    //编队组中的4张AV卡牌
    GameObject[] cards = new GameObject[4];

    [Header("曲线")]
    public AnimationCurve curve;

    //滑动类
    //ExploreDragAnimate eda;
    //slider与title的原位置，为返回动画而设
    Vector3 sliderPos, titlePos;

    ExploreSceneAnimation esa;
    private void Awake()
    {
        esa = new ExploreSceneAnimation();
        //eda = GameObject.Find("UI/DrapCanvas").GetComponent<ExploreDragAnimate>();
        sld = transform.Find("Slider").GetComponent<Slider>();
        mainCam = GameObject.Find("Explore/Main Camera").GetComponent<Camera>();
        oldCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        



        btnTroops = GetComponent<UISubObject>().buttons[0];
        btnBack = GetComponent<UISubObject>().buttons[1];

        Troops = GetComponent<UISubObject>().go[0];
        BGTroops = GetComponent<UISubObject>().go[1];
        TextTitle = GetComponent<UISubObject>().go[2];
        heng = GetComponent<UISubObject>().go[3];
        heng2 = GetComponent<UISubObject>().go[4];
        heng3 = GetComponent<UISubObject>().go[5];
        toggleSwitch = GetComponent<UISubObject>().go[6];
        Scroll = GetComponent<UISubObject>().go[7];
        heng4 = GetComponent<UISubObject>().go[8];
        TroopsGroup = GetComponent<UISubObject>().go[9];
        Slider = GetComponent<UISubObject>().go[10];
        mainTitle = GetComponent<UISubObject>().go[11];
        info = GetComponent<UISubObject>().go[12];
        icon = GetComponent<UISubObject>().go[13];
        hengTop = GetComponent<UISubObject>().go[14];
        hengBottom = GetComponent<UISubObject>().go[15];
        circle = GetComponent<UISubObject>().go[16];
        for (int i = 0; i < 4; i++)
        {
            cards[i] = GetComponent<UISubObject>().go[17].transform.GetChild(i).gameObject;
        }

        Transform t = GameObject.Find("Scene/Explore/CityPlane/Canvas").transform;
        for (int i = 0; i < 4; i++)
        {
            SceneBtn[i] = t.GetChild(i).GetChild(0).gameObject;
        }



    }
    private void OnEnable()
    {

        oldCam.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(true);
        AnimateManager.TroopsPreviousnimate(Troops, BGTroops, TextTitle, heng, heng2, heng3, btnTroops, toggleSwitch, Scroll, heng4, info, icon);

        ExploreEnterAnimate();


    }


    private void Start()
    {

        topORbottom = 1;
        //slider与title原位置
        sliderPos = Slider.transform.localPosition;
        titlePos = mainTitle.transform.localPosition;
        //编队按钮点击监听
        btnTroops.onClick.AddListener(() =>
        {
            Troops.gameObject.SetActive(true);
            AnimateManager.TroopsEnterAnimate(Troops, BGTroops, TextTitle, heng, heng2, heng3, toggleSwitch, Scroll, heng4, TroopsGroup, Slider, mainTitle, info, icon);
        });

        //探索场景按钮动画 旋转

        for (int i = 0; i < 4; i++)
        {
            float x = Mathf.Pow(-1, i);

            SceneBtn[i].transform.DOBlendableLocalRotateBy(new Vector3(0, 720, 0), 1.5f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo).SetEase(curve).SetDelay(2 * i);


        }




        //返回按钮动画
        btnBack.onClick.AddListener(() =>
        {
            Debug.Log("返回");
            AnimateManager.TroopsExitAnimate(Troops, BGTroops, TextTitle, heng, heng2, heng3, toggleSwitch, Scroll, heng4, TroopsGroup, Slider, mainTitle, info, icon);
            mainTitle.transform.DOLocalMove(titlePos, 1).SetEase(Ease.InOutBack).SetDelay(0.5f);
            Slider.transform.DOLocalMove(sliderPos, 1).SetEase(Ease.InOutBack).SetDelay(0.5f);
            StartCoroutine(backAnimate());

        });

    }

    private void Update()
    {


        //滑动条

        if (isDarp == false)
        {
            mainCam.transform.localPosition = new Vector3(0, 17, sld.value);
        }
        else
        {
            sld.value = Mathf.Clamp(mainCam.transform.localPosition.z, -21, -2);
        }

        timer += Time.deltaTime;


    }
    IEnumerator backAnimate()
    {

        yield return new WaitForSeconds(2);
        Troops.SetActive(false);
        Debug.Log("backanimate");
    }

    int topORbottom;
    float timer = 0;
    /// <summary>
    /// 编队组滑动动画
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {

        Sequence top = DOTween.Sequence();
        Sequence bottom = DOTween.Sequence();
        if (eventData.pressPosition.y < 400)
        {
            if (topORbottom == 1 && timer > 0.3f)
            {
                timer = 0;
                cards[0].transform.parent.SetSiblingIndex(2);

                top.Append(hengTop.transform.DOLocalMoveY(150, 0.3f));
                top.Insert(0, hengBottom.transform.DOLocalMoveY(-50, 0.3F));
                top.Insert(0, hengTop.transform.DOBlendableLocalRotateBy(new Vector3(360, 0, 0), 0.5f, RotateMode.FastBeyond360));
                top.Append(hengTop.transform.DOLocalMoveY(50, 0.3f));
                top.Insert(0, hengBottom.transform.DOBlendableLocalRotateBy(new Vector3(360, 0, 0), 0.5f, RotateMode.FastBeyond360));
                top.Insert(0.3f, hengBottom.transform.DOLocalMoveY(78, 0.3f));
                top.Insert(0, circle.transform.DOLocalRotate(new Vector3(0, 0, -30), 0.3f).SetEase(Ease.InOutBack));
                top.Insert(0.3f, circle.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.3f).SetEase(Ease.InOutBack));
                top.InsertCallback(0f, () =>
                {
                    for (int i = 0; i < 4; i++)
                    {
                        cards[i].transform.DOLocalMoveX(130, 0.3f * i).SetEase(Ease.InOutBack);
                        cards[i].transform.GetChild(0).GetComponent<Image>().DOFade(0, 0.5f * i);
                    }
                });
                top.InsertCallback(0.8f, () =>
                {
                    cards[0].transform.DOLocalMoveX(-435, 0.3f).SetEase(Ease.InOutBack);
                    cards[1].transform.DOLocalMoveX(-295, 0.6f).SetEase(Ease.InOutBack);
                    cards[2].transform.DOLocalMoveX(-155, 0.9f).SetEase(Ease.InOutBack);
                    cards[3].transform.DOLocalMoveX(-15, 1.2f).SetEase(Ease.InOutBack);
                    for (int i = 0; i < 4; i++)
                    {
                        cards[i].transform.GetChild(0).GetComponent<Image>().DOFade(1, 0.5f * i);
                    }
                });
                top.InsertCallback(0.3f, () =>
                {
                    hengTop.transform.SetSiblingIndex(0);
                });
                topORbottom = -1;

            }
            else
            {
                if (timer <= 0.3f) return;
                timer = 0;
                cards[0].transform.parent.SetSiblingIndex(2);
                bottom.Append(hengBottom.transform.DOLocalMoveY(150, 0.3f));
                bottom.Insert(0, hengTop.transform.DOLocalMoveY(-50, 0.3f));
                bottom.Insert(0, hengBottom.transform.DOBlendableLocalRotateBy(new Vector3(360, 0, 0), 0.5f, RotateMode.FastBeyond360));
                bottom.Append(hengBottom.transform.DOLocalMoveY(50, 0.3f));
                bottom.Insert(0, hengTop.transform.DOBlendableLocalRotateBy(new Vector3(360, 0, 0), 0.5f, RotateMode.FastBeyond360));
                bottom.Insert(0.3f, hengTop.transform.DOLocalMoveY(78, 0.3f));
                bottom.Insert(0, circle.transform.DOLocalRotate(new Vector3(0, 0, -30), 0.3f).SetEase(Ease.InOutBack));
                bottom.Insert(0.3f, circle.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.3f).SetEase(Ease.InOutBack));
                bottom.InsertCallback(0f, () =>
                {

                    for (int i = 0; i < 4; i++)
                    {
                        cards[i].transform.DOLocalMoveX(130, 0.3f * i).SetEase(Ease.InOutBack);
                        cards[i].transform.GetChild(0).GetComponent<Image>().DOFade(0, 0.5f * i);
                    }
                });
                bottom.InsertCallback(0.8f, () =>
                {
                    cards[0].transform.DOLocalMoveX(-435, 0.3f).SetEase(Ease.InOutBack);
                    cards[1].transform.DOLocalMoveX(-295, 0.6f).SetEase(Ease.InOutBack);
                    cards[2].transform.DOLocalMoveX(-155, 0.9f).SetEase(Ease.InOutBack);
                    cards[3].transform.DOLocalMoveX(-15, 1.2f).SetEase(Ease.InOutBack);
                    for (int i = 0; i < 4; i++)
                    {
                        cards[i].transform.GetChild(0).GetComponent<Image>().DOFade(1, 0.5f * i);
                    }
                });
                bottom.InsertCallback(0.3f, () =>
                {
                    hengBottom.transform.SetSiblingIndex(0);
                });

                topORbottom = 1;

            }


        }

    }
    //探索界面入场动画
    void ExploreEnterAnimate()
    {
        UISubObject uISub = GetComponent<UISubObject>();
        uISub.go[11].GetComponent<RectTransform>().DOAnchorPosX(uISub.go[2].GetComponent<RectTransform>().anchoredPosition.x - 1500, 1).SetEase(Ease.InOutBack).From();
        uISub.go[10].GetComponent<RectTransform>().DOAnchorPosX(uISub.go[10].GetComponent<RectTransform>().anchoredPosition.x + 500, 1).SetEase(Ease.InOutBack).From();
        uISub.go[9].GetComponent<RectTransform>().DOAnchorPosY(uISub.go[9].GetComponent<RectTransform>().anchoredPosition.x - 500, 1).SetEase(Ease.InOutBack).From();
        uISub.buttons[1].GetComponent<RectTransform>().DOAnchorPosY(uISub.go[9].GetComponent<RectTransform>().anchoredPosition.x + 500, 1).SetEase(Ease.InOutBack).From();

        uISub.go[18].GetComponent<Image>().DOColor(new Color(1,1,1,0),1).From();
    }
}
