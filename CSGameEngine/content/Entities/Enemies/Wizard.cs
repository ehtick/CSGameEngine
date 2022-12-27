using Raylib_cs;
using System.Numerics;

class Wizard : Enemy
{
    public Wizard(Level level, Vector2 position) : base(level, Entities.WIZARD, "Wizard", StatPreset.MAGE)
    {
        this.Position = position;

        Width = 50;
        Height = 50;
    }

    public override void update(int shiftx = 0, int shifty = 0)
    {
        Vector2 abspos = Position - new Vector2(shiftx, shifty);
        Raylib.DrawRectangle((int)abspos.X, (int)abspos.Y, Width, Height, Color.DARKPURPLE);
        base.update(shiftx, shifty);
    }
}