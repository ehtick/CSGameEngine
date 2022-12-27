using System.Numerics;
using Raylib_cs;

class MultiplayerPlayer : Entity
{
    public static List<Texture> playerAnimationTextures;

    public Animation playerAnimation;

    public string username;

    public MultiplayerPlayer(Level level, string username, Vector2 position) : base(level, Entities.PLAYER, username, false)
    {
        Position = position;
        this.username = username;

        playerAnimation = new Animation(playerAnimationTextures, 6);

        level.entityManager.Entities.Remove(this);
    }

    public static void Init()
    {
        playerAnimationTextures = Animation.GetTexturesFromDir("player-animation/", 50, 50);
    }

    public override void update(int shiftx = 0, int shifty = 0)
    {
        playerAnimation.update();

        Raylib.DrawTexture(playerAnimation.frame.LoadedTexture, (int)Position.X - shiftx, (int)Position.Y - shifty, Color.WHITE);
    }
}