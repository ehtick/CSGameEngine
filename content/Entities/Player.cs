using Raylib_cs;
using System.Numerics;

class Player : Entity
{
    public Camera2D camera = new Camera2D();

    public float speed = 0.5f;

    public Rectangle rect;

    public Texture2D playerImage;

    public Player() : base(Entities.PLAYER, "player")
    {
        this.position = new Vector2(400, 400);

        this.rect = new Rectangle(new Vector2(0, 0), 50, 50, Color.RED);
        this.rect.affectedByCamera = false;
        this.rect.Center = new Vector2(400, 400);

        this.playerImage = Textures.LoadTexture("male-character.png", this.rect.width, this.rect.height);
    }

    public override void update()
    {
        if (Raylib.IsKeyPressed(Binding.MAIN_MENU.key))
        {
            GuiManager.ToggleGui("MAIN_MENU");
        }

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

        this.camera.draw();
    }

    public void move(Vector2 amount)
    {
        camera.position += VectorMath.Normalize(amount * speed * Raylib.GetFrameTime() * 1000);
    }

    public override void draw(int shiftx = 0, int shifty = 0)
    {
        Raylib.DrawTexture(playerImage, (int)400 - (width / 2), (int)400 - (height / 2), Color.WHITE);
    }

    public override void close()
    {
        Raylib.UnloadTexture(playerImage);
    }
}