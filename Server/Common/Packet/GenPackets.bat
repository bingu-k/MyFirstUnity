START ../../PacketGenerator/bin/PacketGenerator.exe ../../PakcetGenerator/PDL.xml

XCOPY /y GenPackets.cs "../../DummyClient/Packet/"
XCOPY /y GenPackets.cs "../../Server/Packet/"

XCOPY /y PacketManager.cs "../../DummyClient/Packet/"
XCOPY /y PacketManager.cs "../../Server/Packet/"
