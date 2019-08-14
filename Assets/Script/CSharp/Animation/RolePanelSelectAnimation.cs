using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;
using DG.Tweening;
using UnityEngine.Events;


public class RolePanelSelectAnimation : MonoBehaviour
{
    public UnityAction changeRoleEvent;
    public Scrollbar scrollbar;
    public ScrollRect scrollRect;
    public Image rolePage;
    public GameObject touchMask;
    public Canvas canvas;
    public int currentRoleID;

    [Header("头像缩放系数")]
    public float scaleRange = 1;

    [Header("立绘缩放系数")]
    public float pageScaleRange = 1;

    [Header("立绘移动范围")]
    public float pageMoveRange = 1;

    [Header("回弹速度阈值 越小越不易回弹")]
    public float backSpeed = 1;


    [Header("回弹时切换到下一个的阈值 越大越不易切换")]
    public float nextValue = 1f;
    [Header("点击插值 越大切换越快")]
    public float clickLerp = 1;
    [Header("回弹插值 越大回弹越快")]
    public float backLerp = 1;

    public Color normalColor = Color.white;
    public Color selectColor = Color.white;
    public AnimationCurve curve;
    public AnimationCurve roleCurve;

    float changeDistance = 0;
    float OPosition = 0;
    Vector3 ORolePosition;
    float timer = 0;
    Button[] buttons;
    float targetValue = 0;
    float lastValue = 0;
    bool needBack = false;
    float lerp = 0.15f;
    private void Start()
    {
        buttons = new Button[transform.childCount];
        ORolePosition = rolePage.transform.position;
        OPosition = canvas.worldCamera.ScreenToWorldPoint(new Vector3(Screen.width * 0.5f, 0, 0)).x;
        for (int i = 0; i < transform.childCount; i++)
        {
            int j = i;
            buttons[j] = transform.GetChild(j).GetComponent<Button>();
            buttons[j].transform.GetChild(0).GetComponent<Image>().color = normalColor;
            buttons[j].onClick.AddListener(() =>
            {
                ClickRole(j);
            });
        }
        //添加事件，选中第一个
        scrollRect.onValueChanged.AddListener(OnValueChange);
        currentRoleID = 0;
        buttons[currentRoleID].transform.GetChild(0).GetComponent<Image>().color = selectColor;
        scrollbar.value = 0.001f;
    }

    //滑动时根据和初始位置的偏移改变每个子物体的缩放,判断当前选中的role
    public void OnValueChange(Vector2 v)
    {
        if (changeDistance == 0)
            changeDistance = Mathf.Abs(buttons[0].transform.position.x - buttons[1].transform.position.x) * 0.5f;
        for (int i = 0; i < buttons.Length; i++)
        {
            int j = i;
            float eachDistance = buttons[j].transform.position.x - OPosition;
            float value = curve.Evaluate(eachDistance / (Screen.width * 0.025f * scaleRange));
            buttons[j].transform.localScale = new Vector3(value, value, 1);
            //改变当前角色
            if (currentRoleID == j && Mathf.Abs(eachDistance) > changeDistance)
            {
                buttons[currentRoleID].transform.GetChild(0).GetComponent<Image>().DOColor(normalColor, 0.15f);
                //得出下一个角色
                int nextIndex = 0;
                if (eachDistance > 0)
                    nextIndex = Mathf.Clamp(currentRoleID - 1, 0, buttons.Length - 1);
                else
                    nextIndex = Mathf.Clamp(currentRoleID + 1, 0, buttons.Length - 1);
                currentRoleID = nextIndex;
                buttons[nextIndex].transform.GetChild(0).GetComponent<Image>().DOColor(selectColor, 0.15f).onComplete = () =>
                {
                    if (changeRoleEvent != null) changeRoleEvent();
                };

            }
        }

        //松手时移动速度小于设定则回弹
        if (Input.GetMouseButtonUp(0)) needBack = true;
        float speed = Mathf.Abs(lastValue - scrollbar.value);
        float dir = buttons[currentRoleID].transform.position.x - OPosition;
        float distance = Mathf.Abs(dir);
        if (needBack && speed < backSpeed * 0.0005f && distance > 0.01f)
        {
            needBack = false;
            if (distance > nextValue && lastValue - scrollbar.value > 0 && dir > 0)
            {
                ClickRole(Mathf.Clamp((currentRoleID - 1), 0, buttons.Length - 1), true);
            }
            else if (distance > nextValue && lastValue - scrollbar.value < 0 && dir < 0)
            {
                ClickRole(Mathf.Clamp((currentRoleID + 1), 0, buttons.Length - 1), true);
            }
            else
            {
                ClickRole(currentRoleID, true);
            }
        }

        lastValue = scrollbar.value;

        //移动立绘
        MovePage();
    }

    //点击头像获取该角色在子物体中的占比得出滑动条目标value，插值前往该value
    public void ClickRole(int currentID, bool isBack = false)
    {
        lerp = isBack ? backLerp : clickLerp;
        targetValue = currentID / (buttons.Length - 1.0f);
        touchMask.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            if (timer <= 0)
            {

            }
        }
        if (touchMask.activeSelf)
        {
            scrollbar.value = Mathf.Lerp(scrollbar.value, targetValue, 0.15f * lerp);
            if (Mathf.Abs(scrollbar.value - targetValue) < 0.001f)
            {
                touchMask.SetActive(false);
            }
        }
    }

    void MovePage()
    {
        float distance = buttons[currentRoleID].transform.position.x - OPosition;
        float value = roleCurve.Evaluate(distance / (Screen.width * 0.015f * pageScaleRange));
        rolePage.color = new Color(1, 1, 1, value);
        rolePage.transform.position = new Vector3(ORolePosition.x + distance * 0.2f * pageMoveRange, ORolePosition.y, ORolePosition.z);
    }
}
