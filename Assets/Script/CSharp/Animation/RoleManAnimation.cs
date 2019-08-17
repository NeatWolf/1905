using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class RoleManAnimation : MonoBehaviour
{

    GameObject roleName;
    GameObject roleValue;
    /// <summary>
    /// 未选择的Role
    /// </summary>
    GameObject[] otherRole;
    /// <summary>
    /// 当前角色av
    /// </summary>
    GameObject currentRole;
    /// <summary>
    /// 当前角色av作为子物体的下标
    /// </summary>
    int currentRoleIndex;
    Button[] btns;
    GameObject[] canvas;
    /// <summary>
    /// RoleAv父物体
    /// </summary>
    GameObject content, contentOld;
    /// <summary>
    /// 立绘
    /// </summary>
    GameObject roleTexture;
    GameObject intro;
    GameObject EquipGroup, ScrollView, BG_Equip, BG_Level, BG_Food;

    /// <summary>
    /// 控制个界面的back按钮
    /// </summary>
    int backNum = 0;


    ///
    Vector3 roleTexturePos, introPos, currentRolePos, roleValuePos, roleNamePos;
    Vector3[] btnsPos = new Vector3[3];
    Vector3[] otherRolePos;


    private void Awake()
    {
        BG_Level = GetComponent<UISubObject>().go[3];
        btns = GetComponent<UISubObject>().buttons;
        contentOld = GetComponent<UISubObject>().go[6];
        roleTexture = GetComponent<UISubObject>().go[7];
        intro = GetComponent<UISubObject>().go[8];
        roleValue = GetComponent<UISubObject>().go[9];
        roleName = GetComponent<UISubObject>().go[10];
        EquipGroup = GetComponent<UISubObject>().go[11];
        ScrollView = GetComponent<UISubObject>().go[12];
        BG_Equip = GetComponent<UISubObject>().go[13];
        BG_Food = GetComponent<UISubObject>().go[5];

        BG_Equip.SetActive(false);
        BG_Level.SetActive(false);

    }
    private void OnEnable()
    {

        intro.transform.GetChild(0).gameObject.SetActive(false);
        intro.transform.GetChild(1).gameObject.SetActive(false);

        AnimateManager.AddButtonsAnimate(btns);

        //原坐标
        roleTexturePos = roleTexture.GetComponent<RectTransform>().position;
        introPos = intro.GetComponent<RectTransform>().position;

        roleValuePos = roleValue.GetComponent<RectTransform>().position;
        roleNamePos = roleName.GetComponent<RectTransform>().position;

        for (int i = 0; i < 3; i++)
        {
            btnsPos[i] = btns[i].GetComponent<RectTransform>().position;
        }



    }

    private void Start()
    {
        btns[2].onClick.AddListener(() =>
        {

            EquipClickAnimate();


        });


        btns[0].onClick.AddListener(() =>
    {
        EquipExitAnimate();

    });



        btns[0].onClick.AddListener(() =>
    {
        LevelExitAnimate();

    });


        btns[1].onClick.AddListener(() =>
        {

            LevelEnterAnimate();

        });
        btns[3].onClick.AddListener(() =>
        {
            FoodEntrAnimate();
        });

    }


    //x:715  选择的role av位置
    /// <summary>
    /// 装备按钮点击，装备界面入场动画
    /// </summary>
    public void EquipClickAnimate()
    {
        content = Instantiate(contentOld);

        content.name = contentOld.name + "Copy";
        content.transform.parent = contentOld.transform.parent;
        content.transform.position = contentOld.transform.transform.position;
        content.transform.localPosition = Vector3.zero;
        content.transform.localRotation = Quaternion.identity;
        content.transform.localScale = Vector3.one;

        contentOld.SetActive(false);
        content.SetActive(true);

        BG_Equip.SetActive(true);
        for (int i = 0; i < content.transform.childCount; i++)
        {
            if (content.transform.GetChild(i).transform.localPosition.x == 715)
            {
                
                currentRole = content.transform.GetChild(i).gameObject;
                currentRolePos = currentRole.GetComponent<RectTransform>().position;
                currentRoleIndex = i;
                break;
            }

        }


        //获取非currentRole的角色
        otherRole = new GameObject[content.transform.childCount - 1];
        otherRolePos = new Vector3[content.transform.childCount - 1];

        for (int i = 0; i < otherRole.Length + 1; i++)
        {
            if (i < currentRoleIndex)
            {
                otherRole[i] = content.transform.GetChild(i).gameObject;
            }
            else if (i > currentRoleIndex)
            {
                otherRole[i - 1] = content.transform.GetChild(i).gameObject;
            }


        }
        Debug.Log(otherRole[0].name);
        for (int i = 0; i < otherRole.Length; i++)
        {

            otherRole[i].GetComponent<RectTransform>().DOAnchorPosX(otherRole[i].GetComponent<RectTransform>().anchoredPosition.x - 1500, (1f + 0.2f * i)).SetEase(Ease.InOutBack);

        }

        roleTexture.transform.DOLocalMoveX(-1500, 1f).SetEase(Ease.InOutBack);
        intro.transform.DOLocalMoveX(-301, 1f).SetEase(Ease.InOutBack);
        currentRole.transform.DOLocalMoveX(415, 1f).SetEase(Ease.InOutBack);

        roleValue.GetComponent<RectTransform>().DOAnchorPosX(roleValue.GetComponent<RectTransform>().anchoredPosition.x - 1500, 1).SetEase(Ease.InOutBack);

        btns[2].GetComponent<RectTransform>().DOAnchorPosX(btns[2].GetComponent<RectTransform>().anchoredPosition.x - 1500, 1).SetEase(Ease.InOutBack);


        btns[2].transform.DOLocalRotate(new Vector3(0, 720, 0), 1, RotateMode.FastBeyond360);

        intro.transform.GetChild(0).gameObject.SetActive(true);

        intro.transform.GetChild(1).gameObject.SetActive(true);
        btns[1].gameObject.SetActive(false);

        roleName.GetComponent<RectTransform>().DOAnchorPosX(roleName.GetComponent<RectTransform>().anchoredPosition.x - 301 + 12, 1).SetEase(Ease.InOutBack);
        roleName.transform.DOScale(0.755f, 1).SetEase(Ease.InOutBack);
        roleName.GetComponent<Image>().DOColor(new Color(0.054f, 0.368f, 0.360f, 0.4f), 1);

        //
        for (int i = 0; i < EquipGroup.transform.childCount; i++)
        {
            EquipGroup.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPosX(EquipGroup.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition.x - 800, 1 + 0.2f * i).SetEase(Ease.InOutBack);
        }


        ScrollView.GetComponent<RectTransform>().DOAnchorPosX(ScrollView.GetComponent<RectTransform>().anchoredPosition.x - 800, 1).SetEase(Ease.InOutBack);

    }

    /// <summary>
    /// 装备界面退出动画
    /// </summary>
    public void EquipExitAnimate()
    {


        for (int i = 0; i < otherRole.Length; i++)
        {
            otherRole[i].GetComponent<RectTransform>().DOAnchorPosX(otherRole[i].GetComponent<RectTransform>().anchoredPosition.x + 1500, (1f + 0.2f * i)).SetEase(Ease.InOutBack);

        }

        roleTexture.transform.DOMoveX(roleTexturePos.x, 1f).SetEase(Ease.InOutBack);
        intro.transform.DOMoveX(introPos.x, 1f).SetEase(Ease.InOutBack);
        currentRole.transform.DOLocalMoveX(715, 1f).SetEase(Ease.InOutBack);

        roleValue.GetComponent<RectTransform>().DOAnchorPosX(roleValue.GetComponent<RectTransform>().anchoredPosition.x + 1500, 1).SetEase(Ease.InOutBack);
        btns[2].GetComponent<RectTransform>().DOAnchorPosX(btns[2].GetComponent<RectTransform>().anchoredPosition.x + 1500, 1).SetEase(Ease.InOutBack);
        btns[2].transform.DORotate(new Vector3(0, 720, 0), 1, RotateMode.FastBeyond360);

        intro.transform.GetChild(0).gameObject.SetActive(false);

        intro.transform.GetChild(1).gameObject.SetActive(false);
        btns[1].gameObject.SetActive(true);


        roleName.GetComponent<RectTransform>().DOAnchorPosX(roleName.GetComponent<RectTransform>().anchoredPosition.x + 301 - 12, 1).SetEase(Ease.InOutBack);
        roleName.transform.DOScale(1f, 1).SetEase(Ease.InOutBack);
        roleName.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0.4f), 1);

        //

        for (int i = 0; i < EquipGroup.transform.childCount; i++)
        {
            EquipGroup.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPosX(EquipGroup.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition.x + 800, 1 + 0.2f * i).SetEase(Ease.InOutBack);
        }


        ScrollView.GetComponent<RectTransform>().DOAnchorPosX(ScrollView.GetComponent<RectTransform>().anchoredPosition.x + 800, 1).SetEase(Ease.InOutBack).onComplete = () =>
        {
            BG_Equip.SetActive(false);
            content.SetActive(false);
            contentOld.SetActive(true);


        };

    }

    /// <summary>
    /// 等级界面进入动画
    /// </summary>
    public void LevelEnterAnimate()
    {
        BG_Level.SetActive(true);
        BG_Level.GetComponent<Image>().DOFade(0.4f, 0.5f);
        BG_Level.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosX(BG_Level.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x - 850, 1f).SetEase(Ease.InOutBack);

    }
    /// <summary>
    /// 等级界面退出动画
    /// </summary>
    public void LevelExitAnimate()
    {

        BG_Level.GetComponent<Image>().DOFade(0, 0.5f);
        BG_Level.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosX(BG_Level.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x + 850, 1f).SetEase(Ease.InOutBack).onComplete = () =>
        {
            BG_Level.SetActive(false);
        };
    }

    /// <summary>
    /// 食物界面进入动画
    /// </summary>
    public void FoodEntrAnimate()
    {
        BG_Food.SetActive(true);
        BG_Food.transform.GetComponent<Image>().DOFade(0.4f, 0.5f);
        BG_Food.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosX(BG_Food.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x - 1200, 1f).SetEase(Ease.InOutBack);

    }
    /// <summary>
    /// 食物界面退出动画
    /// </summary>
    public void FoodExitAnimate()
    {
        BG_Food.transform.GetComponent<Image>().DOFade(0, 0.5f);
        BG_Food.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosX(BG_Food.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x + 1200, 1f).SetEase(Ease.InOutBack).onComplete = () =>
        {
            BG_Food.SetActive(false);
        };
    }
}
