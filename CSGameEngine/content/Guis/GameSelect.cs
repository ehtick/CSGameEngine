using System.Numerics;
using Raylib_cs;

class GameSelect : GuiScreen
{
    public string[] GameTypes = new string[] { "ENDLESS", "TEST" };
    public int currentGameType = 0;

    public string[] Classes = new string[] { "ASSASSIN", "MAGE", "WARRIOR" };
    public int currentClass = 0;

    public Button gameTypeBtn;
    public Button classBtn;

    public GameSelect() : base("GAME_SELECT")
    {
        this.position = new Vector2(100, 100);
        this.width = 600;
        this.height = 600;

        Button backBtn = this.CreateButton(new Vector2(0, 0), 550, 63, Color.RED, "BACK", 45, this.backClickHandler);
        backBtn.Center = new Vector2(400, 250);

        gameTypeBtn = this.CreateButton(new Vector2(0, 0), 550, 63, Color.RED, "GAME TYPE: " + GameTypes[currentGameType], 40, this.gameTypeClickHandler);
        gameTypeBtn.Center = new Vector2(400, 350);

        classBtn = this.CreateButton(new Vector2(0, 0), 550, 63, Color.RED, "CLASS: " + Classes[currentClass], 40, this.classClickHandler);
        classBtn.Center = new Vector2(400, 450);

        Button createWorldBtn = this.CreateButton(new Vector2(0, 0), 550, 63, Color.RED, "CREATE WORLD", 40, this.createWorldClickHandler);
        createWorldBtn.Center = new Vector2(400, 550);

        GuiManager.RegisterGui(this);
    }

    public int createWorldClickHandler(Vector2 clickpos)
    {
        Level? level = null;

        if (GameTypes[currentGameType] == "ENDLESS")
        {
            level = new Endless(Classes[currentClass]);
        }

        if (level is Level)
        {
            LevelManager.ActiveLevel = level;
            GuiManager.CloseGui(this.name);
        }

        return 0;
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

    public int gameTypeClickHandler(Vector2 clickpos)
    {
        currentGameType++;

        if (currentGameType == GameTypes.Length)
        {
            currentGameType = 0;
        }

        gameTypeBtn.text = "GAME TYPE: " + GameTypes[currentGameType];

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
        if (this.open)
        {
            this.DrawBackground(Raylib.ColorAlpha(Color.BLACK, 0.6f));
            base.update(shiftx, shifty);
        }
    }
}