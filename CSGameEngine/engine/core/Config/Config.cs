using System.IO;
using System.Text.Json;

class Config
{
    public static T LoadFile<T>(string path, Type type)
    {
        string rawjson = File.ReadAllText(Path.Join(Environment.CurrentDirectory, path));

        object? json = JsonSerializer.Deserialize(rawjson, type);

        if (json is object)
        {
            return (T)json;
        }
        else throw new Exception();
    }

    public Config()
    {

    }
}