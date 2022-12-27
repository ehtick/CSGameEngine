using Raylib_cs;
using System.Numerics;

class Label : Rectangle
{
    public string text;
    public int fontSize;
    public bool center_text;

    public Label(Vector2 position, int width, int height, Color colour, string text, int fontSize, bool center_text) : base(position, width, height, colour)
    {
        this.text = text;
        this.fontSize = fontSize;
        this.center_text = center_text;
    }

    public override void update(int shiftx = 0, int shifty = 0)
    {
        base.update(shiftx, shifty);

        int textWidth = Raylib.MeasureText(this.text, fontSize);
        int textCenterX = (int)textWidth / 2;

        int textHeight = (int)fontSize;
        int textCenterY = (int)textHeight / 2;

        Raylib.DrawText(text, (int)(this.Center.X - textCenterX), (int)(this.Center.Y - textCenterY), fontSize, Color.BLACK);

    }
}