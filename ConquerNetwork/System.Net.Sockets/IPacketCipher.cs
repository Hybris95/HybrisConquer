using System;
namespace System.Net.Sockets
{
	public interface IPacketCipher
	{
		void Encrypt(byte[] In, byte[] Out, int Size);
		unsafe void Encrypt(byte* In, byte[] Out, int Size);
		void Decrypt(byte[] In, byte[] Out, int Size);
	}
}
