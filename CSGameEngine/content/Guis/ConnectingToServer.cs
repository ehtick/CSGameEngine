using System.Numerics;
using Raylib_cs;

class ConnectingToServer : GuiScreen
{
    public ConnectingToServer() : base("SERVER_CONNECT_PENDING")
    {
        Label message = new Label(new Vector2(0, 0), 550, 63, Color.RED, "CONNECTING TO SERVER.", 40, true);
        message.Center = new Vector2(400, 400);
        this.RegisterUIElement(message);

        GuiManager.RegisterGui(this);
    }
}