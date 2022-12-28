using System.Numerics;
using Raylib_cs;

class OptionsArray : IUpdateable
{
    public string[] Options;
    public List<Button> OptionsButtons = new List<Button>();

    public int gapx, gapy, startx, starty, width, optionwidth, optionheight, rowcount;

    public string SelectedOption;
    public int SelectedOptionIndex = 0;

    public OptionsArray(string[] Options, int gapx, int gapy, int startx, int starty, int width, int optionwidth, int optionheight, int rowcount, int fontsize)
    {
        if (Options.Count() < 2) throw new Exception();

        this.Options = Options;

        this.SelectedOption = Options[0];

        this.gapx = gapx;
        this.gapy = gapy;
        this.startx = startx;
        this.starty = starty;
        this.width = width;
        this.optionwidth = optionwidth;
        this.optionheight = optionheight;
        this.rowcount = rowcount;

        foreach (string opt in Options)
        {
            OptionsButtons.Add(new Button(Vector2.Zero, optionwidth, optionheight, Color.RED, opt, fontsize, (Vector2 clickpos) =>
            {
                SelectedOption = opt;
                SelectedOptionIndex = Options.ToList().IndexOf(opt);
                return 0;
            }, true));
        }
    }

    public void update(int shiftx = 0, int shifty = 0)
    {
        int i = 0, x = 0, y = 0;
        foreach (string option in Options)
        {
            if (i == SelectedOptionIndex)
            {
                Raylib.DrawRectangleLines(startx + optionwidth * x + gapx * x - 2, starty + optionheight * y + gapy * y - 2, optionwidth + 4, optionheight + 4, Color.DARKGRAY);
            }

            Button btn = OptionsButtons[i];
            btn.Center = new Vector2(startx + optionwidth * x + (int)(optionwidth / 2) + gapx * x, starty + optionheight * y + (int)(optionheight / 2) + gapy * y);
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