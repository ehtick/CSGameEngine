using System.Numerics;

class Wall : Object
{
    public static Texture TEX_TOP;
    public static void Init()
    {
        TEX_TOP = new Texture("Wall.png", 100, 100);
    }

    public Wall(Level level, Vector2 position) : base(level, Entities.WALL, "Wall", position, TEX_TOP, false, true)
    {
        this.Width = 100;
        this.Height = 100;
    }
}