using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddMainCamera : MonoBehaviour
{
    public Camera mainCam;
    private void Awake()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    private void OnEnable()
    {
        if (gameObject.GetComponent<Canvas>().worldCamera==null)
        {
            gameObject.GetComponent<Canvas>().worldCamera = mainCam;
        }
        
    }
}
