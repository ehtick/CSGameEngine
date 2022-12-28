using Raylib_cs;

class Game
{
    public static bool Running = true;

    public Game(int SCREEN_WIDTH, int SCREEN_HEIGHT, string name)
    {
        Raylib.InitWindow(SCREEN_WIDTH, SCREEN_HEIGHT, name);
        Raylib.SetExitKey(KeyboardKey.KEY_NULL);

        Raylib.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT);

        // Init Managers
        new LevelManager();
        new GuiManager();
    }

    public bool Loop()
    {
        if (!Raylib.WindowShouldClose() && Running)
        {
            Time.UpdateEvents();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);

            ManagerController.update();

            Raylib.DrawFPS(5, 5);

            Raylib.EndDrawing();

            return true;
        }
        else
        {
            ManagerController.close();
            Raylib.CloseWindow();
            return false;
        }

    }
}