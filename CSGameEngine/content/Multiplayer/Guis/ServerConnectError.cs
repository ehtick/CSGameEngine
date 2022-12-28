using System.Numerics;
using Raylib_cs;

class ServerConnectError : GuiScreen
{
    public Label errorCode;
    public static string CONNECTION_ERROR = "";
    public ServerConnectError() : base("SERVER_CONNECT_ERROR")
    {
        this.position = new Vector2((Configuration.config.SCREEN_WIDTH - 650) / 2, (Configuration.config.SCREEN_HEIGHT - 300) / 2);
        this.width = 650;
        this.height = 200;

        Label title = new Label(new Vector2(0, 0), 0, 0, Color.RED, "UNABLE TO CONNECT TO SERVER", 35, true);
        title.Center = new Vector2(400, 325);
        RegisterUIElement(title);

        errorCode = new Label(new Vector2(0, 0), 0, 0, Color.RED, CONNECTION_ERROR, 25, true);
        errorCode.Center = new Vector2(400, 350);
        RegisterUIElement(errorCode);

        Button closeButton = CreateButton(Vector2.Zero, 400, 50, Color.RED, "CLOSE", 40, close, true);
        closeButton.Center = new Vector2(400, 500);

        GuiManager.RegisterGui(this);
    }

    public int close(Vector2 position)
    {
        GuiManager.CloseGui(this.name);
        GuiManager.OpenGui("MULTIPLAYER_CONNECT");
        return 0;
    }

    public override void update(int shiftx = 0, int shifty = 0)
    {
        errorCode.text = CONNECTION_ERROR;
        errorCode.Center = new Vector2(400, 375);

        DrawBackground(Color.RED);
        base.update(shiftx, shifty);
    }
}