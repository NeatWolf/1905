using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "CreatManagerVarsContainer")]
public class ManagerVars : ScriptableObject
{
    public static ManagerVars GetManagerVars()
    {
        return Resources.Load<ManagerVars>("ManagerVarsContainer");
    }
    [Header("MainMenu_Panel")]
    public List<GameObject> MM_P = new List<GameObject>();
    [Header("MainMenu_Right按钮背景")]
    public List<Sprite> BtnSprite_MMR = new List<Sprite>();
    [Header("MainMenu_Left按钮背景")]
    public List<Sprite> BtnSprite_MML = new List<Sprite>();
    

}
