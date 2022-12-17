using Raylib_cs;
using System.Numerics;

class GuiScreen : IDrawable
{
    public List<IDrawable> UIELements = new List<IDrawable>();

    public bool open = false;

    public Guid id = Guid.NewGuid();

    public string name;

    public Vector2 position = Vector2.Zero;
    public int width = 800;
    public int height = 800;

    public GuiScreen(string name)
    {
        this.name = name;
    }

    public virtual void draw(int shiftx = 0, int shifty = 0)
    {
        foreach (IDrawable uiel in UIELements)
        {
            uiel.draw(shiftx, shifty);
        }
    }

    public void DrawBackground(Color color)
    {
        Raylib.DrawRectangle((int)position.X, (int)position.Y, width, height, color);
    }

    public void CreateButton(Vector2 position, int width, int height, Color colour, string text, int fontSize, Func<Vector2, int> onClick, bool center_text = true)
    {
        UIELements.Add(new Button(position, width, height, colour, text, fontSize, onClick, center_text));
    }

    public void RegisterUIElement(IDrawable uiel)
    {
        UIELements.Add(uiel);
    }
}