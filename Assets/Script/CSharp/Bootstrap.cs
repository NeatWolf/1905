using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void AddManagers() {
        gameObject.AddComponent<UIManager>();
        gameObject.AddComponent<EventManager>();
        gameObject.AddComponent<ResourcesManager>();
    }
    void InitAllManager() {
        
    }
}
