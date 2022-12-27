using Raylib_cs;
using System.Numerics;

public class Rectangle : IUpdateable
{
    public bool affectedByCamera = true;
    public Color colour;
    public Vector2 position;

    private Vector2 center;
    public Vector2 Center
    {
        get
        {
            return new Vector2(position.X + (int)(width / 2), position.Y + (int)(height / 2));
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

    public virtual void update(int shiftx = 0, int shifty = 0)
    {
        Raylib.DrawRectangle((int)position.X, (int)position.Y, width, height, colour);
    }
}