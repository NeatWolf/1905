namespace HonorZhao
{
	/// <summary>
	/// TCP连接状态
	/// </summary>
	public enum TCP_CONNECT_STATUS
	{
		/// <summary>
		/// 未连接（默认）
		/// </summary>
		Default,

		/// <summary>
		/// 开始异步连接
		/// </summary>
		BeginConnect,

		/// <summary>
		/// 异步连接成功（应执行连接成功的回调函数，内含Unity的场景操作）
		/// </summary>
		EndConnect,

		/// <summary>
		/// 通信中
		/// </summary>
		Online,

		/// <summary>
		/// 开始异步断开连接
		/// </summary>
		BeginDisconnect,

        /// <summary>
        /// 异步断开连接成功（应执行已断开的回调函数，内含Unity的场景操作）
        /// </summary>
        EndDisconnect,
	}
}

