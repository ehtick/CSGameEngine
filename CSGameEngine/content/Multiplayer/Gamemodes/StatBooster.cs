class StatBooster
{
    public string statName { get; set; }
    public BoostType boostType { get; set; }
    public bool boostValueBool { get; set; }
    public float boostValue { get; set; }

    public StatBooster(string statName, BoostType boostType, float boostValue)
    {
        this.statName = statName;
        this.boostType = boostType;
        this.boostValue = boostValue;
    }

    public StatBooster(string statName, bool boostValue)
    {
        this.statName = statName;
        this.boostType = BoostType.BOOLEAN;
        this.boostValueBool = boostValue;
    }

    public StatBooster()
    {

    }
}

enum BoostType
{
    ADDITIVE,
    MULTIPLICATIVE,
    BOOLEAN,
}