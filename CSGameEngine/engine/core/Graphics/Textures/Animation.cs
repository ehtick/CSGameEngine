class Animation : IUpdateable
{
    public List<Texture> frames = new List<Texture>();
    public int index = 0;

    public long timeSinceLastFrameUpdate;
    public int msPerFrame;

    public Texture frame;

    public static List<Texture> GetTexturesFromDir(string path, int width = 50, int height = 50)
    {
        List<Texture> Textures = new List<Texture>();

        string actpath = @Path.Join(Environment.CurrentDirectory, "assets", path);

        foreach (string file in Directory.EnumerateFiles(actpath))
        {
            if (file.EndsWith(".png"))
            {
                Textures.Add(new Texture(file.Substring(Path.Join(Environment.CurrentDirectory, "assets").Length), width, height));
            }
        }

        return Textures;
    }

    public Animation(List<Texture> Textures, int fps = 30)
    {
        this.frames = Textures;

        timeSinceLastFrameUpdate = DateTime.Now.Ticks / 10000;
        msPerFrame = 1000 / fps;

        frame = frames[index];
    }

    public Animation(string path, int width = 50, int height = 50, int fps = 30)
    {
        string actpath = @Path.Join(Environment.CurrentDirectory, "assets", path);

        foreach (string file in Directory.EnumerateFiles(actpath))
        {
            if (file.EndsWith(".png"))
                frames.Add(new Texture(file.Substring(Path.Join(Environment.CurrentDirectory, "assets").Length), width, height));
        }

        timeSinceLastFrameUpdate = DateTime.Now.Ticks / 10000;
        msPerFrame = 1000 / fps;

        frame = frames[index];
    }

    public void update(int shiftx = 0, int shifty = 0)
    {
        if (DateTime.Now.Ticks / 10000 >= timeSinceLastFrameUpdate + msPerFrame)
        {
            index++;
            if (index == frames.Count)
            {
                index = 0;
            }

            frame = frames[index];

            timeSinceLastFrameUpdate = DateTime.Now.Ticks / 10000;
        }
    }
}