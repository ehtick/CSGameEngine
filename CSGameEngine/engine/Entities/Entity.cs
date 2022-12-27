using Raylib_cs;
using System.Numerics;

class Entity : IUpdateable
{
    public int entity_id;
    public string EntityDisplayName { get; set; }

    private Vector2 position;

    public Vector2 Position
    {
        get
        {
            return position;
        }
        set
        {
            position = value;
            rect.position = value;
        }
    }

    private int width, height;

    public int Width
    {
        get
        {
            return width;
        }
        set
        {
            width = value;
            rect.width = value;
        }
    }

    public int Height
    {
        get
        {
            return height;
        }
        set
        {
            height = value;
            rect.height = value;
        }
    }

    public Level currentLevel;

    public bool hascollision;

    public Rectangle rect;

    public Entity(Level level, Entities entity, string display_name, bool hascollision)
    {
        currentLevel = level;

        this.entity_id = Convert.ToInt32(entity);
        this.EntityDisplayName = display_name;

        this.hascollision = hascollision;

        this.rect = new Rectangle(Position, width, height, Color.RED);

        currentLevel.entityManager.RegisterEntity(this);
    }

    public Entity(Level level, Entities entity, string display_name, Vector2 starting_position, bool hascollision) : this(level, entity, display_name, hascollision)
    {
        this.Position = starting_position;
    }

    public void TransportLevel(Level newLevel)
    {
        currentLevel.entityManager.Entities.Remove(this);
        newLevel.entityManager.Entities.Add(this);
    }

    public virtual void update(int shiftx = 0, int shifty = 0)
    {
        Raylib.DrawRectangle((int)this.Position.X - shiftx, (int)this.Position.Y - shifty, width, height, Color.RED);
    }

    public virtual void update()
    {

    }

    public virtual void close()
    {

    }
}