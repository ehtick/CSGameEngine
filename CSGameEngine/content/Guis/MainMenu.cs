using Raylib_cs;
using System.Numerics;

class MainMenu : GuiScreen
{
    public MainMenu() : base("MAIN_MENU")
    {
        this.position = new Vector2(100, 100);
        this.width = 600;
        this.height = 600;

        Button playBtn = this.CreateButton(new Vector2(0, 0), 550, 63, Color.RED, "PLAY", 45, this.playClickHandler);
        playBtn.Center = new Vector2(400, 300);

        Button multiplayerBtn = this.CreateButton(new Vector2(0, 0), 550, 63, Color.RED, "MULTIPLAYER", 45, this.multiplayerClickHandler);
        multiplayerBtn.Center = new Vector2(400, 400);

        Button exitBtn = this.CreateButton(new Vector2(0, 0), 550, 63, Color.RED, "EXIT", 45, this.exitClickHandler);
        exitBtn.Center = new Vector2(400, 500);

        GuiManager.RegisterGui(this);
    }

    public int playClickHandler(Vector2 clickpos)
    {
        GuiManager.CloseGui(this.name);
        GuiManager.OpenGui("GAME_SELECT");
        return 0;
    }

    public int multiplayerClickHandler(Vector2 clickpos)
    {
        GuiManager.CloseGui(this.name);
        GuiManager.OpenGui("MULTIPLAYER_CONNECT");
        return 0;
    }

    public int exitClickHandler(Vector2 clickpos)
    {
        Game.Running = false;
        return 0;
    }

    public override void update(int shiftx = 0, int shifty = 0)
    {
        if (this.open)
        {
            this.DrawBackground(Raylib.ColorAlpha(Color.BLACK, 0.6f));
            base.update(shiftx, shifty);
        }
    }
}