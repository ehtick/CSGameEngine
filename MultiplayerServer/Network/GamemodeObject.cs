class GamemodeObject
{
    public MultiplayerGamemode Gamemode { get; set; }

    public GamemodeObject(MultiplayerGamemode gamemode)
    {
        this.Gamemode = gamemode;
    }
}