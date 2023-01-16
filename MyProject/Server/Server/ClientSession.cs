using System;
using ServerCore;
using System.Net;

namespace Server
{
    public class ClientSession : PacketSession
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
            PacketManager.Instance.OnRecvPacket(this, buffer);
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
}

