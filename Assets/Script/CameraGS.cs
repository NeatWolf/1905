using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraGS : MonoBehaviour
{
    public bool on = true;
    public float horizontalSpeed = -1f;
    public float verticalSpeed = 1f;
    Vector3 oPoint;
    Vector3 oPosition;

    private void Start()
    {
        oPoint = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        oPosition = transform.localPosition;


        // StartCoroutine(HttpResquest("www.baidu.com"));
    }

    private void FixedUpdate()
    {
        if (!on) return;
        Vector3 pos = Input.mousePosition - oPoint;
        transform.localPosition = oPosition + new Vector3(
            pos.x / Screen.width * 0.1f * horizontalSpeed,
            pos.y / Screen.height * 0.1f * verticalSpeed,
            0
        );
    }

//网络请求
    // IEnumerator HttpResquest( string url){
    //     UnityWebRequest http=UnityWebRequest.Get(url);
    //     yield return http.SendWebRequest();
    //     Debug.Log("请求成功"+http.downloadHandler.text);
        
    //     http.Dispose();
    // }
}
