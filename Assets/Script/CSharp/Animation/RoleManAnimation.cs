using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleManAnimation : MonoBehaviour
{
    Button[] btns;
    GameObject[] canvas;
    private void Awake()
    {
        btns = GetComponent<UISubObject>().buttons;
        canvas = GetComponent<UISubObject>().go;
    }
    private void OnEnable()
    {
        
        AnimateManager.AddCams(canvas);
        AnimateManager.AddButtonsAnimate(btns);
    }

    private void Start() {
        btns[2].onClick.AddListener(()=>{
            


        });
    }
}
