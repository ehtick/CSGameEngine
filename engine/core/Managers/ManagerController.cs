static class ManagerController
{
    static List<IManager> Managers = new List<IManager>();

    public static void RegisterManager(IManager manager)
    {
        Managers.Add(manager);
    }

    public static void update()
    {
        foreach (IManager manager in Managers)
        {
            manager.update();
        }
    }

    public static void close()
    {
        foreach (IManager manager in Managers)
        {
            manager.close();
        }
    }
}