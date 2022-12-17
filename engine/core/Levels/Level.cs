class Level
{
    public EntityManager entityManager;
    public GuiManager guiManager;

    public Level()
    {
        entityManager = new EntityManager();
        guiManager = new GuiManager();
    }

    public void update()
    {
        Console.WriteLine(entityManager.Entities);
        entityManager.update();
        guiManager.update();
    }
}