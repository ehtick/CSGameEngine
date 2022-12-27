using Raylib_cs;
using System.Numerics;

class Particle : IUpdateable
{
    public Vector2 Position { get; set; }
    public Color Colour { get; set; }
    public float Size { get; set; }

    public bool affectedByCamera;

    public bool isDead = false;

    public Particle(Color Colour, float Size, bool affectedByCamera)
    {
        this.Position = Vector2.Zero;
        this.Colour = Colour;
        this.Size = Size;

        this.affectedByCamera = affectedByCamera;
    }

    public virtual void update(int shiftx = 0, int shifty = 0)
    {
        if (!isDead)
        {
            Vector2 pos = affectedByCamera ? Position - new Vector2(shiftx, shifty) : Position;

            Raylib.DrawRectangle((int)pos.X, (int)pos.Y, (int)Size, (int)Size, Colour);
        }
    }
}