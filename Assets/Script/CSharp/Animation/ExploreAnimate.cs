using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExploreAnimate : MonoBehaviour
{
    Slider sld;
    Camera mainCam;
    Camera oldCam;
    Button[] SceneBtn;
    private void Awake()
    {
        sld = transform.Find("Slider").GetComponent<Slider>();
        mainCam = GameObject.Find("Scene/Explore/Main Camera").GetComponent<Camera>();
        oldCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        for (int i = 0; i < 4; i++)
        {
            SceneBtn[i] = mainCam.transform.parent.transform.GetComponent<Button>();
        }

    }
    private void OnEnable()
    {
        oldCam.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(true);
        
    }
    
    private void Update()
    {
        mainCam.transform.localPosition = new Vector3(0, 17, -(42-sld.value));

    }
}
