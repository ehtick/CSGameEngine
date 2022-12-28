class PlayerObject
{
    public string username { get; set; }
    public int x { get; set; }
    public int y { get; set; }

    public PlayerObject(int x, int y, string username)
    {
        this.username = username;

        this.x = x;
        this.y = y;
    }
}