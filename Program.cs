using Raylib_cs;
using System.Numerics;

static class Program
{
    public static void Main()
    {
        Game game = new Game(800, 800, "Engine Test");

        Player player = new Player();

        Entity entity = new Entity(Entities.PLAYER, "enemy");
        entity.position = new Vector2(400, 400);

        while (game.Loop())
        {

        }

    }
}