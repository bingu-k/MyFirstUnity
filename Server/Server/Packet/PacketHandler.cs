using System;
using ServerCore;

public class PacketHandler
{
    public static void PlayerInfoReqHandler(PacketSession session, IPacket packet)
    {
        PlayerInfoReq playerInfoReq = packet as PlayerInfoReq;

        Console.WriteLine($"PlayerInfoReq : {playerInfoReq.name}");
        foreach (PlayerInfoReq.Skill skill in playerInfoReq.skills)
            Console.WriteLine($"Skill Status\nID : {skill.id}\tLevel : {skill.level}\tDuration : {skill.duration}");
    }

    public static void TestHandler(PacketSession session, IPacket packet)
    { }
}