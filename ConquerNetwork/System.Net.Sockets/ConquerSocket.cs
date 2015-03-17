using System;
using System.Net.Sockets.Encryptions;
namespace System.Net.Sockets
{
	public class ConquerSocket : ServerSocket
	{
		protected override IPacketCipher MakeCrypto()
		{
			return new ConquerStanderedCipher();
		}
	}
}
