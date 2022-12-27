using System.Numerics;
using Raylib_cs;

class VectorMath
{
    public static Vector2 Normalize(Vector2 vector)
    {
        float distance = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        return new Vector2(vector.X / distance, vector.Y / distance);
    }

    public static bool VectorCollision(Vector2 vec, Rect rect)
    {
        return vec.X >= rect.x && vec.X <= rect.x + rect.width && vec.Y >= rect.y && vec.Y <= rect.y + rect.height;
    }

    public static bool VectorCollision(Vector2 vec, Circle circ)
    {
        Vector2 distance = vec - new Vector2(circ.x, circ.y);
        float length = distance.LengthSquared();
        return length <= circ.r;
    }

    public static bool IsOnScreen(Vector2 position)
    {
        return position.X >= 0 && position.X <= Configuration.config.SCREEN_WIDTH && position.Y >= 0 && position.Y <= Configuration.config.SCREEN_HEIGHT;
    }
}