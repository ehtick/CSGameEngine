using Raylib_cs;
using System.Net;
using System.Net.Sockets;

static class Program
{
    public static string GetLocalIPAddress()
    {
        string localIP;
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 65530);
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            localIP = endPoint.Address.ToString();
        }
        return localIP;
    }
    public static void Main()
    {
        Game game = new Game(Configuration.config.SCREEN_WIDTH, Configuration.config.SCREEN_HEIGHT, "Engine Test");

        Wall.Init();
        MultiplayerPlayer.Init();

        GuiManager.OpenGui("MAIN_MENU");

        if (Configuration.config.debugMode)
        {
            GuiManager.CloseGui("MAIN_MENU");
            LevelManager.ActiveLevel = new MultiplayerServer(GetLocalIPAddress(), "player" + new Random().Next(1, 999), "ASSASSIN");
        }

        Raylib.HideCursor();

        while (game.Loop())
        {

        }

    }
}