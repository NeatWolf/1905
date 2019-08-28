using System;
using System.Collections.Generic;
using System.Net;

namespace HonorZhao
{
	/// <summary>
	/// TCP数据包处理类
	/// </summary>
	public static class TcpPackage
	{
		/// <summary>
        /// 数据打包方法
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <returns>打包好的数据包</returns>
		public static byte[] Pack(TcpMessage data)
		{
			//包头8字节，消息号4字节，消息体长度，共同构成包体大小
            //前4个字节记录整个包长度
            //中间4个字节，干扰字节
            //后4个字节，记录消息号                           //转换成网络字节序
			byte[] length = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(data.Data == null ? 12 : data.Data.Length + 12));
			//干扰字节
			byte[] zero = BitConverter.GetBytes(UnityEngine.Random.Range(0, 99999999));
			//消息号
			byte[] code = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(data.Code));

			//生成一个新包
			List<byte> message = new List<byte>();
			//追加包体长度
			message.AddRange(length);
			//追加干扰字节
			message.AddRange(zero);
			//追加消息号
			message.AddRange(code);
			//追加消息体
			if(data.Data != null) message.AddRange(data.Data);

			//转化为数组
			return message.ToArray();
		}

		/// <summary>
		/// 消息解包
		/// </summary>
		public static TcpMessage Unpack(byte[] message)
		{
			TcpMessage data;

			//获取消息号
			byte[] codeBytes = new byte[4];
			//提取消息号字节（8~11索引的值）
			Array.Copy(message, 8, codeBytes, 0, 4);
			//提取消息号,数字转换成主机字节序
			data.Code = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(codeBytes, 0));

			//计算整个包体大小
			int length = message.Length;
			//提取消息体
			//消息体有值
			if(length > 12)
			{
				//去掉头部长度
				data.Data = new byte[length - 12];
				//将数据提取出来
				Array.Copy(message, 12, data.Data, 0, length - 12);
			}
			//消息体无值
			else
			{
				data.Data = null;
			}
			
			return data;
		}
	}
}
