class EntityManager : IManager
{
    public static List<Entity> Entities = new List<Entity>();

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

    public static void RegisterEntity(Entity entity)
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
}