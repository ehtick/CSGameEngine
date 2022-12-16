using Raylib_cs;
using System.Numerics;

class Rectangle : IDrawable
{
    public bool affectedByCamera = true;
    public Color colour;
    public Vector2 position;

    private Vector2 center;
    public Vector2 Center
    {
        get
        {
            return center;
        }

        set
        {
            center = value;
            position = new Vector2((int)(value.X - width / 2), (int)(value.Y - height / 2));
        }
    }

    public int width, height;

    public Rectangle(Vector2 position, int width, int height, Color colour)
    {
        this.position = position;

        this.width = width;
        this.height = height;

        this.colour = colour;
    }

    public void draw(int shiftx = 0, int shifty = 0)
    {
        Raylib.DrawRectangle((int)position.X, (int)position.Y, width, height, colour);
    }
}