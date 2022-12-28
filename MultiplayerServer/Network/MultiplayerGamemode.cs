class MultiplayerGamemode
{
    public string name { get; set; }
    public List<StatBooster> statBoosters { get; set; }

    public MultiplayerGamemode(string name, List<StatBooster> statBoosters)
    {
        this.name = name;
        this.statBoosters = statBoosters;
    }
}