using Raylib_cs;
using System.Numerics;

class Camera2D
{
    public Vector2 position;

    public Nullable<Vector2> destination = Vector2.Zero;
    public float speed = 0;

    public Camera2D()
    {

    }

    public void Pan(Vector2 toLocation, float speed)
    {
        this.destination = toLocation;
        this.speed = speed;
    }

    public void draw()
    {
        if (destination != null)
        {
            Vector2 posbefore = position;

            position += ((Vector2)destination - position) * speed;

            if ((position - posbefore).Length() < 1e-2)
            {
                destination = null;
            }
        }

        if (LevelManager.ActiveLevel is Level)
        {
            Vector2 mousepos = Raylib.GetMousePosition();
            Circle lightCirc = new Circle((int)mousepos.X, (int)mousepos.Y, Configuration.config.lightSize);

            foreach (Entity entity in LevelManager.ActiveLevel.entityManager.Players.ToList().Concat(LevelManager.ActiveLevel.entityManager.Entities.ToList()))
            {
                if (!(entity is Wall || entity is Player))
                {
                    Rect enemyRect = new Rect((int)(entity.Position.X - position.X), (int)(entity.Position.Y - position.Y), entity.Width, entity.Height);
                    if (CollisionMath.CircleIntersectsRectangle(lightCirc, enemyRect))
                    {
                        entity.update((int)position.X, (int)position.Y);
                    }
                }
                else if (!(entity is Player))
                {
                    entity.update((int)position.X, (int)position.Y);
                }
            }
        }

    }
}