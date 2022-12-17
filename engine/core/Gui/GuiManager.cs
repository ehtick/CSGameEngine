class GuiManager : IManager
{
    public static List<GuiScreen> Guis = new List<GuiScreen>();

    public static List<GuiScreen> GuiStack = new List<GuiScreen>();

    public GuiManager()
    {
        // Init Guis
        new MainMenu();

        ManagerController.RegisterManager(this);
    }

    public static void ToggleGui(string name)
    {
        if (IsGuiOpen(name))
        {
            CloseGui(name);
        }
        else
        {
            OpenGui(name);
        }
    }

    public static bool IsGuiOpen(string name)
    {
        return GetOpenedGuiScreenByName(name) != null;
    }

    public static void OpenGui(string name)
    {
        GuiScreen? gui = GetGuiScreenByName(name);

        if (gui is GuiScreen)
        {
            gui.open = true;
            GuiStack.Add(gui);
        }
    }

    public static void CloseGui(string name)
    {
        GuiScreen? gui = GetGuiScreenByName(name);

        if (gui is GuiScreen)
        {
            GuiStack.Remove(gui);
        }
    }

    private static GuiScreen? GetOpenedGuiScreenByName(string name)
    {
        GuiScreen? gui = null;

        foreach (GuiScreen _gui in GuiStack)
        {
            if (_gui.name == name)
            {
                gui = _gui;
                break;
            }
        }

        return gui;
    }

    private static GuiScreen? GetGuiScreenById(Guid guid)
    {
        GuiScreen? gui = null;

        foreach (GuiScreen _gui in Guis)
        {
            if (_gui.id == guid)
            {
                gui = _gui;
                break;
            }
        }

        return gui;
    }

    private static GuiScreen? GetGuiScreenByName(string name)
    {
        GuiScreen? gui = null;

        foreach (GuiScreen _gui in Guis)
        {
            if (_gui.name == name)
            {
                gui = _gui;
                break;
            }
        }

        return gui;
    }

    private static bool IsGuiRegistered(GuiScreen gui)
    {
        bool registered = false;

        foreach (GuiScreen _gui in Guis)
        {
            if (_gui.id == gui.id)
            {
                registered = true;
                break;
            }
        }

        return registered;
    }

    public static void RegisterGui(GuiScreen gui)
    {
        if (!IsGuiRegistered(gui))
        {
            gui.open = false;
            Guis.Add(gui);
        }

    }

    public void update()
    {
        foreach (GuiScreen gui in GuiStack.ToList())
        {
            gui.draw();
        }
    }

    public void close()
    {

    }
}