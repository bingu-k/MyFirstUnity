using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketHandler
{
    public static void S_ChatHandler(PacketSession session, IMessage packet)
    {
        S_Chat chatPacket = packet as S_Chat;
        ServerSession = serverSession = session as ServerSession;

        Debug.Log(chatPacket.Context);
    }

    public static void S_EnterGameHandler(PacketSession session, IMessage packet)
    {
        S_EnterGame enterGamePakcet = packet as S_EnterGame;
        ServerSession serverSession = session as ServerSession;
    }
}