class Gamemodes
{
    public static string[] LIST = new string[] { "HASTY", "TEST" };
    public static GamemodeObject HASTY = new GamemodeObject(new MultiplayerGamemode("HASTY", new List<StatBooster>()
    {
        new StatBooster("Speed", BoostType.MULTIPLICATIVE, 1.5f),
        new StatBooster("AttackSpeed", BoostType.MULTIPLICATIVE, 1.5f)
    }));


}