using Raylib_cs;
using System.Numerics;

class Player : Entity
{
    public Camera2D camera = new Camera2D();

    public StatManager statManager;

    public Inventory inventory;

    public Texture playerTexture;

    public float angle = 0.0f;

    public Player(Level level, string playerClass) : base(level, Entities.PLAYER, "player", false)
    {
        this.Position = new Vector2(400, 400);

        Width = 50;
        Height = 50;

        this.rect.affectedByCamera = false;
        this.rect.Center = new Vector2(400, 400);

        playerTexture = new Texture("textures/entities/player/player-texture.png", 50, 50);

        StatManager? sm = StatPreset.GetPreset(playerClass);
        if (sm is StatManager)
        {
            statManager = sm.DeepCopy();
        }
        else throw new Exception();

        inventory = new Inventory(18);
    }

    public void move(Vector2 amount)
    {
        Vector2 newPos = camera.position + VectorMath.Normalize(amount) * statManager.Speed * Time.deltaTime;
        bool can_move = true;

        foreach (Entity entity in this.currentLevel.entityManager.Entities)
        {
            if (entity.hascollision)
            {
                Vector2 abspos = entity.Position;

                bool collides = VectorMath.VectorCollision(newPos + Position, new Rect((int)abspos.X, (int)abspos.Y, entity.Width, entity.Height));

                if (collides)
                {
                    can_move = false;
                    break;
                }

            }
        }

        if (can_move)
        {
            camera.position = newPos;
        }
    }

    public override void update()
    {
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

    public override void update(int shiftx = 0, int shifty = 0)
    {

        angle += Configuration.config.playerRotationSpeed * Time.deltaTime;

        if (angle >= 360)
        {
            angle -= 360;
        }

        Rlgl.rlPushMatrix();
        Rlgl.rlTranslatef(400, 400, 0);
        Rlgl.rlRotatef(angle, 0, 0, 1);
        Rlgl.rlTranslatef(-400, -400, 0);

        if (statManager.Health > 0)
        {
            Raylib.DrawTexture(playerTexture.LoadedTexture, (int)400 - (Width / 2), (int)400 - (Height / 2), Color.GOLD);
        }
        else
        {
            Raylib.DrawTexture(playerTexture.LoadedTexture, (int)400 - (Width / 2), (int)400 - (Height / 2), Color.LIGHTGRAY);
        }

        Rlgl.rlPopMatrix();
    }

    public override void close()
    {
    }
}