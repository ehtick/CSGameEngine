using Raylib_cs;

public class ColourHelper
{
    public static Color ColorFromHex(string hexCode)
    {
        // Parse the hex code string to get the individual red, green, and blue components
        int r = int.Parse(hexCode.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        int g = int.Parse(hexCode.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        int b = int.Parse(hexCode.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        // Create a new Color struct with the parsed color values
        return new Color(r, g, b, 255);
    }

}
