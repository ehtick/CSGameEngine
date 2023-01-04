class Configuration
{
    public static Configuration config = Config.LoadFile<Configuration>("config.json", typeof(Configuration));

    public int SCREEN_WIDTH { get; set; }
    public int SCREEN_HEIGHT { get; set; }

    public int lightSize { get; set; }

    public int attackRangeRadius { get; set; }
    public int attackRangeBaseLength { get; set; }

    public bool debugMode { get; set; }

    public bool showNetworkMessages { get; set; }
}