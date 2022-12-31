using Raylib_cs;

static class Program
{
    public static void Main()
    {
        Game game = new Game(Configuration.config.SCREEN_WIDTH, Configuration.config.SCREEN_HEIGHT, "Engine Test");

        Wall.Init();
        MultiplayerPlayer.Init();

        GuiManager.OpenGui("MAIN_MENU");

        if (Configuration.config.debugMode)
        {
            GuiManager.CloseGui("MAIN_MENU");
            LevelManager.ActiveLevel = new MultiplayerServer("192.168.1.156", "player" + new Random().Next(1, 999), "ASSASSIN");
        }

        Raylib.HideCursor();

        while (game.Loop())
        {

        }

    }
}