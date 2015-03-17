using System;
using System.Runtime.InteropServices;
namespace System.Net.Sockets
{
	internal class Native
	{
		[DllImport("msvcrt.dll")]
		public unsafe static extern void* memcpy(byte[] dst, byte[] src, int size);
	}
}
