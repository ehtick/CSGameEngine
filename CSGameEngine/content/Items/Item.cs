class Item
{
    public string name { get; set; }

    public ItemRarity rarity { get; set; }

    public Item(string name, ItemRarity? rarity = null)
    {
        this.name = name;

        if (rarity is null)
        {
            rarity = ItemRarity.GetRandomItemRarity();
        }
    }
}