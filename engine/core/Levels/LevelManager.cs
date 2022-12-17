class LevelManager : IManager
{
    public static List<Level> Levels = new List<Level>();
    public static Level? ActiveLevel;

    public LevelManager()
    {
        ManagerController.RegisterManager(this);
    }

    public static void RegisterLevel(Level level)
    {
        Levels.Add(level);
    }

    public void close()
    {
        return;
    }

    public bool isActive()
    {
        return true;
    }

    public void update()
    {
        if (ActiveLevel is Level)
        {
            ActiveLevel.update();
        }
    }
}