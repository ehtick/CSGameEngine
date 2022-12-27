using Raylib_cs;
using System.Numerics;

class GuiScreen : IUpdateable
{
    public List<IUpdateable> Updateables = new List<IUpdateable>();

    public bool open = false;

    public Guid id = Guid.NewGuid();

    public string name;

    public Vector2 position = Vector2.Zero;
    public int width;
    public int height;

    public GuiScreen(string name)
    {
        this.name = name;

        width = Configuration.config.SCREEN_WIDTH;
        height = Configuration.config.SCREEN_HEIGHT;
    }

    public virtual void update(int shiftx = 0, int shifty = 0)
    {
        foreach (IUpdateable uiel in Updateables)
        {
            uiel.update(shiftx, shifty);
        }

        Vector2 mousepos = Raylib.GetMousePosition();
        Raylib.DrawCircle((int)mousepos.X, (int)mousepos.Y, 10, Color.WHITE);
    }

    public void DrawBackground(Color color)
    {
        Raylib.DrawRectangle((int)position.X, (int)position.Y, width, height, color);
    }

    public Button CreateButton(Vector2 position, int width, int height, Color colour, string text, int fontSize, Func<Vector2, int> onClick, bool center_text = true)
    {
        Button button = new Button(position, width, height, colour, text, fontSize, onClick, center_text);
        Updateables.Add(button);
        return button;
    }

    public Input CreateInput(Vector2 position, int width, int height, Color colour, string text, int fontSize, bool center_text = true)
    {
        Input input = new Input(position, width, height, colour, text, fontSize, center_text);
        Updateables.Add(input);
        return input;
    }

    public void RegisterUIElement(IUpdateable uiel)
    {
        Updateables.Add(uiel);
    }

    public virtual void close()
    {
        foreach (IUpdateable uiel in Updateables)
        {
            if (uiel is ICloseable)
            {
                ((ICloseable)uiel).close();
            }
        }
    }
}