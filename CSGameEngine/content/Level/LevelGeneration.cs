using RSG;
using System.Numerics;

class LevelGeneration
{
    public static IPromise<bool> GenerateLevel(Level level, int left, int right, int top, int bottom, Player player)
    {
        return new Promise<bool>((resolve, reject) =>
        {
            int wallsize = 100;

            int rleft = (int)Math.Floor((double)(left + wallsize) / wallsize);
            int rright = (int)Math.Floor((double)(right) / wallsize);
            int rtop = (int)Math.Floor((double)(top + wallsize) / wallsize);
            int rbottom = (int)Math.Floor((double)(bottom - wallsize) / wallsize);

            List<List<bool>> cells = new List<List<bool>>();

            double total = (rright - rleft) * (rbottom - rtop);

            Random random = new Random();

            for (int y = rtop; y < rbottom; y++)
            {
                List<bool> row = new List<bool>();
                for (int x = rleft; x < rright; x++)
                {
                    double rand = random.NextDouble() * 100;

                    row.Add(rand <= 30);

                    LoadingScreen.progress += 1 / total / 2;
                }

                cells.Add(row);
            }

            int _y = rtop;
            int _x = rleft;

            foreach (List<bool> row in cells)
            {
                _x = rleft;
                new Promise((resolvex, rejectx) =>
                {
                    foreach (bool cell in row)
                    {
                        if (cell)
                        {
                            if (!VectorMath.VectorCollision(player.Position, new Rect(_x * wallsize, _y * wallsize, wallsize, wallsize)))
                            {
                                new Wall(level, new Vector2(_x * wallsize, _y * wallsize));
                            }
                        }
                        LoadingScreen.progress += 1 / total / 2;
                        _x++;

                        if (_x - rtop + 1 == row.Count)
                            resolvex();
                    }
                }).Then(() =>
                {
                    _y++;


                    if (_y - rleft + 1 == cells.Count)
                        resolve(true);
                });
            }
        });
    }
}