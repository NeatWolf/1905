using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Events;

namespace HonorZhao
{
	/// <summary>
	/// TCP驱动类
	/// </summary>
	public class TcpDriver : MonoBehaviour
	{

		#region Singleton

		/// <summary>
		/// 保证不可被外部实例化
		/// </summary>
		private TcpDriver() {}

		/// <summary>
		/// 存储单例的静态成员变量
		/// </summary>
		private static TcpDriver _Instance = null;

		/// <summary>
		/// 获取单例的静态方法
		/// </summary>
		public static TcpDriver One()
		{
			if(_Instance == null)
			{
				//创建GameObject容纳TCP驱动类组件
				GameObject driver = new GameObject("TcpDriver");
				//TCP驱动类组件创建
				_Instance = driver.AddComponent<TcpDriver>();
				//防止切换场景，导致TCP驱动类丢失
				DontDestroyOnLoad(driver);
			}

			return _Instance;
		}

		#endregion

		#region Mono API

		void Start() {}

		void Update()
		{
			//在主线程处理接收到的消息
			_UpdateCallReceiveMessageActions();
			//在主线程处理连接状态变化
			_UpdateConnectStatusChange();
			//在主线程检测心跳包发送
			_UpdateCheckHeartBeat();
			//在主线程处理消息发送队列发送
			_UpdateSendWaitSendQueue();
		}

		void OnDestroy()
		{
            _Instance = null;
			BeginDisconnect();
		}

		#endregion

		#region Connection

		/// <summary>
		/// 当前驱动类的连接状态，初始值为默认状态
		/// </summary>
		public TCP_CONNECT_STATUS _ConnectStatus = TCP_CONNECT_STATUS.Default;

		/// <summary>
		/// 服务器主机地址
		/// </summary>
		public string Host = "";
		/// <summary>
		/// 服务器开放TCP连接的端口
		/// </summary>
		public int Port = 0;

		/// <summary>
		/// 连接成功后的回调函数
		/// </summary>
		public UnityAction ConnectedAction = null;
		/// <summary>
		/// 断开连接后的回调函数
		/// </summary>
		public UnityAction DisconnectedAction = null;

		/// <summary>
		/// 与客户端建立的TCP连接
		/// </summary>
		private Socket _TcpConnection = null;

		/// <summary>
		/// 异步创建一个连接
		/// </summary>
		public void BeginConnect()
		{
			//连接状态处于默认状态下，才去进行连接
			if(_ConnectStatus != TCP_CONNECT_STATUS.Default) return;

			//创建TCP的Socket资源
			_TcpConnection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			//开始异步连接服务器
			_TcpConnection.BeginConnect(Host, Port, _EndConnect, null);

			//连接状态切换为开始连接
			_ConnectStatus = TCP_CONNECT_STATUS.BeginConnect;

			Debug.Log("[TCP]开始异步连接");
		}

		/// <summary>
		/// 异步连接回调函数（分线程）
		/// </summary>
		private void _EndConnect(IAsyncResult ar)
		{
			//异步连接有Begin，就需要有End
			_TcpConnection.EndConnect(ar);

			//连接状态切换为连接完成，等待连接回调函数执行
			_ConnectStatus = TCP_CONNECT_STATUS.EndConnect;

			Debug.Log("[TCP]异步连接成功，等待执行连接回调函数");
		}

		/// <summary>
		/// 异步断开连接
		/// </summary>
		public void BeginDisconnect()
		{
			if(_TcpConnection == null) return;

			_TcpConnection.BeginDisconnect(false, _EndDisconnect, null);

			_ConnectStatus = TCP_CONNECT_STATUS.BeginDisconnect;

			Debug.Log("[TCP]客户端发起断开");
		}

		/// <summary>
		/// 异步断开连接回调函数（分线程）
		/// </summary>
		private void _EndDisconnect(IAsyncResult ar)
		{
			//异步断开连接有Begin，就需要有End
			_TcpConnection.EndDisconnect(ar);
			//关闭Socket并释放所有资源
			_TcpConnection.Close();
			//Socket资源置空
			_TcpConnection = null;

			_ConnectStatus = TCP_CONNECT_STATUS.EndDisconnect;
			
			Debug.Log("[TCP]客户端发起断开成功");
		}

