using System.Numerics;
using Raylib_cs;

class MultiplayerPlayer : Entity
{
    public static Texture playerTexture;

    public string username;
    public float health;

    public float angle = 0.0f;

    public MultiplayerPlayer(Level level, string username, Vector2 position) : base(level, Entities.PLAYER, username, false)
    {
        Position = position;
        this.username = username;

        level.entityManager.Entities.Remove(this);
    }

    public static void Init()
    {
        playerTexture = new Texture("textures/entities/player/player-texture.png", 50, 50);
    }

    public override void update(int shiftx = 0, int shifty = 0)
    {
        angle += Configuration.config.playerRotationSpeed * Time.deltaTime;

        if (angle >= 360)
        {
            angle -= 360;
        }

        int x = (int)Position.X - shiftx;
        int y = (int)Position.Y - shifty;

        Rlgl.rlPushMatrix();
        Rlgl.rlTranslatef(x + 25, y + 25, 0);
        Rlgl.rlRotatef(angle, 0, 0, 1);
        Rlgl.rlTranslatef(-(x + 25), -(y + 25), 0);

        if (health > 0)
        {
            Raylib.DrawTexture(playerTexture.LoadedTexture, x, y, ColourHelper.ColorFromHex("FF0000"));
        }
        else if (((MultiplayerServer)LevelManager.ActiveLevel).player.statManager.Health <= 0)
        {
            Raylib.DrawTexture(playerTexture.LoadedTexture, x, y, ColourHelper.ColorFromHex("FF8B8B"));
        }

        Rlgl.rlPopMatrix();
    }
}