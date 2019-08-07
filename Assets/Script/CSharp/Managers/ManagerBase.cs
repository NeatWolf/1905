using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class ManagerBase : MonoBehaviour
{

    public void Init()
    {

        OnInit();
    }

    protected abstract void OnInit();

}
