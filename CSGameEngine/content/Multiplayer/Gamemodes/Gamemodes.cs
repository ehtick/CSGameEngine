using System.Reflection;

class Gamemodes
{
    public static string[] LIST = new string[] { "HASTY", "TEST" };
    public static GamemodeObject HASTY = new GamemodeObject(new MultiplayerGamemode("HASTY", new List<StatBooster>()
    {
        new StatBooster("Speed", BoostType.MULTIPLICATIVE, 1.5f),
        new StatBooster("AttackSpeed", BoostType.MULTIPLICATIVE, 1.5f)
    }));

    public static void InitGamemode(GamemodeObject gamemode, Player player)
    {
        foreach (StatBooster sb in gamemode.Gamemode.statBoosters)
        {
            FieldInfo? fieldInfo = player.statManager.GetType().GetField(string.Format("<{0}>k__BackingField", sb.statName), BindingFlags.Instance | BindingFlags.NonPublic);

            if (fieldInfo is FieldInfo)
            {
                if (sb.boostType == BoostType.ADDITIVE)
                {
                    fieldInfo.SetValue(player.statManager, Convert.ToDouble(fieldInfo.GetValue(player.statManager)) + sb.boostValue);
                }
                else if (sb.boostType == BoostType.MULTIPLICATIVE)
                {
                    fieldInfo.SetValue(player.statManager, (float)fieldInfo.GetValue(player.statManager) * sb.boostValue);
                }
                else if (sb.boostType == BoostType.BOOLEAN)
                {
                    fieldInfo.SetValue(player.statManager, sb.boostValueBool);
                }
            }
        }
    }
}