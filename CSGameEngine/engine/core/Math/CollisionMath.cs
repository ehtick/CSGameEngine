using System.Numerics;
using Raylib_cs;

public class CollisionMath
{
    public static bool intersects(Circle circ, Rect rect)
    {
        return (bool)Raylib.CheckCollisionCircleRec(new Vector2(circ.x, circ.y), circ.r, new Raylib_cs.Rectangle(rect.x, rect.y, rect.width, rect.height));
    }
}