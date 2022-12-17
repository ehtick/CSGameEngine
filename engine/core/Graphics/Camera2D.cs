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
            foreach (Entity entity in LevelManager.ActiveLevel.entityManager.Entities)
            {
                entity.draw((int)position.X, (int)position.Y);
            }
        }

    }
}