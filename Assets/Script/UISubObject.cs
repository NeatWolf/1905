using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;
using TMPro;

public class UISubObject : MonoBehaviour
{
    public Button[] buttons;
    public Toggle[] toggles;
    public Text[] texts;
    public TextMeshProUGUI[] tmps;
    public Image[] images;
    public InputField[] inputFields;
    public GameObject[] go;

    [CSharpCallLua]
    public void OnDestroy()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.RemoveAllListeners();
        }
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].onCullStateChanged.RemoveAllListeners();
        }
        for (int i = 0; i < images.Length; i++)
        {
            images[i].onCullStateChanged.RemoveAllListeners();
        }
        for (int i = 0; i < inputFields.Length; i++)
        {
            inputFields[i].onEndEdit.RemoveAllListeners();
        }

    }
}
