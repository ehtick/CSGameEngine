using System.Numerics;

using Raylib_cs;

class MultiplayerConnect : GuiScreen
{
    public Input ipInput;
    public Input usernameInput;

    public string[] Classes = new string[] { "ASSASSIN", "MAGE", "WARRIOR" };
    public int currentClass = 0;

    public Button classBtn;

    public MultiplayerConnect() : base("MULTIPLAYER_CONNECT")
    {

        Button backBtn = this.CreateButton(new Vector2(0, 0), 550, 63, Color.RED, "BACK", 45, this.backClickHandler);
        backBtn.Center = new Vector2(400, 250);

        usernameInput = this.CreateInput(new Vector2(0, 0), 550, 63, Color.RED, "Username", 45, true);
        usernameInput.Center = new Vector2(400, 350);

        ipInput = this.CreateInput(new Vector2(0, 0), 550, 63, Color.RED, "Server IP", 45, true);
        ipInput.Center = new Vector2(400, 450);

        classBtn = this.CreateButton(new Vector2(0, 0), 550, 63, Color.RED, "CLASS: " + Classes[currentClass], 40, this.classClickHandler);
        classBtn.Center = new Vector2(400, 550);

        Button joinServerBtn = this.CreateButton(new Vector2(0, 0), 550, 63, Color.RED, "Join Server", 45, this.joinServerClick, true);
        joinServerBtn.Center = new Vector2(400, 650);

        GuiManager.RegisterGui(this);
    }

    public int classClickHandler(Vector2 clickpos)
    {
        currentClass++;

        if (currentClass == Classes.Length)
        {
            currentClass = 0;
        }

        classBtn.text = "CLASS: " + Classes[currentClass];

        return 0;
    }


    public int joinServerClick(Vector2 clickpos)
    {
        if (usernameInput.textwritten != "")
        {
            LevelManager.ActiveLevel = new MultiplayerServer(ipInput.textwritten, usernameInput.textwritten, Classes[currentClass]);
            GuiManager.CloseGui(this.name);
        }

        return 0;
    }

    public int backClickHandler(Vector2 clickpos)
    {
        GuiManager.CloseGui(this.name);
        GuiManager.OpenGui("MAIN_MENU");
        return 0;
    }

    public override void update(int shiftx = 0, int shifty = 0)
    {
        this.DrawBackground(Raylib.ColorAlpha(Color.BLACK, 0.6f));
        base.update(shiftx, shifty);
    }
}