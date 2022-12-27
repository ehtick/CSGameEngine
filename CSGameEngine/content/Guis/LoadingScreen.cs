using System.Numerics;
using Raylib_cs;

class LoadingScreen : GuiScreen
{
    public static double progress = 0d;
    public static int progressPerSecond = 0;

    public Label progressLabel;

    public LoadingScreen() : base("LOADING_SCREEN")
    {
        this.position = new Vector2(100, 100);
        this.width = 600;
        this.height = 600;

        progressLabel = new Label(new Vector2(0, 0), 500, 63, Color.RED, "GENERATING WORLD: " + (int)(progress * 100) + "%", 30, true);
        progressLabel.Center = new Vector2(400, 350);
        RegisterUIElement(progressLabel);

        GuiManager.RegisterGui(this);
    }

    public void UpgradeProgressLabel()
    {
        progressLabel.text = "GENERATING WORLD: " + (int)(progress * 100) + "%";
    }

    public override void update(int shiftx = 0, int shifty = 0)
    {
        this.UpgradeProgressLabel();
        DrawBackground(Raylib.ColorAlpha(Color.BLACK, 0.6f));
        base.update(shiftx, shifty);
    }
}