		/// <summary>
		/// 在主线程根据连接状态处理响应逻辑
		/// </summary>
		private void _UpdateConnectStatusChange()
		{
			switch(_ConnectStatus)
			{
				//这些状态什么也不做
				case TCP_CONNECT_STATUS.Default:
				case TCP_CONNECT_STATUS.BeginConnect:
				case TCP_CONNECT_STATUS.Online:
				case TCP_CONNECT_STATUS.BeginDisconnect:
					break;

				//异步连接成功
				case TCP_CONNECT_STATUS.EndConnect:
					//开始接收服务器数据
					_BeginReceive();

					//调用外部连接成功回调函数
					if(ConnectedAction != null) ConnectedAction();

					//连接成功回调函数执行后，切换为在线状态
					_ConnectStatus = TCP_CONNECT_STATUS.Online;
					break;

				//网络断开后
				case TCP_CONNECT_STATUS.EndDisconnect:
					//断开连接回调函数（放前面是因为有可能在断开回调函数中再次连接）
					_ConnectStatus = TCP_CONNECT_STATUS.Default;

					//调用外部断开连接回调函数
					if(DisconnectedAction != null) DisconnectedAction();
					break;
			}
		}

		#endregion

		#region Receive

		/// <summary>
		/// 上层注册过来的回调函数
		/// TCP驱动类收到消息后，会根据消息ID，调用回调函数，并将数据传递给回调函数
		/// </summary>
		public Dictionary<int, UnityAction<byte[]>> ReceivedMessageActions = new Dictionary<int, UnityAction<byte[]>>();
		
		/// <summary>
		/// 从系统驱动层接收到的byte数据缓冲区
		/// </summary>
		private byte[] _ReceiveBuffer = new byte[1024 * 1024];

		//private List<byte> _TempBytes = new List<byte>();

		/// <summary>
		/// 收到的消息队列（主线程，分线程共享，注意加互斥锁）
		/// </summary>
		private Queue<TcpMessage> _ReceivedMessageQueue = new Queue<TcpMessage>();

		/// <summary>
		/// 开始异步接收数据
		/// </summary>
		private void _BeginReceive()
		{
			_TcpConnection.BeginReceive(
				_ReceiveBuffer,			//接收数据缓冲区
				0,						//接收缓冲区起始下标
				_ReceiveBuffer.Length,	//接收缓冲区长度
				SocketFlags.None,		//接收类型
				_EndReceive,			//接收回调函数
				null					//给接收结束回调函数的传参
			);
		}

		/// <summary>
		/// 异步接收数据回调函数（分线程）
		/// </summary>
		private void _EndReceive(IAsyncResult ar)
		{
            //异步接收有Begin，就需要有End
            //totalLength是从网卡接收过来的数据的总长度
            int totalLength = _TcpConnection.EndReceive(ar);

			//服务器发起断开连接，会接收到0字节的数据
			if(totalLength == 0)
			{
				Debug.Log("[TCP]被服务器断开，接收到0字节，关闭连接");

				//断开连接
				_TcpConnection.Disconnect(false);
				//释放资源
				_TcpConnection.Close();
				//Socket资源置空
				_TcpConnection = null;

				//状态变更为断开成功，
				_ConnectStatus = TCP_CONNECT_STATUS.EndDisconnect;
			}
			//正常接收
			else
			{
				Debug.Log("[TCP]成功接收" + totalLength + "字节");

				try
				{
					//分包
					//当前读取的起始下标
					int startIndex = 0;

					//还没有读取完接收到的所有数据
					while(startIndex < totalLength)
					{
						//获得单个包的大小
						byte[] plBytes = new byte[4];
						Array.Copy(_ReceiveBuffer, startIndex, plBytes, 0, 4);
						int packageLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(plBytes, 0));

						//创建单个包的字节数组
						byte[] package = new byte[packageLength];
						//从缓冲区数组中读
						Array.Copy(_ReceiveBuffer, startIndex, package, 0, packageLength);

						//向消息接收队列中追加消息
						//主线程，分线程因为共享队列，所以需要加锁
						lock(_ReceivedMessageQueue)
						{
							_ReceivedMessageQueue.Enqueue(TcpPackage.Unpack(package));
						}
					
						//假设0下标，读了6个，下标变成6开始读
						startIndex += packageLength;
					}
				}
				catch(Exception e)
				{
					Debug.Log(e.ToString());
				}

				//开启一个新的接收线程
				_BeginReceive();
			}
		}

