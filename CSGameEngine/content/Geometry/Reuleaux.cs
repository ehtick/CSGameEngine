using System.Numerics;
using Raylib_cs;

class Raeuleaux : IUpdateable
{
    public Circle circ;
    public Triangle tri;

    public Vector2 top;
    public int baseLength;

    public Vector2 targetPoint;

    public int frame = 0;

    public Raeuleaux(Vector2 top, int radius, int baseLength, Vector2 targetPoint)
    {
        this.baseLength = baseLength;

        this.top = top;
        this.targetPoint = targetPoint;
        this.circ = new Circle((int)top.X, (int)top.Y, radius);

        (Vector2 p1, Vector2 p2) = ShapeMath.CalculateTrianglePoints(circ.x, circ.y, circ.r, baseLength, targetPoint);
        this.tri = new Triangle(top, p1, p2);
    }

    public void updateTriangle()
    {
        (Vector2 p1, Vector2 p2) = ShapeMath.CalculateTrianglePoints(circ.x, circ.y, circ.r, baseLength, targetPoint);
        this.tri = new Triangle(top, p1, p2);
    }

    public void update(int shiftx = 0, int shifty = 0)
    {
        updateTriangle();

        Raylib.DrawTriangle(tri.p1, tri.p2, tri.p3, Raylib.ColorAlpha(Color.RED, 0.1f));
    }
}