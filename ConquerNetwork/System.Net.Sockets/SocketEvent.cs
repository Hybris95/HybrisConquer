using System;
namespace System.Net.Sockets
{
	public delegate void SocketEvent<T, T2>(T Sender, T2 Arg);
}
