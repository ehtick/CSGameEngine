class StatBooster
{
    public string statName { get; set; }
    public BoostType boostType { get; set; }
    public float boostValue { get; set; }

    public StatBooster(string statName, BoostType boostType, float boostValue)
    {
        this.statName = statName;
        this.boostType = boostType;
        this.boostValue = boostValue;
    }
}

enum BoostType
{
    ADDITIVE,
    MULTIPLICATIVE
}