using System;
namespace System.Net.Sockets
{
	public abstract class ServerSocket
	{
		private Socket Connection;
		private ushort port;
		private int backlog;
		private bool enabled;
		private int clientbuffersize;
		public SocketEvent<HybridWinsockClient, object> OnClientConnect;
		public SocketEvent<HybridWinsockClient, object> OnClientDisconnect;
		public SocketEvent<HybridWinsockClient, byte[]> OnClientReceive;
		public SocketEvent<HybridWinsockClient, SocketError> OnClientError;
		public ushort Port
		{
			get
			{
				return this.port;
			}
			set
			{
				this.enabledCheck("Port");
				this.port = value;
			}
		}
		public int Backlog
		{
			get
			{
				return this.backlog;
			}
			set
			{
				this.enabledCheck("Backlog");
				this.backlog = value;
			}
		}
		public int ClientBufferSize
		{
			get
			{
				return this.clientbuffersize;
			}
			set
			{
				this.enabledCheck("ClientBufferSize");
				this.clientbuffersize = value;
			}
		}
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
		}
		private void enabledCheck(string Variable)
		{
			if (this.enabled)
			{
				throw new Exception("Cannot modify " + Variable + " while socket is enabled.");
			}
		}
		protected abstract IPacketCipher MakeCrypto();
		public ServerSocket()
		{
			this.Connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			this.clientbuffersize = 65535;
		}
		public void Enable()
		{
			if (!this.enabled)
			{
				this.Connection.Bind(new IPEndPoint(IPAddress.Any, (int)this.port));
				this.Connection.Listen(this.backlog);
				this.Connection.BeginAccept(new AsyncCallback(this.AsyncConnect), null);
				this.enabled = true;
			}
		}
		public void Disable()
		{
			if (this.enabled)
			{
				this.Connection.Close();
				this.enabled = false;
			}
		}
		private void AsyncConnect(IAsyncResult res)
		{
			byte b = 0;
			try
			{
				HybridWinsockClient hybridWinsockClient = new HybridWinsockClient(this, this.Connection.EndAccept(res), this.clientbuffersize);
				hybridWinsockClient.Crypto = this.MakeCrypto();
				hybridWinsockClient.Connected = true;
				b += 1;
				if (this.OnClientConnect != null)
				{
					this.OnClientConnect(hybridWinsockClient, null);
				}
				this.Connection.BeginAccept(new AsyncCallback(this.AsyncConnect), null);
				hybridWinsockClient.Connection.BeginReceive(hybridWinsockClient.Buffer, 0, hybridWinsockClient.Buffer.Length, SocketFlags.None, new AsyncCallback(this.AsyncReceive), hybridWinsockClient);
				b += 1;
			}
			catch (SocketException)
			{
				if (this.enabled)
				{
					this.Connection.BeginAccept(new AsyncCallback(this.AsyncConnect), null);
				}
			}
			catch (ObjectDisposedException)
			{
			}
		}
		private unsafe void AsyncReceive(IAsyncResult res)
		{
			try
			{
				HybridWinsockClient hybridWinsockClient = (HybridWinsockClient)res.AsyncState;
				SocketError socketError;
				hybridWinsockClient.RecvSize = hybridWinsockClient.Connection.EndReceive(res, out socketError);
				if (socketError == SocketError.Success && hybridWinsockClient.RecvSize > 0 && hybridWinsockClient.Connection.Connected)
				{
					byte[] array = new byte[hybridWinsockClient.RecvSize];
					if (hybridWinsockClient.Crypto != null)
					{
						hybridWinsockClient.Crypto.Decrypt(hybridWinsockClient.Buffer, array, array.Length);
					}
					else
					{
						Native.memcpy(array, hybridWinsockClient.Buffer, hybridWinsockClient.RecvSize);
					}
					if (this.OnClientReceive != null)
					{
						this.OnClientReceive(hybridWinsockClient, array);
					}
					hybridWinsockClient.Connection.BeginReceive(hybridWinsockClient.Buffer, 0, hybridWinsockClient.Buffer.Length, SocketFlags.None, new AsyncCallback(this.AsyncReceive), hybridWinsockClient);
				}
				else
				{
					hybridWinsockClient.Connected = false;
					if (socketError != SocketError.Success && this.OnClientError != null)
					{
						this.OnClientError(hybridWinsockClient, socketError);
					}
					this.InvokeDisconnect(hybridWinsockClient);
				}
			}
			catch (SocketException)
			{
			}
			catch (ObjectDisposedException)
			{
			}
		}
		public void InvokeDisconnect(HybridWinsockClient Client)
		{
			if (Client != null)
			{
				try
				{
					if (Client.Connected)
					{
						Client.Connection.Shutdown(SocketShutdown.Both);
						Client.Connection.Close();
					}
					else
					{
						if (this.OnClientDisconnect != null)
						{
							this.OnClientDisconnect(Client, null);
						}
						Client.Wrapper = null;
						Client = null;
					}
				}
				catch (ObjectDisposedException)
				{
				}
			}
		}
	}
}
