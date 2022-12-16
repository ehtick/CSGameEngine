using Raylib_cs;
using System.Numerics;

class Player : Entity
{
    public Camera2D camera = new Camera2D();

    public float speed = 0.1f;

    public Rectangle rect;

    public Player() : base(Entities.PLAYER, "player")
    {
        this.position = new Vector2(400, 400);

        this.rect = new Rectangle(new Vector2(0, 0), 50, 50, Color.RED);
        this.rect.affectedByCamera = false;
        this.rect.Center = new Vector2(400, 400);

        Console.WriteLine(this.rect.position);
    }

    public override void update()
    {
        this.camera.draw();
    }

    public void move(Vector2 amount)
    {
        camera.position += amount * speed * Raylib.GetFrameTime() * 1000;
    }

    public override void draw(int shiftx = 0, int shifty = 0)
    {
        this.rect.draw(0, 0);

        if (Raylib.IsKeyDown(Binding.MOVE_LEFT.key))
        {
            move(Binding.MOVE_LEFT.axis);
        }

        if (Raylib.IsKeyDown(Binding.MOVE_RIGHT.key))
        {
            move(Binding.MOVE_RIGHT.axis);
        }

        if (Raylib.IsKeyDown(Binding.MOVE_UP.key))
        {
            move(Binding.MOVE_UP.axis);
        }

        if (Raylib.IsKeyDown(Binding.MOVE_DOWN.key))
        {
            move(Binding.MOVE_DOWN.axis);
        }
    }
}