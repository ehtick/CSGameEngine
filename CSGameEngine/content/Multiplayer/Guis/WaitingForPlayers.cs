using Raylib_cs;
using System.Numerics;

class WaitingForPlayers : GuiScreen
{
    public static List<string> Players = new List<string>();

    public static bool isHost;

    public Button? startButton = null;

    Grid playerGrid;

    public WaitingForPlayers() : base("WAITING_FOR_PLAYERS")
    {
        Label title = new Label(Vector2.Zero, 600, 100, Color.RED, "WAITING FOR PLAYERS", 40, true);
        title.Center = new Vector2(400, 150);
        RegisterUIElement(title);

        playerGrid = new Grid(Players.ToArray(), 15, 15, 200, 350, 400, 100, 100, 4, 20);

        GuiManager.RegisterGui(this);
    }

    public override void update(int shiftx = 0, int shifty = 0)
    {
        if (startButton == null && isHost)
        {
            startButton = CreateButton(Vector2.Zero, 500, 75, Color.RED, "START GAME", 45, (Vector2 clickpos) =>
            {
                GuiManager.CloseGui(this.name);
                MultiplayerServer.PlayersReady = true;

                return 0;
            });
            startButton.Center = new Vector2(400, 450);
        }

        if (Players.ToArray() != playerGrid.Items)
        {
            playerGrid = new Grid(Players.ToArray(), 15, 15, 200, 250, 400, 150, 75, 4, 20);
        }

        DrawBackground(Color.BLACK);
        base.update(shiftx, shifty);

        playerGrid.update();
    }
}