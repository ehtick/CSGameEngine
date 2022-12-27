using System.Numerics;
using RSG;

class Level
{
    public EntityManager entityManager;

    public int MAPSIZE;

    public Level()
    {
        entityManager = new EntityManager();
    }

    public static List<Entity> GenerateBorder(Level level, int leftborder, int rightborder, int topborder, int bottomborder)
    {
        List<Entity> borders = new List<Entity>();

        int wallsize = 100;

        int startx = leftborder;
        int starty = topborder;

        for (int y = starty; y < bottomborder; y += wallsize)
        {

            for (int x = startx; x < rightborder; x += wallsize)
            {
                if (y == starty || y == bottomborder - wallsize || x == startx || x == rightborder - wallsize)
                {
                    borders.Add((Entity)new Wall(level, new Vector2(x, y)));
                }
            }

        }

        return borders;
    }

    public static IPromise<bool> GenerateLevelContents(Level level, int left, int right, int top, int bottom, Player player)
    {
        return LevelGeneration.GenerateLevel(level, left, right, top, bottom, player);
    }

    public virtual void update()
    {

    }
}