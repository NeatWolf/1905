using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExploreAnimate : MonoBehaviour
{
    Slider sld;
    //场景中的相机
    Camera mainCam;
    //原主相机
    Camera oldCam;
    //场景中的区域选择按钮
    Button[] SceneBtn;
    //编队按钮，点击后弹出编队界面
    Button btnTroops;
    //编队界面,编队背景，编队标题
    GameObject Troops,BGTroops,TextTitle;
    private void Awake()
    {
        sld = transform.Find("Slider").GetComponent<Slider>();
        mainCam = GameObject.Find("Scene/Explore/Main Camera").GetComponent<Camera>();
        oldCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        
 
        // for (int i = 0; i < 4; i++)
        // {
              
        //     SceneBtn[i] =GameObject.Find("Scene/Explore").GetComponent<UISubObject>().buttons[i].GetComponent<Button>();

        // }
        btnTroops=GetComponent<UISubObject>().buttons[0];
        Troops=GetComponent<UISubObject>().go[0];
        BGTroops=GetComponent<UISubObject>().go[1];
        TextTitle=GetComponent<UISubObject>().go[2];

    }
    private void OnEnable()
    {
        
        oldCam.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(true);
        
        
    }
    private void Start() {
        btnTroops.onClick.AddListener(()=>{
            Troops.gameObject.SetActive(true);

        });
    }
    private void Update()
    {
        mainCam.transform.localPosition = new Vector3(0, 17, -(42-sld.value));

        // for (int i = 0; i < 4; i++)
        // {
        //     SceneBtn[i].transform.DORotate(Vector3.up,3,RotateMode.FastBeyond360);
        // }


    }
}
