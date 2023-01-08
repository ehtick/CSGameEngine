class PlayerObject
{
    public string username { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public float health { get; set; }

    public PlayerObject(int x, int y, string username, float health)
    {
        this.username = username;

        this.x = x;
        this.y = y;

        this.health = health;
    }
}