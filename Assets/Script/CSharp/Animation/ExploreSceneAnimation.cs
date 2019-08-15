using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ExploreSceneAnimation : MonoBehaviour
{
    Button[] btns;
    GameObject mainCam;
    GameObject[] fatherObj = new GameObject[4];
    private void Awake()
    {
        btns = GetComponent<UISubObject>().buttons;
        mainCam = GetComponent<UISubObject>().go[0];
        for (int i = 0; i < 4; i++)
        {
            fatherObj[i] = GetComponent<UISubObject>().go[i + 1];
        }
    }

    private void Update()
    {
        //按钮点击场景中的图标
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition + Vector3.forward * 50);
            RaycastHit hitInfo;
            if (Physics.Raycast(r, out hitInfo))
            {
                if (hitInfo.transform.tag == "button")
                {
                    for (int i = 0; i < 4; i++)
                    {
                        fatherObj[i].transform.GetChild(0).DOScale(1, 0.3f);
                        fatherObj[i].transform.GetChild(0).GetComponent<Image>().DOColor(new Color(1,1,1,1),0.3f);
                    }
                    hitInfo.transform.GetChild(0). DOScale(1.5f, 0.3f);
                    hitInfo.transform.GetChild(0).GetComponent<Image>().DOColor(new Color(0.013f,0.981f,0.799f,1),0.3f);
                    //hitInfo.transform.GetChild(0).DOLocalMoveY(0.5f,0.3f);
                    
                }
            }
        }


    }
}
