using System.Numerics;

public class Circle
{
    public int r;
    public int x;
    public int y;

    public Circle(int x, int y, int r)
    {
        this.x = x;
        this.y = y;
        this.r = r;
    }
}

public class Rect
{
    public int x;
    public int y;
    public int width;
    public int height;

    public Vector2 topleft
    {
        get
        {
            return new Vector2(x, y);
        }

        set
        {
            x = (int)value.X;
            y = (int)value.Y;
        }
    }

    public Vector2 topright
    {
        get
        {
            return new Vector2(x + width, y);
        }

        set
        {
            x = (int)value.X - width;
            y = (int)value.Y;
        }
    }

    public Vector2 bottomleft
    {
        get
        {
            return new Vector2(x, y + height);
        }

        set
        {
            x = (int)value.X;
            y = (int)value.Y - height;
        }
    }

    public Vector2 bottomright
    {
        get
        {
            return new Vector2(x + width, y + height);
        }

        set
        {
            x = (int)value.X - width;
            y = (int)value.Y - height;
        }
    }

    public Rect(int x, int y, int w, int h)
    {
        this.x = x;
        this.y = y;
        this.width = w;
        this.height = h;
    }
}

class Triangle
{
    public Vector2 p1, p2, p3;

    public Triangle(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        this.p1 = p1;
        this.p2 = p2;
        this.p3 = p3;
    }
}