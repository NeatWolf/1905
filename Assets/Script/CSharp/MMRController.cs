using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMRController : MonoBehaviour
{
    Button btn_Active, btn_RoleMan, btn_Fabricate, btn_Warehouse;
    MMRView mmrView = new MMRView();
   
    private void Awake()
    {
        btn_Active = transform.Find("Btn_Active").GetComponent<Button>();
        btn_RoleMan = transform.Find("Btn_RoleMan").GetComponent<Button>();
        btn_Fabricate = transform.Find("Btn_Fabricate").GetComponent<Button>();
        btn_Warehouse = transform.Find("Btn_Warehouse").GetComponent<Button>();
        
    }

    private void Start()
    {
        btn_Active.onClick.AddListener(() =>
        {
             EventCenter.Broadcast(EventDefine.LoadNormal);
             mmrView. btnActive_Move();
        });
        btn_RoleMan.onClick.AddListener(() =>
        {
            mmrView.btnActive_Move();
            Prefabs.Load("UI/P_RoleMan");

        });
        btn_Fabricate.onClick.AddListener(() =>
        {
            mmrView.btnActive_Move();
            Prefabs.Load("UI/P_Fabricate");
        });
        btn_Warehouse.onClick.AddListener(() =>
        {
            mmrView.btnActive_Move();
            Prefabs.Load("UI/P_Warehouse");
        });

    }
}
