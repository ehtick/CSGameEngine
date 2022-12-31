using System.Numerics;
using Raylib_cs;

class WaitingGamemodeSelect : GuiScreen
{
    public WaitingGamemodeSelect() : base("WAITING_GAMEMODE_SELECT")
    {
        Rectangle labelBg = new Rectangle(Vector2.Zero, 600, 100, Color.RED);
        labelBg.Center = new Vector2(400, 400);
        RegisterUIElement(labelBg);

        Label message = new Label(Vector2.Zero, 600, 100, Color.BLACK, "WAITING FOR HOST TO SELECT GAMEMODE", 35, true);
        message.Center = new Vector2(400, 400);
        RegisterUIElement(message);

        GuiManager.RegisterGui(this);
    }

    public override void update(int shiftx = 0, int shifty = 0)
    {
        DrawBackground(Color.BLACK);
        base.update(shiftx, shifty);
    }
}