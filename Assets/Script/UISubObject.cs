using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;

public class UISubObject : MonoBehaviour
{
    public Button[] buttons;
    public Toggle[] toggles;
    public Text[] texts;
    public Image[] images;
    public InputField[] inputFields;
    public GameObject[] go;



    [CSharpCallLua]
    public void OnDestroy()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if(buttons[i]!=null)
            buttons[i].onClick.RemoveAllListeners();
        }
        for (int i = 0; i < texts.Length; i++)
        {
            if (texts[i] != null)
            texts[i].onCullStateChanged.RemoveAllListeners();
        }
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i] != null)
            images[i].onCullStateChanged.RemoveAllListeners();
        }
        for (int i = 0; i < inputFields.Length; i++)
        {
            if (inputFields[i] != null)
            inputFields[i].onEndEdit.RemoveAllListeners();
        }

    }
}
