using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ConquerServer_Basic
{
    public class MessagePacket : IClassPacket
    {
        public string _From;
        public string _To;
        public const uint Center = 2011;
        public uint ChatType;
        public uint Color;
        public const uint Dialog = 2101;
        public const uint Guild = 2004;
        public string Message;
        public const uint Service = 2014;
        public const uint Talk = 2000;
        public const uint Team = 2003;
        public const uint TopLeft = 2005;
        public const uint Whisper = 2001;

        public const uint Black = 0x00000000;
        public const uint White = 0x00FFFFFF;
        public const uint Red = 0x00FF0000;
        public const uint Blue = 0x0000FF00;
        public const uint Green = 0x000000FF;
        public const uint Yellow = 0x00FFFF00;
        public const uint Pink = 0x00FF00FF;
        public const uint Teal = 0x00FFFF;
        public MessagePacket(string _Message, uint _Color, uint _ChatType)
        {
            this.Message = _Message;
            this._To = "ALL";
            this._From = "SYSTEM";
            this.Color = _Color;
            this.ChatType = _ChatType;
        }
        public MessagePacket(string _Message, string __To, uint _Color, uint _ChatType)
        {
            this.Message = _Message;
            this._To = __To;
            this._From = "SYSTEM";
            this.Color = _Color;
            this.ChatType = _ChatType;
        }
        public MessagePacket(string _Message, string __To, string __From, uint _Color, uint _ChatType)
        {
            this.Message = _Message;
            this._To = __To;
            this._From = __From;
            this.Color = _Color;
            this.ChatType = _ChatType;
        }
        public MessagePacket()
        {
        }

        public void Deserialize(byte[] Bytes)
        {
            this.Color = BitConverter.ToUInt32(Bytes, 4);
            this.ChatType = BitConverter.ToUInt32(Bytes, 8);
            this._From = Encoding.ASCII.GetString(Bytes, 26, Bytes[25]);
            this._To = Encoding.ASCII.GetString(Bytes, 27 + this._From.Length, Bytes[26 + this._From.Length]);
            this.Message = Encoding.ASCII.GetString(Bytes, (29 + this._From.Length) + this._To.Length, Bytes[(28 + this._From.Length) + this._To.Length]);
        }

        public byte[] Serialize()
        {
            byte[] Packet = new byte[(((32 + this._From.Length) + this._To.Length) + this.Message.Length) + 1];
            PacketBuilder.WriteUInt16((ushort)Packet.Length, Packet, 0);
            PacketBuilder.WriteUInt16(1004, Packet, 2);
            PacketBuilder.WriteUInt32(this.Color, Packet, 4);
            PacketBuilder.WriteUInt32(this.ChatType, Packet, 8);
            Packet[24] = 4;
            PacketBuilder.WriteStringWithLength(this._From, Packet, 25);
            PacketBuilder.WriteStringWithLength(this._To, Packet, (ushort)(26 + this._From.Length));
            PacketBuilder.WriteStringWithLength(this.Message, Packet, (ushort)((28 + this._From.Length) + this._To.Length));
            return Packet;
        }
    }
}
