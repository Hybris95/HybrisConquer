using System;
namespace System.Net.Sockets
{
	public class HybridWinsockClient
	{
		private static uint socketUID;
		private ServerSocket server;
		private Socket connection;
		private uint UID;
		public SocketEvent<HybridWinsockClient, object> SocketCorrupt;
		public object Wrapper;
		public IPacketCipher Crypto;
		public byte[] Buffer;
		public int RecvSize;
		public bool Connected;
		public ServerSocket Server
		{
			get
			{
				return this.server;
			}
		}
		public Socket Connection
		{
			get
			{
				return this.connection;
			}
		}
		public uint UniqueID
		{
			get
			{
				return this.UID;
			}
		}
		public HybridWinsockClient(ServerSocket _Server, Socket _Connection, int BufferSize)
		{
			this.server = _Server;
			this.connection = _Connection;
			this.UID = HybridWinsockClient.socketUID++;
			this.Buffer = new byte[BufferSize];
			this.RecvSize = 0;
			this.Wrapper = null;
		}
		public bool Send(byte[] Packet)
		{
			bool result;
			try
			{
				if (this.Crypto != null)
				{
					byte[] array = new byte[Packet.Length];
					this.Crypto.Encrypt(Packet, array, array.Length);
					this.connection.Send(array);
				}
				else
				{
					this.connection.Send(Packet);
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}
		public void Disconnect()
		{
			try
			{
				this.Connection.Disconnect(false);
			}
			catch
			{
				this.Server.InvokeDisconnect(this);
			}
		}
	}
}
