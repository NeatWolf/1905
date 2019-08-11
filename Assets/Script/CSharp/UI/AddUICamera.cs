using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUICamera : MonoBehaviour
{
    public static Camera UICamera = null;
    private void Awake() {
        if (UICamera == null)
        {
            UICamera = GameObject.Find("UICamera").GetComponent<Camera>();
        }
        transform.GetComponent<Canvas>().worldCamera = UICamera;
    }
}
