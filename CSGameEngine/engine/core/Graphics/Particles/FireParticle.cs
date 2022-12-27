using System.Numerics;
using Raylib_cs;

class FireParticle : Particle
{
    public static string[] Colours = new string[] { "FF0000", "FF3600", "FF5a2e", "FFD100" };

    public decimal Speed;

    public float EndHeight = 0;

    public FireParticle() : base(getRandomFireColour(), new Random().Next(5, 8), true)
    {
        this.Speed = (decimal)(new Random().Next(3, 5)) / 10;
    }

    public static Color getRandomFireColour()
    {
        string n = Colours[new Random().Next(0, Colours.Count())];
        Color col = (ColourHelper.ColorFromHex(n));
        col.a = 255;
        return col;
    }

    public override void update(int shiftx = 0, int shifty = 0)
    {
        if (!isDead)
        {
            Vector2 pos = affectedByCamera ? Position - new Vector2(shiftx, shifty) : Position;

            Raylib.DrawTriangle(new Vector2(pos.X + Size / 2, pos.Y), new Vector2(pos.X, pos.Y + Size), new Vector2(pos.X + Size, pos.Y + Size), Colour);
        }
    }
}