using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 class UIManager : ManagerBase
{
    private static UIManager instance;
    private Transform uiObject;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }

    }
    //Panel对象池
    public Dictionary<string, UIPanelBase> uiPool = new Dictionary<string, UIPanelBase>();

    protected override void OnInit() {
        uiObject = GameObject.Find("UI").transform;
    }
}
