using System.Numerics;
using Raylib_cs;

public class CollisionMath
{
    public static bool CircleIntersectsRectangle(Circle circ, Rect rect)
    {
        return (bool)Raylib.CheckCollisionCircleRec(new Vector2(circ.x, circ.y), circ.r, new Raylib_cs.Rectangle(rect.x, rect.y, rect.width, rect.height));
    }

    public static bool CircleIntersectsTriangle(Vector2 center, int radius, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        return Raylib.CheckCollisionPointCircle(p1, center, radius) ||
                   Raylib.CheckCollisionPointCircle(p2, center, radius) ||
                   Raylib.CheckCollisionPointCircle(p3, center, radius) ||
                   Raylib.CheckCollisionPointTriangle(center, p1, p2, p3);
    }
}