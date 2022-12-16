using Raylib_cs;
using System.Numerics;

class Entity : IDrawable
{
    public int entity_id;
    public string EntityDisplayName { get; set; }

    public Vector2 position { get; set; } = new Vector2(0, 0);

    public int width { get; set; } = 50;
    public int height { get; set; } = 50;

    public Entity(Entities entity, string display_name)
    {
        this.entity_id = Convert.ToInt32(entity);
        this.EntityDisplayName = display_name;

        EntityManager.RegisterEntity(this);
    }

    public Entity(Entities entity, string display_name, Vector2 starting_position) : this(entity, display_name)
    {
        this.position = starting_position;
    }

    public virtual void draw(int shiftx = 0, int shifty = 0)
    {
        Raylib.DrawRectangle((int)this.position.X - shiftx, (int)this.position.Y - shifty, width, height, Color.RED);
    }

    public virtual void update()
    {
        // draw();
    }
}