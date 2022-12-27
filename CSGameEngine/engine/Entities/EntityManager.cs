using System.Numerics;
using Raylib_cs;

class EntityManager
{
    public bool active = true;

    public List<Entity> Entities = new List<Entity>();
    public List<MultiplayerPlayer> Players = new List<MultiplayerPlayer>();

    public EntityManager()
    {
    }

    public void update(Camera2D camera)
    {
        foreach (Entity entity in Entities.ToList())
        {
            entity.update();
        }
    }

    public void RegisterEntity(Entity entity)
    {
        Entities.Add(entity);
    }

    public void RegisterEntities(List<Entity> entities)
    {
        foreach (Entity entity in entities)
        {
            RegisterEntity(entity);
        }
    }

    public void close()
    {
        foreach (Entity entity in Entities)
        {
            entity.close();
        }
    }

    public bool isActive()
    {
        return active;
    }

    public MultiplayerPlayer? GetPlayerByUsername(string username)
    {
        MultiplayerPlayer? player = null;

        foreach (MultiplayerPlayer p in Players)
        {
            if (p.username == username)
            {
                player = p;
                break;
            }
        }

        return player;
    }

    public bool PlayerRegistered(string username)
    {
        bool registered = false;

        foreach (MultiplayerPlayer p in Players)
        {
            if (p.username == username)
            {
                registered = true;
                break;
            }
        }

        return registered;
    }
}