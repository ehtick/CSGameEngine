using Raylib_cs;
using System.Numerics;

class Enemy : Entity
{
    public float health = 100.0f;

    public StatManager statManager;

    public Enemy(Level level, Entities entity, string display_name, StatManager? preset) : base(level, entity, display_name, false)
    {
        if (!(preset is StatManager))
        {
            preset = StatPreset.BALANCED;
        }

        statManager = preset.DeepCopy();
    }

    public override void update(int shiftx = 0, int shifty = 0)
    {
        Vector2 abspos = rect.Center - new Vector2(shiftx, shifty);

        Vector2 text_center = abspos - new Vector2(0, Height / 2 + 15);

        int text_width = Raylib.MeasureText(EntityDisplayName, 15);

        Raylib.DrawText(EntityDisplayName, (int)text_center.X - text_width / 2, (int)text_center.Y - 15 / 2, 15, Color.BLACK);
    }
}