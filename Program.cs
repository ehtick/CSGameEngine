using Raylib_cs;
using System.Numerics;

static class Program
{
    public static void Main()
    {
        Raylib.InitWindow(800, 800, "Hello World");

        // Init Managers
        new EntityManager();

        Player player = new Player();

        Entity entity = new Entity(Entities.PLAYER, "enemy");
        entity.position = new Vector2(400, 400);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            ManagerController.update();

            Raylib.EndDrawing();

            Raylib.DrawFPS(5, 5);

            Time.Time.UpdateEvents();
        }

        Raylib.CloseWindow();
    }
}