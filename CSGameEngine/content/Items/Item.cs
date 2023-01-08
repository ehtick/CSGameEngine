class Item
{
    public string name { get; set; }

    public Texture image { get; set; }

    public ItemRarity rarity { get; set; }

    public Item(string name, string imagePath, ItemRarity? rarity = null)
    {
        this.name = name;

        this.image = new Texture(imagePath);

        if (rarity is null)
        {
            rarity = ItemRarity.GetRandomItemRarity();
        }
    }

    public virtual void updateIngame(Player player)
    {

    }

    public virtual void updateGui()
    {

    }
}