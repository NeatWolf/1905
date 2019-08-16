using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace HonorZhao
{
	public sealed class HttpDriver : MonoBehaviour
	{

		#region Singleton

		private HttpDriver() { }

		public static HttpDriver _Instance = null;

		public static HttpDriver One()
		{
			if(_Instance == null)
			{
				GameObject obj = new GameObject("HttpDriver");
                DontDestroyOnLoad(obj);
                _Instance = obj.AddComponent<HttpDriver>();
			}

			return _Instance;
		}

		#endregion

		private bool _Networking = false;

		/// <summary>
		/// 请求HTTP数据
		/// </summary>
		/// <param name="type">请求类型（来自服务器）</param>
		/// <param name="url">URL地址</param>
		/// <param name="successResponse">成功请求的回调函数（服务器返回的字符串）</param>
		/// <param name="errorResponse">失败的回调函数（服务器返回的状态号，错误信息，失败的URL地址，失败的数据）</param>
		/// <param name="data">数据（Key=>参数的名字，Value=>参数的值）</param>
		/// <param name="timeOut">过期秒数</param>
		/// <returns></returns>
		public bool Request(
			HTTP_REQUEST_TYPE type,
			string url,
			UnityAction<string> successResponse,
			UnityAction<int, string, string, Dictionary<string, string>> errorResponse,
			Dictionary<string, string> data = null,
			int timeOut = 10
		)
		{
			if (type == HTTP_REQUEST_TYPE.Post && (data == null || data.Count == 0))
			{
				Debug.LogError("使用HTTP的POST方式请求服务器，表单数据不能为空！");
				return false;
			}

            //保证同一时间只会有一个请求在进行
			if (_Networking)
			{
				Debug.LogError("HTTP引擎正在请求服务器！");
				return false;
			}

			StartCoroutine(
				_Request(type, url, successResponse, errorResponse, data, timeOut)
			);

			return true;
		}

		private IEnumerator _Request(
			HTTP_REQUEST_TYPE type,
			string url,
			UnityAction<string> successResponse,
			UnityAction<int, string, string, Dictionary<string, string>> errorResponse,
			Dictionary<string, string> data,
			int timeOut
		)
		{

			string debug = "URL地址：" + url + "\n";
			debug += "数据：" + HttpUtility.GetOriginalDataString(data) + "\n";
			debug += "请求类型：" + type.ToString().ToUpper() + "\n";
			DateTime debugTime = DateTime.UtcNow;

            //Unity5时代使用WWW请求HTTP，Unity2017开始，使用UnityWebRequest
            //生成请求
            UnityWebRequest engine;
			if (type == HTTP_REQUEST_TYPE.Get)
			{
                //根据传入的参数，获得HTTP元数据
				string getData = HttpUtility.GetOriginalDataString(data);
                //将URL元数据拼接在URL地址上
				url = url + (getData != "" ? "?" : "") + getData;
                //创建GET类型的请求对象
				engine = UnityWebRequest.Get(url);
			}
			else
			{
                //生成HTTP的POST表单数据
				WWWForm postData = new WWWForm();
				foreach(string keyword in data.Keys)
				{
					postData.AddField(keyword, data[keyword]);
				}
                //创建POST类型的请求对象
                engine = UnityWebRequest.Post(url, postData);
			}
            //设置HTTP请求超时为10秒
			engine.timeout = timeOut;

			_Networking = true;
            //将网络请求发送出去
			yield return engine.SendWebRequest();
			_Networking = false;

            //计算请求时间
			debug += "消耗时间：" + (DateTime.UtcNow - debugTime).TotalMilliseconds / 1000 + "秒\n";

			//网络问题
			if (engine.isNetworkError)
			{
				Debug.LogError("网络错误：" + engine.error + "\n" + debug);

                //网络错误，执行失败回调函数
				errorResponse(0, engine.error, url, data);

				engine.Dispose();
				yield break;
			}

			//服务器报错
			if (engine.isHttpError)
			{
				debug = "服务器报错（" + engine.responseCode + "）：" + engine.error + "\n" + debug;
				debug += "服务器返回值：" + engine.downloadHandler.text;
				Debug.LogError(debug);

                //服务器报错，执行失败回调函数
				errorResponse((int)engine.responseCode, engine.error, url, data);

				engine.Dispose();
				yield break;
			}

			//请求成功
			Debug.Log("请求成功：" + debug + "服务器返回值：" + engine.downloadHandler.text);

			string response = engine.downloadHandler.text;
			engine.Dispose();
            //网络请求成功后，执行成功回调函数
			successResponse(response);
		}
	}
}
