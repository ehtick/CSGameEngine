using System.Numerics;

class VectorMath
{
    public static Vector2 Normalize(Vector2 vector)
    {
        float distance = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        return new Vector2(vector.X / distance, vector.Y / distance);
    }

    public static bool VectorCollision(Vector2 vec, Rectangle rect)
    {
        bool collides = false;

        if (vec.X >= rect.position.X && vec.X <= rect.position.X + rect.width && vec.Y >= rect.position.Y && vec.Y <= rect.position.Y + rect.height)
        {
            collides = true;
        }

        return collides;
    }
}