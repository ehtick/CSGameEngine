using Raylib_cs;

class TextureManager : IManager
{
    public static List<Texture> Textures = new List<Texture>();

    public void close()
    {
        foreach (Texture texture in Textures)
        {
            Raylib.UnloadTexture(texture.LoadedTexture);
        }
    }

    public bool isActive()
    {
        return true;
    }

    public void update()
    {
    }

    public static void RegisterTexture(Texture texture)
    {
        Textures.Add(texture);
    }
}

class Texture
{
    public Texture2D LoadedTexture;
    public string TexturePath;

    public Texture(string path)
    {
        TexturePath = path;
        LoadedTexture = Raylib.LoadTexture(Path.Join(Environment.CurrentDirectory, "assets", path));

        TextureManager.RegisterTexture(this);
    }

    public Texture(string path, int width, int height) : this(path)
    {
        LoadedTexture.width = width;
        LoadedTexture.height = height;
    }
}