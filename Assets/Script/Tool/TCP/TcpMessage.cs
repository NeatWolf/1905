namespace HonorZhao
{
	/// <summary>
	/// TCP消息结构（数据包）
	/// </summary>
	public struct TcpMessage
	{
		/// <summary>
		/// 消息号（类似于URL地址，告诉对方，想执行哪些代码逻辑）
		/// </summary>
		public int Code;

		/// <summary>
		/// 消息数据
		/// </summary>
		public byte[] Data;

		/// <summary>
		/// 构造函数
		/// </summary>
		public TcpMessage(int code, byte[] data)
		{
			Code = code;
			Data = data;
		}
	}
}
