using Raylib_cs;
using System.Numerics;

class Light : IUpdateable
{
    public int size, totalRange;
    public float intensity;

    public bool affectedByCamera;

    private Vector2 position;
    public Vector2 Position
    {
        get
        {
            return position;
        }
        set
        {
            position = value;
        }
    }

    public Light(int size, float intensity, int totalRange, Vector2 position, bool affectedByCamera = false)
    {
        this.size = size;
        this.intensity = intensity;
        this.totalRange = totalRange;

        this.position = position;

        this.affectedByCamera = affectedByCamera;
    }

    public void update(int shiftx = 0, int shifty = 0)
    {
        Vector2 center = affectedByCamera ? position - new Vector2(shiftx, shifty) : position;
        int fade = totalRange - size;

        float intensityfade = intensity / fade;

        for (int i = totalRange; i > 0; i -= 4)
        {
            if (i <= size)
            {
                Raylib.DrawCircle((int)center.X, (int)center.Y, i, Raylib.ColorAlpha(Color.WHITE, intensityfade));
            }
        }
    }
}