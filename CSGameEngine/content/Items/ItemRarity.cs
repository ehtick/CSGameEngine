// Common - Gray - 50%
// Uncommon - Green - 30%
// Rare - Blue - 10%
// Legendary - Golden % 9.9
// Mythical - Red - 0.1

class ItemRarity
{
    public static ItemRarity COMMON = new ItemRarity("9F9F9F", 1.0f, 50.0f);
    public static ItemRarity UNCOMMON = new ItemRarity("05AA00", 1.2f, 30.0f);
    public static ItemRarity RARE = new ItemRarity("4556FF", 1.5f, 15.0f);
    public static ItemRarity LEGENDARY = new ItemRarity("FCBF00", 2.0f, 4.9f);
    public static ItemRarity MYTHICAL = new ItemRarity("FF3737", 2.5f, 0.1f);

    public static List<ItemRarity> RARITIES = new List<ItemRarity>() { COMMON, UNCOMMON, RARE, LEGENDARY, MYTHICAL };


    public string Colour;
    public float UniversalMultiplier;
    public float chance;

    public ItemRarity(string Colour, float unimul, float chance)
    {
        this.Colour = Colour;
        this.UniversalMultiplier = unimul;
        this.chance = chance;
    }

    public static ItemRarity GetRandomItemRarity()
    {
        List<ItemRarity> rarelist = new List<ItemRarity>();

        foreach (ItemRarity rarity in RARITIES)
        {
            for (int i = 0; i < rarity.chance * 10; i++)
            {
                rarelist.Add(rarity);
            }
        }

        Random random = new Random();

        int rand = random.Next(1000);

        return rarelist[rand];
    }
}