using System.Numerics;
using Raylib_cs;

class WaitingGamemodeSelect : GuiScreen
{
    public WaitingGamemodeSelect() : base("WAITING_GAMEMODE_SELECT")
    {


        GuiManager.RegisterGui(this);
    }

    public override void update(int shiftx = 0, int shifty = 0)
    {
        DrawBackground(Color.BLACK);
        base.update(shiftx, shifty);

        Console.WriteLine("NOT HOST");
    }
}