		private void _UpdateCallReceiveMessageActions()
		{
			//获取接收队列中的数据
			lock(_ReceivedMessageQueue)
			{
                //获取队列长度
				int length = _ReceivedMessageQueue.Count;

				for(int i = 0; i < length; i++)
				{
                    //从队列的头部，取出一条消息
					TcpMessage message = _ReceivedMessageQueue.Dequeue();

					//如果接收的是心跳消息
					if(message.Code == (int)TCP_MESSAGE_CODE.S2C_HeartBeat)
					{
						ReceivedHeartBeat();
					}
					else
					{
						//如果接收的是注册在回调函数池中的回调函数
						if(ReceivedMessageActions.ContainsKey(message.Code) && ReceivedMessageActions[message.Code] != null)
						{
							ReceivedMessageActions[message.Code](message.Data);
						}
					}
				}
			}
		}

		#endregion

		#region Send

		/// <summary>
		/// 是否有数据正在发送中
		/// </summary>
		private bool _Sending = false;

		/// <summary>
		/// 等待发送的数据包队列
		/// </summary>
		public Queue<byte[]> _WaitSendQueue = new Queue<byte[]>();

		/// <summary>
		/// 将数据添加到待发送数据包队列中
		/// </summary>
		public void AddPackageToWaitSendQueue(int code, byte[] data)
		{
			//生成一个数据包，并添加到等待发送队列中
			_WaitSendQueue.Enqueue(
				TcpPackage.Pack(
					new TcpMessage(code, data)
				)
			);
		}

		/// <summary>
		/// 异步发送等待数据队列中的数据
		/// </summary>
		private void _BeginSend()
		{
			//将待发送队列中的数据进行拼接（粘包）
			List<byte> data = new List<byte>();
			int count = _WaitSendQueue.Count;
			for(int i = 0; i < count; i++)
			{
				data.AddRange(_WaitSendQueue.Dequeue());
			}
			byte[] bytes = data.ToArray();

			//异步发送数据
			_TcpConnection.BeginSend(
				bytes,
				0,
				bytes.Length,
				SocketFlags.None,
				_EndSend,
				null
			);

			//发送状态开启，防止在发送过程中，再开启异步发送
			_Sending = true;
		}

		/// <summary>
		/// 异步发送回调函数（分线程）
		/// </summary>
		private void _EndSend(IAsyncResult ar)
		{
			//异步发送有Begin，就需要有End
			int length = _TcpConnection.EndSend(ar);

			//发送完成，就关闭正在发送这个状态，可以让后序队列中的数据继续发送
			_Sending = false;

			Debug.Log("[TCP]成功发送" + length + "字节");
		}

		/// <summary>
		/// 发送等待发送队列中的数据
		/// </summary>
		private void _UpdateSendWaitSendQueue()
		{
			//SOCKET连接资源可用
			//连接状态为在线
			//没有处于数据发送状态下
			//等待发送队列内部含有数据
			if(_TcpConnection != null && _ConnectStatus == TCP_CONNECT_STATUS.Online && !_Sending && _WaitSendQueue.Count > 0)
			{
				_BeginSend();
			}
		}

		#endregion

		#region HeartBeat

		/// <summary>
		/// 发送心跳间隔秒数
		/// </summary>
		private readonly float _SendHeartBeatIntervalTime = 5f;

		/// <summary>
		/// 发送心跳已经等待时间
		/// </summary>
		private float _SendHeartBeatWaitTime = 0f;

		/// <summary>
		/// 检查心跳
		/// </summary>
		private void _UpdateCheckHeartBeat()
		{
			//如果处于连接状态，才进行心跳检测
			if(_TcpConnection != null && _ConnectStatus == TCP_CONNECT_STATUS.Online)
			{
				//每隔固定秒数，就需要发送心跳
				if (_SendHeartBeatWaitTime >= _SendHeartBeatIntervalTime)
				{
					//将心跳包添加到待发送队列中
					AddPackageToWaitSendQueue((int)TCP_MESSAGE_CODE.C2S_HeartBeat, null);
					//每次发送后，就要把等待时间重置
					_SendHeartBeatWaitTime = 0f;

					Debug.Log("[TCP]发送心跳包");
				}
				//没到秒数
				else
				{
					//使用帧间距时间累加距离上次发送心跳时间
					_SendHeartBeatWaitTime += Time.deltaTime;
				}
			}
		}

		/// <summary>
		/// 收到服务器发送的心跳
		/// </summary>
		private void ReceivedHeartBeat()
		{
			Debug.Log("[TCP]接收心跳包");
		}

		#endregion

	}
}

