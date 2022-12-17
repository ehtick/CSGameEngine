using Raylib_cs;
using System.Numerics;

class GuiScreen : IDrawable
{
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

    }

    public void DrawBackground(Color color)
    {
        Raylib.DrawRectangle((int)position.X, (int)position.Y, width, height, color);
    }
}