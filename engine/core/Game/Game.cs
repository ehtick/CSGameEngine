using Raylib_cs;

class Game
{
    public Game(int SCREEN_WIDTH, int SCREEN_HEIGHT, string name)
    {
        Raylib.InitWindow(800, 800, name);
        Raylib.SetExitKey(KeyboardKey.KEY_NULL);

        // Init Managers
        new EntityManager();
        new GuiManager();
    }

    public bool Loop()
    {
        if (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            ManagerController.update();

            Raylib.EndDrawing();

            Raylib.DrawFPS(5, 5);

            Time.Time.UpdateEvents();

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