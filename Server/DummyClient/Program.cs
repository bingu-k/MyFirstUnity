using ServerCore;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DummyClient
{
    class Packet
    {
        public ushort size;
        public ushort packetId;
    }

    class GameSession : Session
    {
        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected : {endPoint}");

            Packet packet = new Packet() { size = 4, packetId = 7 };
            for (int i = 0; i < 5; ++i)
            {

                ArraySegment<byte> segment = SendBufferHelper.Open(4096);
                byte[] size = BitConverter.GetBytes(packet.size);
                byte[] packetId = BitConverter.GetBytes(packet.packetId);
                int length = 0;

                Array.Copy(size, 0, segment.Array, segment.Offset + length, size.Length);
                length += size.Length;
                Array.Copy(packetId, 0, segment.Array, segment.Offset + length, packetId.Length);
                length += packetId.Length;

                ArraySegment<byte> sendBuff = SendBufferHelper.Close(packet.size);
                Send(sendBuff);
            }
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnDisconnected : {endPoint}");
        }

        public override int OnRecv(ArraySegment<byte> buffer)
        {
            string data = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
            Console.WriteLine($"[From Server] : {data}");
            return buffer.Count;
        }

        public override void OnSend(int numOfBytes)
        {
            Console.WriteLine($"Transferred bytes: {numOfBytes}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string host = Dns.GetHostName();
            IPHostEntry iphost = Dns.GetHostEntry(host);
            IPAddress ipAddr = iphost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            Connector connector = new Connector();

            connector.Connect(endPoint, () => { return new GameSession(); });
            while (true)
            {
                try
                {
                    Thread.Sleep(500);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

        }
    }
}