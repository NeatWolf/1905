using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    GameObject EventSystem;
    AudioSource audioSource;
    void Start()
    {
        EventSystem=GameObject.Find("EventSystem");
        audioSource=EventSystem.GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            audioSource.Play();
        }
    }
}
