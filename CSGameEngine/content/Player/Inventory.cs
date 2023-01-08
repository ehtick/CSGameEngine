using Raylib_cs;

class Inventory
{
    public int size;

    public List<Item?> Items;
    public List<Item?> Hotbar = new List<Item?>(5);

    public Item? equippedItem = null;
    public int equippedIndex = 0;

    public Inventory(int size)
    {
        this.size = size;

        Items = new List<Item?>(new Item?[size]);
    }

    public void update(Player player)
    {
        Hotbar = Items.Chunk(5).First().ToList();

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_I))
        {

        }

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
        {
            equippedIndex++;
            if (equippedIndex == 5)
            {
                equippedIndex = 0;
            }
        }

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
        {
            equippedIndex--;
            if (equippedIndex == -1)
            {
                equippedIndex = 4;
            }
        }

        equippedItem = Items[equippedIndex];

        if (equippedItem is Item)
        {
            equippedItem.updateIngame(player);
        }
    }

    public int GetNextIndex()
    {
        int index = -1;

        int i = 0;
        foreach (Item? item in Items)
        {
            if (item == null)
            {
                index = i;
                break;
            }

            i++;
        }

        return index;
    }

    public void AddItem(Item item)
    {
        int index = GetNextIndex();

        if (index > -1)
            Items[index] = item;
    }

    public void RemoveItem(int index)
    {
        Items[index] = null;
    }

    public void RemoveItem(Item item, int startIndex = 0)
    {
        for (int i = startIndex; i < Items.Count; i++)
        {
            if (Items[i] is Item && Items[i].GetType() == item.GetType())
            {
                Items[i] = null;
                break;
            }
        }
    }
}