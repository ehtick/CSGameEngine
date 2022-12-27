using Raylib_cs;
using System.Numerics;

class Player : Entity
{
    public Camera2D camera = new Camera2D();

    public Animation playerAnimation;

    public StatManager statManager;

    public Player(Level level, string playerClass) : base(level, Entities.PLAYER, "player", false)
    {
        this.Position = new Vector2(400, 400);

        Width = 50;
        Height = 50;

        this.rect.affectedByCamera = false;
        this.rect.Center = new Vector2(400, 400);

        playerAnimation = new Animation("player-animation/", Width, Height, 6);

        StatManager? sm = StatPreset.GetPreset(playerClass);
        if (sm is StatManager)
        {
            statManager = sm.DeepCopy();
        }
        else throw new Exception();
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
        Raylib.DrawTexture(playerAnimation.frame.LoadedTexture, (int)400 - (Width / 2), (int)400 - (Height / 2), Color.WHITE);

        playerAnimation.update();
    }

    public override void close()
    {
    }
}