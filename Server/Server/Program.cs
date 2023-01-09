using ServerCore;
using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Packet
    {
        public ushort size;
        public ushort packetId;
    }

    class GameSession : PacketSession
    {
        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected : {endPoint}");

            //Packet packet = new Packet() { size = 50, packetId = 10 };

            //ArraySegment<byte> segment = SendBufferHelper.Open(4096);
            //byte[] size = BitConverter.GetBytes(packet.size);
            //byte[] packetId = BitConverter.GetBytes(packet.packetId);
            //int length = 0;

            //Array.Copy(size, 0, segment.Array, segment.Offset + length, size.Length);
            //length += size.Length;
            //Array.Copy(packetId, 0, segment.Array, segment.Offset + length, packetId.Length);
            //length += packetId.Length;

            //ArraySegment<byte> sendBuff = SendBufferHelper.Close(length);
            //Send(sendBuff);

            Thread.Sleep(5000);

            Disconnect();
        }

        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
            ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + 2);
            Console.WriteLine($"RecvPacketId : {id}\t Size : {size}");
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnDisconnected : {endPoint}");
        }

        public override void OnSend(int numOfBytes)
        {
            Console.WriteLine($"Transferred bytes: {numOfBytes}");
        }
    }

    class Program
    {
        static Listener _listener = new Listener();

        static void Main(string[] args)
        {
            string host = Dns.GetHostName();
            IPHostEntry iphost = Dns.GetHostEntry(host);
            IPAddress ipAddr = iphost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            _listener.Init(endPoint, () => { return new GameSession(); });

            Console.WriteLine("Listening...");
            while (true)
            { }
        }
    }
}