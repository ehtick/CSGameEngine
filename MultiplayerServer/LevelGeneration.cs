using RSG;
using System.Numerics;

class LevelGeneration
{
    public static List<List<bool>> GenerateLevel(int left, int right, int top, int bottom)
    {
        TaskCompletionSource<List<List<bool>>> tcs = new TaskCompletionSource<List<List<bool>>>();

        List<List<bool>> WALLS = new List<List<bool>>();

        int wallsize = 100;

        int rleft = (int)Math.Ceiling((double)(left + wallsize) / wallsize);
        int rright = (int)Math.Ceiling((double)(right) / wallsize);
        int rtop = (int)Math.Ceiling((double)(top + wallsize) / wallsize);
        int rbottom = (int)Math.Ceiling((double)(bottom - wallsize) / wallsize);

        List<List<bool>> cells = new List<List<bool>>();

        double total = (rright - rleft) * (rbottom - rtop);

        Random random = new Random();

        for (int y = rtop; y < rbottom + 1; y++)
        {
            List<bool> row = new List<bool>();
            for (int x = rleft; x < rright; x++)
            {
                double rand = random.NextDouble() * 100;

                row.Add(rand <= 30);
            }

            cells.Add(row);
        }

        // int _y = rtop;
        // int _x = rleft;

        // foreach (List<bool> row in cells.ToList())
        // {
        //     List<bool> _row = new List<bool>();
        //     _x = rleft;
        //     foreach (bool cell in row.ToList())
        //     {
        //         if (cell)
        //         {
        //             row.Add(true);
        //         }
        //         _x++;

        //         if (_x - rtop + 1 == row.Count)
        //         {
        //             WALLS.Add(_row);
        //         }
        //     }

        //     _y++;
        // }



        return cells;
    }
}