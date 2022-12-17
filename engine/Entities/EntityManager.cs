class EntityManager : IManager
{
    public bool active = true;

    public List<Entity> Entities = new List<Entity>();

    public EntityManager()
    {
        ManagerController.RegisterManager(this);
    }

    public void update()
    {
        foreach (Entity entity in Entities)
        {
            entity.update();
        }
    }

    public void RegisterEntity(Entity entity)
    {
        Entities.Add(entity);
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
}