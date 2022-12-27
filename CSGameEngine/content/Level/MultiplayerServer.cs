using Raylib_cs;
using System.Numerics;
using System.Text.Json;
using RSG;

class MultiplayerServer : Level
{
    public Player player;
    public Camera2D camera;

    public MultiplayerNetwork network;

    Minimap minimap;

    public Light light;

    public Raeuleaux attackRange;

    public string playerUsername;

    public Dictionary<string, PlayerObject> Players = new Dictionary<string, PlayerObject>();

    public MultiplayerServer(string uri, string username, string PlayerClass) : base()
    {
        player = new Player(this, PlayerClass);
        camera = player.camera;

        this.playerUsername = username;

        network = new MultiplayerNetwork();

        ConnectToServer(uri);

        minimap = new Minimap(this, player);

        this.light = new Light((int)(Configuration.config.lightSize), 0.4f, (int)(Configuration.config.lightSize * 1.33), new Vector2(0, 0), false);

        attackRange = new Raeuleaux(player.rect.Center, Configuration.config.attackRangeRadius, Configuration.config.attackRangeBaseLength, new Vector2(0, 0));

        LevelManager.RegisterLevel(this);
    }

    public async void ConnectToServer(string uri)
    {
        GuiManager.OpenGui("SERVER_CONNECT_PENDING");
        int code = await network.ConnectToServer(uri);

        new Thread(WaitForLevel).Start();
    }

    public void NetworkLoop()
    {
        while (true)
        {
            network.handler.SendMessage(JsonSerializer.Serialize(new PlayerObject((int)camera.position.X + 400, (int)camera.position.Y + 400, playerUsername)));

            string response = network.handler.GetResponse();

            try
            {
                Dictionary<string, PlayerObject>? pobjs = JsonSerializer.Deserialize<Dictionary<string, PlayerObject>>(response);

                if (!(pobjs is Dictionary<string, PlayerObject>))
                {
                    throw new Exception();
                }

                Players = pobjs;

                foreach (KeyValuePair<string, PlayerObject> pobj in pobjs)
                {
                    if (!entityManager.PlayerRegistered(pobj.Value.username))
                    {
                        entityManager.Players.Add(new MultiplayerPlayer(this, pobj.Value.username, new Vector2(pobj.Value.x, pobj.Value.y)));
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
    }

    public void WaitForLevel()
    {
        string response = network.handler.GetResponse();

        JsonSerializerOptions opts = new JsonSerializerOptions() { };
        MapObject? MAP = JsonSerializer.Deserialize<MapObject>(response, opts);

        if (!(MAP is MapObject))
        {
            throw new Exception();
        }

        this.entityManager.RegisterEntities(Level.GenerateBorder(this, -(MAP.MAPSIZE / 2), MAP.MAPSIZE / 2, -(MAP.MAPSIZE / 2), MAP.MAPSIZE / 2));

        int _y = MAP.rtop;
        int _x = MAP.rleft;

        List<Vector2> possibleSpawnLocations = new List<Vector2>();

        foreach (List<bool> row in MAP.MAP)
        {
            _x = MAP.rleft;
            foreach (bool cell in row)
            {
                if (cell)
                {
                    new Wall(this, new Vector2(_x * 100, _y * 100));
                }
                else
                {
                    if (_x < MAPSIZE / 2)
                    {
                        possibleSpawnLocations.Add(new Vector2(_x * 100, _y * 100));
                    }
                }
                _x++;
            }
            _y++;
        }

        Vector2 spawnLoc = possibleSpawnLocations[new Random().Next(0, possibleSpawnLocations.Count - 1)];
        camera.position = new Vector2(spawnLoc.X - 350, spawnLoc.Y - 350);

        GuiManager.CloseGui("SERVER_CONNECT_PENDING");
        LevelManager.ActiveLevel = this;

        new Thread(NetworkLoop).Start();
    }

    public override void update()
    {
        foreach (KeyValuePair<string, PlayerObject> pobj in Players)
        {
            MultiplayerPlayer? player = entityManager.GetPlayerByUsername(pobj.Value.username);

            if (player is MultiplayerPlayer)
            {
                player.Position = new Vector2(pobj.Value.x, pobj.Value.y);
            }
        }

        entityManager.update(camera);

        Raylib.DrawRectangle(0, 0, Configuration.config.SCREEN_WIDTH, Configuration.config.SCREEN_HEIGHT, Raylib.ColorAlpha(Color.BLACK, 0.8f));

        // Mouse guided light
        Vector2 mousepos = Raylib.GetMousePosition();

        this.light.Position = mousepos;
        this.light.update();

        minimap.update();

        attackRange.targetPoint = mousepos;
        attackRange.update();
    }
}