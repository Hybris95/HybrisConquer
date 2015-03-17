using System;
using System.Threading;
namespace System.Net.Sockets.Encryptions
{
	public class ConquerStanderedCipher : IPacketCipher
	{
		private byte[] Key3;
		private byte[] Key4;
		private ushort InCounter;
		private ushort OutCounter;
		private bool UsingAlternate;
		public unsafe void Encrypt(byte* In, byte[] Out, int Length)
		{
			Monitor.Enter(this);
			try
			{
				for (int i = 0; i < Length; i++)
				{
                    Out[i] = (byte)(In[((IntPtr)i).ToInt32() / 1] ^ 171);
					Out[i] = (byte)((int)Out[i] << 4 | Out[i] >> 4);
                    Out[i] = (byte)(ConquerKeys.Key2[this.OutCounter >> 8] ^ Out[i]);
                    Out[i] = (byte)(ConquerKeys.Key1[(int)(this.OutCounter & 255)] ^ Out[i]);
					this.OutCounter += 1;
				}
			}
			finally
			{
				Monitor.Exit(this);
			}
		}
		public void Encrypt(byte[] In, byte[] Out, int Length)
		{
			Monitor.Enter(this);
			try
			{
				for (int i = 0; i < Length; i++)
				{
                    Out[i] = (byte)(In[i] ^ 171);
					Out[i] = (byte)((int)Out[i] << 4 | Out[i] >> 4);
                    Out[i] = (byte)(ConquerKeys.Key2[this.OutCounter >> 8] ^ Out[i]);
                    Out[i] = (byte)(ConquerKeys.Key1[(int)(this.OutCounter & 255)] ^ Out[i]);
					this.OutCounter += 1;
				}
			}
			finally
			{
				Monitor.Exit(this);
			}
		}
		public void Decrypt(byte[] In, byte[] Out, int Length)
		{
			Monitor.Enter(this);
			try
			{
				for (int i = 0; i < Length; i++)
				{
                    Out[i] = (byte)(In[i] ^ 171);
					Out[i] = (byte)((int)Out[i] << 4 | Out[i] >> 4);
					if (this.UsingAlternate)
					{
                        Out[i] = (byte)(this.Key4[this.InCounter >> 8] ^ Out[i]);
						Out[i] = (byte)(this.Key3[(int)(this.InCounter & 255)] ^ Out[i]);
					}
					else
					{
						Out[i] = (byte)(ConquerKeys.Key2[this.InCounter >> 8] ^ Out[i]);
						Out[i] = (byte)(ConquerKeys.Key1[(int)(this.InCounter & 255)] ^ Out[i]);
					}
					this.InCounter += 1;
				}
			}
			finally
			{
				Monitor.Exit(this);
			}
		}
		public unsafe void SetKeys(uint inKey1, uint inKey2)
		{
			uint num = inKey1 + inKey2 ^ 17185u ^ inKey1;
			uint num2 = num * num;
			this.Key3 = new byte[256];
			this.Key4 = new byte[256];
			fixed (void* key = ConquerKeys.Key1, key2 = this.Key3, key3 = ConquerKeys.Key2, key4 = this.Key4)
			{
				for (byte b = 0; b < 64; b += 1)
				{
					((int*)key2)[((IntPtr)b).ToInt32()] = (int)(num ^ *((uint*)key + ((IntPtr)b).ToInt32()));
                    ((int*)key4)[((IntPtr)b).ToInt32()] = (int)(num2 ^ *((uint*)key3 + ((IntPtr)b).ToInt32()));
				}
			}
			this.OutCounter = 0;
			this.UsingAlternate = true;
		}
	}
}
