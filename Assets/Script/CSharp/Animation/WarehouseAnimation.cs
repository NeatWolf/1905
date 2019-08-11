using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarehouseAnimation : MonoBehaviour
{
    GameObject[] canvas;
    Button[] btns;
    private void Awake()
    {
        canvas = GetComponent<UISubObject>().go;
        canvas[0].GetComponent<Canvas>().worldCamera =
                GameObject.Find("UICamera").GetComponent<Camera>();
        canvas[1].GetComponent<Canvas>().worldCamera =
                GameObject.Find("UICamera").GetComponent<Camera>();

        for (int i = 0; i < canvas[2].transform.childCount; i++)
        {

            if (canvas[2].transform.GetChild(i).GetComponent<BtnAnimate>() != null)
            {
                return;
            }

            canvas[2].transform.GetChild(i).gameObject.AddComponent<BtnAnimate>();
        }

    }
    private void OnEnable()
    {
        
    }
    public void ListAnimate() {

    }
}
