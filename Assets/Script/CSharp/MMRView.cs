using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class MMRView : MonoBehaviour
{
    Button btn_Active, btn_RoleMan, btn_Fabricate,
        btn_Warehouse, btn_Lottery,btn_Explore;
    Image ima_AV;
    
   
    private void Awake()
    {
        btn_Active = transform.Find("Btn_Active").GetComponent<Button>();
        btn_RoleMan = transform.Find("Btn_RoleMan").GetComponent<Button>();
        btn_Fabricate = transform.Find("Btn_Fabricate").GetComponent<Button>();
        btn_Warehouse = transform.Find("Btn_Warehouse").GetComponent<Button>();
        btn_Lottery = transform.parent.transform.Find("MainMenu_Left")
            .transform.Find("Btn_Lottery").GetComponent<Button>();
        ima_AV= transform.parent.transform.Find("MainMenu_Left")
            .transform.Find("Btn_AV").transform.Find("AV").GetComponent<Image>();
        btn_Explore = transform.parent.transform.Find("P_Explore")
            .transform.Find("Btn_Explore").GetComponent<Button>();

        

    }

    private void Start()
    {
        btn_Active.onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.LoadNormal);
            btnActive_Move();
            StartCoroutine("_ActiveBlack");
            
        });
        btn_RoleMan.onClick.AddListener(() =>
        {
            Prefabs.Load("UI/P_RoleMan");
            btnActive_Move();
           
        });
        btn_Fabricate.onClick.AddListener(() =>
        {
            Prefabs.Load("UI/P_Fabricate");
            btnActive_Move();
            
        });
        btn_Warehouse.onClick.AddListener(() =>
        {
            Prefabs.Load("UI/P_Warehouse");
            btnActive_Move();
            
        });
        btn_Lottery.onClick.AddListener(() => {

        });

        btn_Explore.onClick.AddListener(() =>
        {
            btn_Explore.transform.DOScale(0.8f, 0.2f);
            btn_Explore.transform.DOScale(100, 0.8f);
            btn_Explore.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), 1);
        });

    }
    IEnumerator _ActiveBlack() {
        yield return new WaitForSeconds(0.5f);
        ActiveBlack();
    }
    public void ActiveBlack() {
        GameObject go = transform.parent.Find("P_Active").gameObject;
        go.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1f), 0.5f);
    }
   

    /// <summary>
    /// Button退出界面动画
    /// </summary>
    public void btnActive_Move() {

        btn_Active.transform.DOLocalMoveX(500,0.3f);
        btn_Active.GetComponent<Image>().DOColor(new Color(1,1,1,0),0.2f);
        btn_RoleMan.gameObject.transform.DOLocalMoveX(500, 0.5f);
        btn_RoleMan.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 0.4f);
        btn_Fabricate.transform.DOLocalMoveX(500,0.7f);
        btn_Fabricate.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 0.6f);
        btn_Warehouse.transform.DOLocalMoveX(500,1);
        btn_Warehouse.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 0.8f);

        btn_Lottery.transform.DOLocalMoveX(-500,0.5f);
        btn_Lottery.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 0.8f);
        

        ima_AV.transform.DOScale(1.2f,0.2f);
        ima_AV.transform.DOScale(0,0.8f);
        ima_AV.GetComponent<Image>().DOColor (new Color(1,1,1,0),0.8f);

        

        DestroyButton();

    }
    /// <summary>
    /// 销毁Button
    /// </summary>
    public void DestroyButton()
    {
        Destroy(btn_Active.gameObject, 2);
        Destroy(btn_RoleMan.gameObject, 2);
        Destroy(btn_Fabricate.gameObject, 2);
        Destroy(btn_Warehouse.gameObject, 2);
        Destroy(btn_Lottery.gameObject,2);
        Destroy(ima_AV.gameObject,2);
    }


}
