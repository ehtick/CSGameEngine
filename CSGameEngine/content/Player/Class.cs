class Class
{
    public static Class WARRIOR = new Class("WARRIOR");

    public static Class[] Classes = new Class[] { WARRIOR };

    public string name;
    public Class(string name)
    {
        this.name = name;
    }

    public static Class? GetClass(string name)
    {
        Class? _class = null;

        foreach (Class c in Classes)
        {
            if (c.name.ToLower() == name.ToLower())
            {
                _class = c;
                break;
            }
        }

        return _class;
    }
}