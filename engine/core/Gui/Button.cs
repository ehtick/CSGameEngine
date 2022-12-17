using Raylib_cs;
using System.Numerics;

class Button : Rectangle
{
    public string text;
    public int fontSize;
    public bool center_text;

    public Color baseColour;
    public Color hoverColour;
    public Color clickColour;

    public Func<Vector2, int> onClick;

    public Vector2 lastMouseClickPos = new Vector2(-1, -1);
    public bool holdingClick = false;

    public Button(Vector2 position, int width, int height, Color colour, string text, int fontSize, Func<Vector2, int> onClick, bool center_text = true) : base(position, width, height, colour)
    {
        this.text = text;
        this.fontSize = fontSize;
        this.center_text = center_text;

        this.onClick = onClick;

        this.baseColour = Raylib.ColorAlpha(colour, 0.6f);
        this.hoverColour = Raylib.ColorAlpha(colour, 0.8f);
        this.clickColour = colour;
    }

    public override void draw(int shiftx = 0, int shifty = 0)
    {
        Vector2 mousePos = Raylib.GetMousePosition();

        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            if (VectorMath.VectorCollision(mousePos, this))
            {
                lastMouseClickPos = mousePos;
                holdingClick = true;
            }
        }

        if (!Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT) && holdingClick && VectorMath.VectorCollision(mousePos, this))
        {
            holdingClick = false;
            this.onClick(mousePos);
        }

        if (VectorMath.VectorCollision(mousePos, this))
        {
            if (!holdingClick)
            {
                colour = hoverColour;
            }
            else colour = clickColour;
        }
        else
        {
            colour = baseColour;
            holdingClick = false;
        }

        base.draw(shiftx, shifty);

        int textWidth = Raylib.MeasureText(this.text, fontSize);
        int textCenterX = (int)textWidth / 2;

        int textHeight = (int)fontSize;
        int textCenterY = (int)textHeight / 2;

        Raylib.DrawText(text, (int)(this.Center.X - textCenterX), (int)(this.Center.Y - textCenterY), fontSize, Color.BLACK);
    }
}