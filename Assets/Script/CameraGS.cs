using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGS : MonoBehaviour
{
    public bool on = true;
    public float horizontalSpeed = -1f;
    public float verticalSpeed = 1f;
    Vector3 oPoint;

    private void Start()
    {
        oPoint = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
    }

    private void FixedUpdate()
    {
        if (!on) return;
        Vector3 pos = Input.mousePosition - oPoint;
        transform.localPosition = new Vector3(
            pos.x / Screen.width * 0.1f * horizontalSpeed,
            pos.y / Screen.height * 0.1f * verticalSpeed,
            0
        );
    }
}
