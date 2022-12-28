using System.Numerics;
using Raylib_cs;
using System.Reflection;

class GamemodeSelect : GuiScreen
{
    public OptionsArray optionsArray;
    public GamemodeSelect() : base("GAMEMODE_SELECT")
    {
        Label title = new Label(Vector2.Zero, 600, 100, Color.RED, "SELECT A GAMEMODE", 45, true);
        title.Center = new Vector2(400, 150);
        RegisterUIElement(title);

        optionsArray = new OptionsArray(Gamemodes.LIST, 15, 15, 100, 250, 600, 125, 75, 5, 30);
        RegisterUIElement(optionsArray);

        Button selectGamemode = CreateButton(Vector2.Zero, 500, 75, Color.RED, "SELECT GAMEMODE", 40, (Vector2 clickpos) =>
        {
            string selectedGamemode = optionsArray.SelectedOption;
            Type t = typeof(Gamemodes);
            FieldInfo? fieldInfo = t.GetField(selectedGamemode, BindingFlags.Public | BindingFlags.Static);
            GamemodeObject? obj = (GamemodeObject?)fieldInfo.GetValue(null);
            MultiplayerServer.gamemode = obj;
            GuiManager.CloseGui(this.name);
            return 0;
        }, true);
        selectGamemode.Center = new Vector2(400, 500);

        GuiManager.RegisterGui(this);
    }

    public override void update(int shiftx = 0, int shifty = 0)
    {
        DrawBackground(Color.BLACK);
        base.update(shiftx, shifty);
    }
}