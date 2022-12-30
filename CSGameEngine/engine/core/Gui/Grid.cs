using System.Numerics;
using Raylib_cs;

class Grid : IUpdateable
{
    public string[] Items;
    public List<Label> ItemsLabels = new List<Label>();

    public int gapx, gapy, startx, starty, width, itemwidth, itemheight, rowcount;

    public Grid(string[] Items, int gapx, int gapy, int startx, int starty, int width, int optionwidth, int optionheight, int rowcount, int fontsize)
    {
        this.Items = Items;

        this.gapx = gapx;
        this.gapy = gapy;
        this.startx = startx;
        this.starty = starty;
        this.width = width;
        this.itemwidth = optionwidth;
        this.itemheight = optionheight;
        this.rowcount = rowcount;

        foreach (string opt in Items)
        {
            ItemsLabels.Add(new Label(Vector2.Zero, optionwidth, optionheight, Color.RED, opt, fontsize, true));
        }
    }

    public void update(int shiftx = 0, int shifty = 0)
    {
        int i = 0, x = 0, y = 0;
        foreach (string option in Items)
        {
            Label btn = ItemsLabels[i];
            btn.Center = new Vector2(startx + itemwidth * x + (int)(itemwidth / 2) + gapx * x, starty + itemheight * y + (int)(itemheight / 2) + gapy * y);
            btn.update();

            if (x + 1 == rowcount)
            {
                y++;
                x = 0;
            }
            else
            {
                x++;
            }

            i++;
        }
    }
}