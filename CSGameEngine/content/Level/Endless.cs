using RSG;
using Raylib_cs;
using System.Numerics;

class Endless : Level
{
    public Player player;
    public Camera2D camera;

    bool generatedWorld = false;

    Minimap minimap;

    public new int MAPSIZE = 1600;

    public Light light;

    public Raeuleaux attackRange;

    public Fire fire;

    public Endless(string PlayerClass) : base()
    {
        player = new Player(this, PlayerClass);
        camera = player.camera;

        new Thread(this.generateWorldThread).Start();

        this.entityManager.RegisterEntities(Level.GenerateBorder(this, -(MAPSIZE / 2), MAPSIZE / 2, -(MAPSIZE / 2), MAPSIZE / 2));

        new Wizard(this, new Vector2(400, 400));

        minimap = new Minimap(this, player);

        this.light = new Light((int)(Configuration.config.lightSize), 0.2f, (int)(Configuration.config.lightSize * 1.33), new Vector2(0, 0), false);

        attackRange = new Raeuleaux(player.rect.Center, Configuration.config.attackRangeRadius, Configuration.config.attackRangeBaseLength, new Vector2(0, 0));

        fire = new Fire(new Vector2(100, 100), 100, 100, true);
        fire.LoadParticles();
    }

    public void generateWorldThread()
    {
        GuiManager.OpenGui("LOADING_SCREEN");

        IPromise<bool> worldGenerationPromise = Level.GenerateLevelContents(this, -(MAPSIZE / 2), MAPSIZE / 2, -(MAPSIZE / 2), MAPSIZE / 2, player);

        worldGenerationPromise.Then((bool completed) =>
        {
            GuiManager.CloseGui("LOADING_SCREEN");
            generatedWorld = true;
        });
    }

    public override void update()
    {
        if (generatedWorld)
        {
            entityManager.update(camera);
        }
        else
        {
            if (!GuiManager.IsGuiOpen("LOADING_SCREEN"))
            {
                GuiManager.OpenGui("LOADING_SCREEN");
            }
        }

        // Create Fog
        Raylib.DrawRectangle(0, 0, Configuration.config.SCREEN_WIDTH, Configuration.config.SCREEN_HEIGHT, Raylib.ColorAlpha(Color.BLACK, 0.98f));

        // Mouse guided light
        Vector2 mousepos = Raylib.GetMousePosition();

        this.light.Position = mousepos;
        this.light.update();

        minimap.update();

        attackRange.targetPoint = mousepos;
        attackRange.update();

        fire.update((int)player.camera.position.X, (int)player.camera.position.Y);
    }
}