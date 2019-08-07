using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;


public class ButtonAniCon : MonoBehaviour, IPointerClickHandler
{
    GameObject btn_Active, btn_RoleMan, btn_Fabricate, btn_Warehouse;
    private void Awake()
    {
        btn_Active = transform.parent.transform.Find("Btn_Active").gameObject;
        btn_RoleMan = transform.parent.transform.Find("Btn_RoleMan").gameObject;
        btn_Fabricate = transform.parent.transform.Find("Btn_Fabricate").gameObject;
        btn_Warehouse = transform.parent.transform.Find("Btn_Warehouse").gameObject;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
        transform.DOLocalMoveX(10, 1);
        transform.GetComponent<Image>().DOColor(new Color(1,1,1,0),0.5f);
        btn_RoleMan.transform.DOMoveX(10, 1);
        //Sequence seq = DOTween.Sequence();
        //seq.Append(transform.DOMoveX(10, 1));
        //seq.Append(btn_RoleMan.transform.DOMoveX(10, 1));

    }
}
