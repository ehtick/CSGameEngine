using Raylib_cs;
using System.Numerics;
using System.Text.Json;
using System.Net.Sockets;
using System.Reflection;

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

    public bool ConnectedToServer = false;

    public string uri;

    public Handshake? handshake;

    public static bool GameStarted = false;

    public static GamemodeObject? gamemode = null;

    public static bool PlayersReady = false;

    public static bool firstUpdate = true;

    public MultiplayerServer(string uri, string username, string PlayerClass) : base()
    {
        player = new Player(this, PlayerClass);
        camera = player.camera;

        this.playerUsername = username;

        network = new MultiplayerNetwork();

        this.uri = uri;
        new Thread(ConnectToServer).Start();

        minimap = new Minimap(this, player);

        this.light = new Light((int)(Configuration.config.lightSize), 0.4f, (int)(Configuration.config.lightSize * 1.33), new Vector2(0, 0), false);

        attackRange = new Raeuleaux(player.rect.Center, Configuration.config.attackRangeRadius, Configuration.config.attackRangeBaseLength, new Vector2(0, 0));

        LevelManager.RegisterLevel(this);
    }

    public void ConnectToServer()
    {
        GuiManager.OpenGui("SERVER_CONNECT_PENDING");

        try
        {
            Console.WriteLine("CONNECTING TO SERVER => " + uri);
            network.ConnectToServer(uri);

            new Thread(Handshake).Start();
        }
        catch (FormatException e)
        {
            GuiManager.CloseGui("SERVER_CONNECT_PENDING");
            ServerConnectError.CONNECTION_ERROR = "INVALID SERVER ADDRESS";
            GuiManager.OpenGui("SERVER_CONNECT_ERROR");
        }
        catch (SocketException e)
        {
            GuiManager.CloseGui("SERVER_CONNECT_PENDING");
            ServerConnectError.CONNECTION_ERROR = "SERVER CONNECTION TIMED OUT (5000MS)";
            GuiManager.OpenGui("SERVER_CONNECT_ERROR");
        }
    }

    public void NetworkLoop()
    {
        while (true)
        {
            string response = network.handler.GetResponse();
            Dictionary<string, PlayerObject>? pobjs = JsonSerializer.Deserialize<Dictionary<string, PlayerObject>>(response);

            try
            {

                if (!(pobjs is Dictionary<string, PlayerObject>))
                {
                    throw new Exception();
                }

                foreach (KeyValuePair<string, PlayerObject> pobj in pobjs)
                {
                    if (pobj.Value.username != playerUsername)
                    {
                        if (!entityManager.PlayerRegistered(pobj.Value.username))
                        {
                            entityManager.Players.Add(new MultiplayerPlayer(this, pobj.Value.username, new Vector2(pobj.Value.x, pobj.Value.y)));
                        }
                    }

                }
            }
            catch (Exception e)
            {
            }

            pobjs[this.playerUsername] = new PlayerObject((int)camera.position.X + 400 - 25, (int)camera.position.Y + 400 - 25, playerUsername);
            Players = pobjs;

            network.handler.SendMessage(JsonSerializer.Serialize(pobjs));
        }
    }

    public void Handshake()
    {
        network.handler.SendMessage(JsonSerializer.Serialize(new HandshakeClient(this.playerUsername)));

        string response = network.handler.GetResponse();
        handshake = JsonSerializer.Deserialize<Handshake>(response);

        if (handshake.success)
        {
            ConnectedToServer = true;
            GuiManager.CloseGui("SERVER_CONNECT_PENDING");
            new Thread(WaitForGameStart).Start();
        }
        else
        {
            GuiManager.CloseGui("SERVER_CONNECT_PENDING");

            if (handshake.error == HandshakeError.SER_MAX_CAP)
            {
                ServerConnectError.CONNECTION_ERROR = "SERVER REACHED MAXIMUM PLAYER CAPACITY";
            }
            else if (handshake.error == HandshakeError.GAME_STARTED)
            {
                ServerConnectError.CONNECTION_ERROR = "THIS SERVER ALREADY STARTED THEIR GAME";
            }

            GuiManager.OpenGui("SERVER_CONNECT_ERROR");
        }
    }

    public async void WaitForGameStart()
    {
        WaitingForPlayers.isHost = handshake.isHost;
        GuiManager.OpenGui("WAITING_FOR_PLAYERS");

        while (true)
        {
            network.handler.SendMessage("request_players");
            string _res = network.handler.GetResponse();
            List<string>? players = JsonSerializer.Deserialize<List<string>>(_res);

            if (players is List<string>)
            {
                WaitingForPlayers.Players = players;
            }

            if (!WaitingForPlayers.isHost)
            {
                network.handler.SendMessage("game_started");
                string res = network.handler.GetResponse();
                PlayersReady = JsonSerializer.Deserialize<bool>(res);
            }

            if (PlayersReady)
            {
                break;
            }
        }

        if (WaitingForPlayers.isHost)
        {
            network.handler.SendMessage("start_game");
        }

        GuiManager.CloseGui("WAITING_FOR_PLAYERS");

        if (handshake.isHost)
        {
            GuiManager.OpenGui("GAMEMODE_SELECT");

            while (true)
            {
                if (gamemode != null)
                {
                    break;
                }
            }

            network.handler.SendMessage(JsonSerializer.Serialize<GamemodeObject>(gamemode));
        }
        else
        {
            GuiManager.OpenGui("WAITING_GAMEMODE_SELECT");

            gamemode = JsonSerializer.Deserialize<GamemodeObject>(network.handler.GetResponse());
            network.handler.SendMessage("received_gamemode");

            GuiManager.CloseGui("WAITING_GAMEMODE_SELECT");
        }

        MapObject MAP = handshake.MAP;

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

        LevelManager.ActiveLevel = this;

        new Thread(NetworkLoop).Start();

        GameStarted = true;
    }

    public override void update()
    {
        if (ConnectedToServer && PlayersReady && GameStarted)
        {
            if (firstUpdate)
            {
                Gamemodes.InitGamemode(gamemode, player);

                firstUpdate = false;
            }

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
}