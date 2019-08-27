using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HonorZhao;
using TMPro;

public class Chat
{
    public string message;
}
public class MainMenuNetwork : MonoBehaviour
{
    TMP_InputField inputField;
    Transform parent;
    GameObject prefab = null;

    void Start()
    {
        inputField = GetComponent<UISubObject>().go[3].GetComponent<TMP_InputField>();
        parent = GetComponent<UISubObject>().go[2].transform;
        TcpDriver.One().Host = "hxsd.tcp.honorzhao.com";
        TcpDriver.One().Port = 443;

        //设置连接成功的回调函数
        TcpDriver.One().ConnectedAction = ConnectSuccess;
        //设置断开连接的回调函数
        TcpDriver.One().DisconnectedAction = Disconnected;
        TcpDriver.One().BeginConnect();
    }

    private void OnEnable()
    {
        if (prefab == null)
        {
            foreach (AssetBundle item in AssetBundle.GetAllLoadedAssetBundles())
            {
                if (item.name == "prefab")
                    prefab = item.LoadAsset<GameObject>("Img_info");
            }
            if (prefab == null)
                prefab = AssetBundle.LoadFromFile(MyConfig.ABRootPath + "prefab").LoadAsset<GameObject>("Img_info");
        }
    }

    void ConnectSuccess()
    {
        Debug.Log("连接回调");

        //[重点]通过和服务器沟通好的消息号，注册回调函数，用来接收数据
        TcpDriver.One().ReceivedMessageActions.Add(
            (int)TCP_MESSAGE_CODE.S2C_ChatToWhole,
            ReceivedWhole
        );
    }

    void Disconnected()
    {
        Debug.Log("断开回调");
    }

    void ReceivedWhole(byte[] data)
    {
        string json = System.Text.Encoding.UTF8.GetString(data);

        Chat c = JsonUtility.FromJson<Chat>(json);

        TextMeshProUGUI text = Instantiate(prefab, parent).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.text = c.message;
    }

    public void SendChat(string str)
    {
        //发送消息
        TcpDriver.One().AddPackageToWaitSendQueue(
            (int)TCP_MESSAGE_CODE.C2S_ChatToWhole,
            System.Text.Encoding.UTF8.GetBytes("{\"message\" : \" " + str + "\"}")
        );

        inputField.text = "";
    }
}
