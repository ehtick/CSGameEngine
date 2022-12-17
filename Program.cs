using Raylib_cs;
using System.Numerics;

static class Program
{
    public static void Main()
    {
        Game game = new Game(800, 800, "Engine Test");

        Level level1 = new Level();
        Level level2 = new Level();
        LevelManager.ActiveLevel = level1;

        Player player = new Player(level1);

        Entity entity = new Entity(level2, Entities.PLAYER, "enemy");
        entity.position = new Vector2(400, 400);
        entity.TransportLevel(level1);

        while (game.Loop())
        {

        }

    }
}