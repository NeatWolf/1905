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
    public ParticleSystem[] fx;

    [CSharpCallLua]
    public void OnDestroy()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] != null)
                buttons[i].onClick.RemoveAllListeners();
        }
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i] != null)
                toggles[i].onValueChanged.RemoveAllListeners();
        }
        for (int i = 0; i < inputFields.Length; i++)
        {
            if (inputFields[i] != null)
                inputFields[i].onEndEdit.RemoveAllListeners();
        }
    }
}
