using Raylib_cs;
using System.Numerics;

static class Program
{
    public static void Main()
    {
        Game game = new Game(Configuration.config.SCREEN_WIDTH, Configuration.config.SCREEN_HEIGHT, "Engine Test");

        Wall.Init();
        MultiplayerPlayer.Init();

        GuiManager.OpenGui("MAIN_MENU");

        Raylib.HideCursor();

        while (game.Loop())
        {

        }

    }
}