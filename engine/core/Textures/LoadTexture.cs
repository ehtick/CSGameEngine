using Raylib_cs;

static class Textures
{
    public static Texture2D LoadTexture(string relpath)
    {
        return Raylib.LoadTexture(Path.Join(Environment.CurrentDirectory, "assets", relpath));
    }

    public static Texture2D LoadTexture(string relpath, int width, int height)
    {
        Texture2D texture = Raylib.LoadTexture(Path.Join(Environment.CurrentDirectory, "assets", relpath));
        texture.width = width;
        texture.height = height;
        return texture;
    }
}