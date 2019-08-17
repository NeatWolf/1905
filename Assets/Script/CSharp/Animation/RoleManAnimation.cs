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
    Button btn_backE;

    ScrollRect scrollRect;

    RolePanelSelectAnimation rolePanelSelectAnimation;

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
        content = GetComponent<UISubObject>().go[6];
        roleTexture = GetComponent<UISubObject>().go[7];
        intro = GetComponent<UISubObject>().go[8];
        roleValue = GetComponent<UISubObject>().go[9];
        roleName = GetComponent<UISubObject>().go[10];
        EquipGroup = GetComponent<UISubObject>().go[11];
        ScrollView = GetComponent<UISubObject>().go[12];
        BG_Equip = GetComponent<UISubObject>().go[4];
        BG_Food = GetComponent<UISubObject>().go[5];
        btn_backE = GetComponent<UISubObject>().go[13].GetComponent<Button>();
        scrollRect = GetComponent<UISubObject>().go[14].GetComponent<ScrollRect>();

        BG_Equip.SetActive(false);
        BG_Level.SetActive(false);

        rolePanelSelectAnimation = GetComponent<UISubObject>().go[6].GetComponent<RolePanelSelectAnimation>();

    }
    private void OnEnable()
    {
        btn_backE.gameObject.SetActive(false);

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


    // private void Update()
    // {
    //     currentRole = rolePanelSelectAnimation.transform.GetChild(rolePanelSelectAnimation.currentRoleID).gameObject;
    //     currentRoleIndex = rolePanelSelectAnimation.currentRoleID;

    //     // for (int i = 0; i < content.transform.childCount; i++)
    //     // {
    //     //     if (content.transform.GetChild(i) == currentRole)
    //     //     {

    //     //         currentRolePos = currentRole.GetComponent<RectTransform>().position;
    //     //         currentRoleIndex = i;

    //     //         break;

    //     //     }

    //     // }

    //     // Debug.Log(currentRole.name);
    // }


    //x:715  选择的role av位置
    /// <summary>
    /// 装备按钮点击，装备界面入场动画
    /// </summary>
    public void EquipClickAnimate(int roleID)
    {
        currentRoleIndex = roleID;
        currentRole = rolePanelSelectAnimation.transform.GetChild(currentRoleIndex).gameObject;
        currentRole.GetComponent<Button>().interactable = false;
        scrollRect.enabled = false;

        BG_Equip.SetActive(true);

        ScrollView.GetComponent<RectTransform>().DOAnchorPosX(ScrollView.GetComponent<RectTransform>().anchoredPosition.x - 800, 1).SetEase(Ease.InOutBack).onComplete = () =>
                {
                    btns[0].gameObject.SetActive(false);
                    btn_backE.gameObject.SetActive(true);
                };

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

        for (int i = 0; i < otherRole.Length; i++)
        {
            otherRole[i].GetComponent<RectTransform>().DOAnchorPosY(otherRole[i].GetComponent<RectTransform>().anchoredPosition.y + 1000, 1);
        }

        roleTexture.transform.DOLocalMoveX(-1500, 1f).SetEase(Ease.InOutBack);
        intro.transform.DOLocalMoveX(-301, 1f).SetEase(Ease.InOutBack);
        // currentRole.transform.DOLocalMoveX(415, 1f).SetEase(Ease.InOutBack);
        currentRole.GetComponent<RectTransform>().DOAnchorPosX(currentRole.GetComponent<RectTransform>().anchoredPosition.x - 298f, 1).SetEase(Ease.InOutBack);

        roleValue.GetComponent<RectTransform>().DOAnchorPosX(roleValue.GetComponent<RectTransform>().anchoredPosition.x - 1500, 1).SetEase(Ease.InOutBack);

        btns[2].GetComponent<RectTransform>().DOAnchorPosX(btns[2].GetComponent<RectTransform>().anchoredPosition.x - 1500, 1).SetEase(Ease.InOutBack);


        btns[2].transform.DOLocalRotate(new Vector3(0, 720, 0), 1, RotateMode.FastBeyond360);

        intro.transform.GetChild(0).gameObject.SetActive(true);

        //补给
        intro.transform.GetChild(1).gameObject.SetActive(true);
        intro.transform.GetChild(0).DOLocalMoveY(intro.transform.GetChild(0).localPosition.y - 1500, 1).From();
        intro.transform.GetChild(1).DOLocalMoveY(intro.transform.GetChild(1).localPosition.y - 500, 1).From();
        btns[1].gameObject.SetActive(false);

        roleName.GetComponent<RectTransform>().DOAnchorPosX(roleName.GetComponent<RectTransform>().anchoredPosition.x - 301 + 12, 1).SetEase(Ease.InOutBack);
        roleName.transform.DOScale(0.755f, 1).SetEase(Ease.InOutBack);
        roleName.GetComponent<Image>().DOColor(new Color(0.054f, 0.368f, 0.360f, 0.4f), 1);


        
        //
        for (int i = 0; i < EquipGroup.transform.childCount; i++)
        {
            EquipGroup.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPosX(EquipGroup.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition.x - 800, 0.5f + 0.2f * i).SetEase(Ease.InOutBack);
        }


    }

    /// <summary>
    /// 装备界面退出动画
    /// </summary>
    public void EquipExitAnimate()
    {
        currentRole.GetComponent<Button>().interactable = true;
        scrollRect.enabled = true;

        for (int i = 0; i < otherRole.Length; i++)
        {
            otherRole[i].GetComponent<RectTransform>().DOAnchorPosY(otherRole[i].GetComponent<RectTransform>().anchoredPosition.y - 1000, 1);

        }

        roleTexture.transform.DOMoveX(roleTexturePos.x, 1f).SetEase(Ease.InOutBack);
        intro.transform.DOMoveX(introPos.x, 1f).SetEase(Ease.InOutBack);
        currentRole.GetComponent<RectTransform>().DOAnchorPosX(currentRole.GetComponent<RectTransform>().anchoredPosition.x + 298f, 1).SetEase(Ease.InOutBack);


        roleValue.GetComponent<RectTransform>().DOAnchorPosX(roleValue.GetComponent<RectTransform>().anchoredPosition.x + 1500, 1).SetEase(Ease.InOutBack);
        btns[2].GetComponent<RectTransform>().DOAnchorPosX(btns[2].GetComponent<RectTransform>().anchoredPosition.x + 1500, 1).SetEase(Ease.InOutBack);
        btns[2].transform.DORotate(new Vector3(0, 720, 0), 1, RotateMode.FastBeyond360);


        intro.transform.GetChild(0).DOLocalMoveY(intro.transform.GetChild(0).localPosition.y - 1500, 1);

        intro.transform.GetChild(1).DOLocalMoveY(intro.transform.GetChild(1).localPosition.y - 500, 1);
        btns[1].gameObject.SetActive(true);


        roleName.GetComponent<RectTransform>().DOAnchorPosX(roleName.GetComponent<RectTransform>().anchoredPosition.x + 301 - 12, 1).SetEase(Ease.InOutBack);
        roleName.transform.DOScale(1f, 1).SetEase(Ease.InOutBack);
        roleName.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0.4f), 1);

        //
        

        for (int i = 0; i < EquipGroup.transform.childCount; i++)
        {
            EquipGroup.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPosX(EquipGroup.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition.x + 800, 0.5f + 0.2f * i).SetEase(Ease.InOutBack);
        }


        ScrollView.GetComponent<RectTransform>().DOAnchorPosX(ScrollView.GetComponent<RectTransform>().anchoredPosition.x + 800, 1).SetEase(Ease.InOutBack).onComplete = () =>
        {
            btns[0].gameObject.SetActive(true);
            btn_backE.gameObject.SetActive(false);

            BG_Equip.SetActive(false);

            intro.transform.GetChild(0).gameObject.SetActive(false);

            intro.transform.GetChild(1).gameObject.SetActive(false);
            intro.transform.GetChild(1).DOLocalMoveY(intro.transform.GetChild(1).localPosition.y + 500, 0.1f);
            intro.transform.GetChild(0).DOLocalMoveY(intro.transform.GetChild(1).localPosition.y + 1500, 0.1f);

        };


    }

    /// <summary>
    /// 等级界面进入动画
    /// </summary>
    public void LevelEnterAnimate()
    {

        BG_Level.SetActive(true);
        BG_Level.transform.GetChild(0).transform.localPosition = new Vector3(1000, 0, -33);

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
