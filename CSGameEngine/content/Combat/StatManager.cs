using System.Text.Json;

public class StatManager
{
    public string ClassName { get; set; }

    public float PhysicalAttackDamage { get; set; }
    public float MagicalAttackDamage { get; set; }
    public float AttackSpeed { get; set; }
    public float CriticalChance { get; set; }
    public float CriticalMultiplier { get; set; }

    public float Health { get; set; }
    public float MaxHealth { get; set; }

    public float Mana { get; set; }
    public float MaxMana { get; set; }

    public float Speed { get; set; }

    public StatManager()
    {

    }

    public override string ToString()
    {
        var opt = new JsonSerializerOptions() { WriteIndented = true };
        string strJson = JsonSerializer.Serialize<StatManager>(this, opt);
        return strJson;
    }

    public StatManager DeepCopy()
    {
        StatManager sm = new StatManager();
        sm.ClassName = this.ClassName;

        sm.PhysicalAttackDamage = this.PhysicalAttackDamage;
        sm.MagicalAttackDamage = this.MagicalAttackDamage;

        sm.AttackSpeed = this.AttackSpeed;

        sm.CriticalChance = this.CriticalChance;
        sm.CriticalMultiplier = this.CriticalMultiplier;

        sm.Health = this.Health;
        sm.MaxHealth = this.MaxHealth;

        sm.Mana = this.Mana;
        sm.MaxMana = this.MaxMana;

        sm.Speed = this.Speed;

        return sm;
    }
}

public class StatPreset
{
    public static StatManager BALANCED = StatPreset.FromData("BALANCED", 20.0f, 20.0f, 1.0f, 15.0f, 1.2f, 100.0f, 100.0f, 0.2f);
    public static StatManager WARRIOR = StatPreset.FromData("WARRIOR", 35.0f, 5.0f, 0.8f, 10.0f, 1.5f, 150.0f, 25.0f, 0.1f);
    public static StatManager MAGE = StatPreset.FromData("MAGE", 5.0f, 35.0f, 0.6f, 40.0f, 1.1f, 75.0f, 150.0f, 0.1f);
    public static StatManager ASSASSIN = StatPreset.FromData("ASSASSIN", 15.0f, 10.0f, 2.0f, 35.0f, 2.0f, 50.0f, 50.0f, 0.4f);

    public static StatManager FromData(string ClassName, float PhysicalAttackDamage, float MagicalAttackDamage, float AttackSpeed, float CriticalChance, float CriticalMultiplier, float Health, float Mana, float Speed)
    {
        StatManager sm = new StatManager();
        sm.ClassName = ClassName;

        sm.PhysicalAttackDamage = PhysicalAttackDamage;
        sm.MagicalAttackDamage = MagicalAttackDamage;

        sm.AttackSpeed = AttackSpeed;

        sm.CriticalChance = CriticalChance;
        sm.CriticalMultiplier = CriticalMultiplier;

        sm.Health = Health;
        sm.MaxHealth = Health;

        sm.Mana = Mana;
        sm.MaxMana = Mana;

        sm.Speed = Speed;

        return sm;
    }

    public static StatManager? GetPreset(string name)
    {
        Type? type = Type.GetType("StatPreset");
        if (type is Type)
        {
            System.Reflection.FieldInfo? field = type.GetField(name);
            if (field is System.Reflection.FieldInfo)
            {
                return (StatManager?)(field.GetValue(null));
            }
            else return null;
        }
        else return null;
    }
}