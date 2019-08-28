using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XLua;
using UnityEngine.UI;
using HonorZhao;


public class LoginCtrl : MonoBehaviour
{
    string UserName;
    string UserPassword;
    GameObject LoginPanel;
    LuaTable GameStartCtrl;

    public void LoginClick(LuaTable lua, string name, string password, GameObject go)
    {
        GameStartCtrl = lua;
        UserName = name;
        UserPassword = password;
        LoginPanel = go;
        Debug.Log("name: " + UserName + ", password: " + UserPassword);
        //    Prefabs.Networking(true);
           HttpDriver.One().Request(
               HTTP_REQUEST_TYPE.Post,
               "http://hxsd.ucenter.honorzhao.com/user/login",
               LoginSuccess,
               null,
               new Dictionary<string, string>
               {
                   { "Phone", UserName },
                   { "Password", UserPassword }
               }
           );
    }


    [CSharpCallLua]
    public delegate GameObject LoadWarning(LuaTable lua, Transform parent, string message, UnityAction callback = null);
    [CSharpCallLua]
    public delegate void Function(LuaTable lua);
    [CSharpCallLua]
    public delegate TextAsset LoadAss(LuaTable lua, string ABName, string fileName);

    LuaTable UICtrl = Luax.Instance.DoString("require('UICtrl.lua.txt')").Get<LuaTable>("UICtrl");
    LuaTable ABManager = Luax.Instance.DoString("require('ABManager.lua.txt')").Get<LuaTable>("ABManager");

    void LoginSuccess(string json)
    {
        LoadWarning loadWarning = UICtrl.Get<LoadWarning>("LoadWarning");
        LoadAss loadAsset = ABManager.Get<LoadAss>("LoadAsset");

       HttpUserLoginProtocol data = JsonUtility.FromJson<HttpUserLoginProtocol>(json);
       
        //    Prefabs.Networking(false);
       GameObject window = null;
       switch(data.Code)
       {
           case 1:
               //登陆令牌
               GlobalData.Token = data.Data.Token;
                
                Function loginSuccess = GameStartCtrl.Get<Function>("LoginSuccess");
                loginSuccess(GameStartCtrl);
                
               break;
           case -100001:
                window = loadWarning(UICtrl, LoginPanel.transform.GetChild(1), "LoginFail! 手机或邮箱至少填写一个");
               break;
           case -100002:
               window = loadWarning(UICtrl, LoginPanel.transform.GetChild(1), "LoginFail! 密码必须填写");
               break;
           case -100011:
               window = loadWarning(UICtrl, LoginPanel.transform.GetChild(1), "LoginFail! 登陆失败，账号不存在");
               break;
           case -100012:
               window = loadWarning(UICtrl, LoginPanel.transform.GetChild(1), "LoginFail! 登陆失败，密码错误");
               break;
           default:
               window = loadWarning(UICtrl, LoginPanel.transform.GetChild(1), "LoginFail! 登陆失败，未知错误");
               break;
       }
    }
}
