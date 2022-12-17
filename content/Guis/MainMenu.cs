using Raylib_cs;
using System.Numerics;


class MainMenu : GuiScreen
{
    public MainMenu() : base("MAIN_MENU")
    {
        this.position = new Vector2(100, 100);
        this.width = 600;
        this.height = 600;

        GuiManager.RegisterGui(this);

        this.CreateButton(new Vector2(200, 200), 250, 63, Color.RED, "TEST", 45, this.testClickHandler);
    }

    public int testClickHandler(Vector2 clickPos)
    {
        GuiManager.CloseGui("MAIN_MENU");

        return 0;
    }

    public override void draw(int shiftx = 0, int shifty = 0)
    {

        if (this.open)
        {
            this.DrawBackground(Raylib.ColorAlpha(Color.BLACK, 0.7f));
            base.draw(shiftx, shifty);
        }
    }
}