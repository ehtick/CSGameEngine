using Raylib_cs;
using System.Numerics;

class Object : Entity
{
    public Texture texture;

    public bool pickupable;
    public Object(Level level, Entities entity, string display_name, Vector2 position, Texture texture, bool pickupable, bool hascollision) : base(level, entity, display_name, hascollision)
    {
        this.Position = position;
        this.texture = texture;
        this.pickupable = pickupable;
    }

    public override void update(int shiftx = 0, int shifty = 0)
    {
        Raylib.DrawTexture(texture.LoadedTexture, (int)Position.X - shiftx, (int)Position.Y - shifty, Color.WHITE);
    }
}