using Raylib_cs;
using System.Numerics;

class Entity : IDrawable
{
    public int entity_id;
    public string EntityDisplayName { get; set; }

    public Vector2 position { get; set; } = new Vector2(0, 0);

    public int width { get; set; } = 50;
    public int height { get; set; } = 50;

    public Level currentLevel;

    public Entity(Level level, Entities entity, string display_name)
    {
        currentLevel = level;

        this.entity_id = Convert.ToInt32(entity);
        this.EntityDisplayName = display_name;

        currentLevel.entityManager.RegisterEntity(this);
    }

    public Entity(Level level, Entities entity, string display_name, Vector2 starting_position) : this(level, entity, display_name)
    {
        this.position = starting_position;
    }

    public void TransportLevel(Level newLevel)
    {
        currentLevel.entityManager.Entities.Remove(this);
        newLevel.entityManager.Entities.Add(this);
    }

    public virtual void draw(int shiftx = 0, int shifty = 0)
    {
        Raylib.DrawRectangle((int)this.position.X - shiftx, (int)this.position.Y - shifty, width, height, Color.RED);
    }

    public virtual void update()
    {
        // draw();
    }

    public virtual void close()
    {

    }
}