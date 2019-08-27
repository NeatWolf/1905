namespace HonorZhao
{
	/// <summary>
	/// TCP通信消息号
	/// </summary>
	public enum TCP_MESSAGE_CODE
	{
		/// <summary>
		/// 客户端发送心跳
		/// </summary>
		C2S_HeartBeat = 666,
		/// <summary>
		/// 服务器发送心跳
		/// </summary>
		S2C_HeartBeat = 667,

		/// <summary>
		/// 客户端发送全服消息
		/// </summary>
		C2S_ChatToWhole = 11000,
		/// <summary>
		/// 客户端接收全服消息
		/// </summary>
		S2C_ChatToWhole = 11001,
	}
}
