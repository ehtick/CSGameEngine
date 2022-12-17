using Raylib_cs;
using System.Numerics;

public class Binding
{
    public static Binding MOVE_LEFT = new Binding(KeyboardKey.KEY_A, -1, 0);
    public static Binding MOVE_RIGHT = new Binding(KeyboardKey.KEY_D, 1, 0);
    public static Binding MOVE_UP = new Binding(KeyboardKey.KEY_W, 0, -1);
    public static Binding MOVE_DOWN = new Binding(KeyboardKey.KEY_S, 0, 1);

    public static Binding MAIN_MENU = new Binding(KeyboardKey.KEY_ESCAPE, 0, 0);

    public KeyboardKey key;
    public Vector2 axis;

    public Binding(KeyboardKey key, Vector2 axis)
    {
        this.key = key;
        this.axis = axis;
    }

    public Binding(KeyboardKey key, int x, int y) : this(key, new Vector2(x, y))
    {

    }
}