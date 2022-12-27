using System.Numerics;
using Raylib_cs;

class Minimap : IUpdateable
{
    public Rect rect;

    public Level level;
    public Player player;

    public Minimap(Level level, Player player)
    {
        this.rect = new Rect(0, 0, 150, 150);
        this.rect.topright = new Vector2(Configuration.config.SCREEN_WIDTH, 0);

        this.level = level;
        this.player = player;
    }

    public void update(int shiftx = 0, int shifty = 0)
    {
        Raylib.DrawRectangle(rect.x, rect.y, rect.width, rect.height, Raylib.ColorAlpha(Color.WHITE, 0.6f));

        int mapsize = level.MAPSIZE;

        if (mapsize == 0)
        {
            mapsize = 1600;
        }

        foreach (Entity entity in level.entityManager.Entities.ToList())
        {
            if (entity is Wall)
            {
                Vector2 abspos = entity.Position + new Vector2(mapsize / 2, mapsize / 2);

                float PERCENTX = abspos.X / mapsize;
                float PERCENTY = abspos.Y / mapsize;

                decimal PERCENTSIZE = (decimal)entity.Width / (decimal)mapsize;

                float MAPX = this.rect.x + (this.rect.width * PERCENTX);
                float MAPY = this.rect.y + (this.rect.height * PERCENTY);

                decimal MAPESIZE = this.rect.width * PERCENTSIZE;

                Raylib.DrawRectangle((int)Math.Ceiling(MAPX), (int)Math.Ceiling(MAPY), (int)Math.Max(MAPESIZE, 1), (int)Math.Max(MAPESIZE, 1), Color.BLACK);
            }
            else if (entity is Player)
            {
                float PERCENTX = (entity.Position.X + player.camera.position.X + mapsize / 2) / mapsize;
                float PERCENTY = (entity.Position.Y + player.camera.position.Y + mapsize / 2) / mapsize;

                float MAPX = this.rect.x + (this.rect.width * PERCENTX);
                float MAPY = this.rect.y + (this.rect.height * PERCENTY);

                decimal PERCENTSIZE = (decimal)entity.Width / (decimal)mapsize;
                decimal MAPESIZE = this.rect.width * PERCENTSIZE;

                Raylib.DrawCircle((int)Math.Ceiling(MAPX), (int)Math.Ceiling(MAPY), (int)Math.Ceiling(MAPESIZE / 2), Color.RED);
            }
        }
    }
}