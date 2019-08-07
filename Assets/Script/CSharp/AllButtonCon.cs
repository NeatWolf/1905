
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public   class AllButtonCon :MonoBehaviour
{
    private  void Start()
    {
        EventCenter.AddListener(EventDefine.LoadNormal, ShowPanel);
    }
    public  void ShowPanel() {
        Prefabs.Load("UI/P_Active");

    }
  
}
