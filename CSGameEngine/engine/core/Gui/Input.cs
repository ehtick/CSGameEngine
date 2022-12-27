using Raylib_cs;
using System.Numerics;

class Input : Button
{
    public bool active = false;

    public string textwritten;
    public string placeholder;

    public Input(Vector2 position, int width, int height, Color colour, string text, int fontSize, bool center_text = true) : base(position, width, height, colour, text, fontSize)
    {
        this.center_text = center_text;
        base.onClick = inputClick;
        base.onNotClick = inputNotClick;

        this.text = "";
        this.textwritten = "";
        this.placeholder = text;
    }

    public int inputClick(Vector2 clickpos)
    {
        active = true;
        return 0;
    }

    public int inputNotClick(Vector2 clickpos)
    {
        active = false;
        return 0;
    }

    public override void update(int shiftx = 0, int shifty = 0)
    {
        if (textwritten == "")
        {
            text = placeholder;
            fontColour = Raylib.ColorAlpha(Color.BLACK, 0.5f);
        }
        else
        {
            text = textwritten;
            fontColour = Color.BLACK;
        }

        base.update(shiftx, shifty);

        if (active)
        {
            int key = Raylib.GetCharPressed();

            while (key > 0)
            {
                if ((key >= 32) && (key <= 125))
                {
                    textwritten += (char)key;
                }

                key = Raylib.GetCharPressed();  // Check next character in the queue
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE))
            {
                if (textwritten.Length > 0)
                {
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_CONTROL))
                    {
                        textwritten = "";
                    }
                    else
                        textwritten = textwritten.Remove(textwritten.Length - 1, 1);
                }
            }
        }
    }
}