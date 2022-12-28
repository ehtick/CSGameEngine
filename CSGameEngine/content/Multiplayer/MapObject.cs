class MapObject
{
    public int rleft { get; set; }
    public int rright { get; set; }
    public int rtop { get; set; }
    public int rbottom { get; set; }

    public int MAPSIZE { get; set; }

    public List<List<bool>> MAP { get; set; }

    public MapObject(int rleft, int rright, int rtop, int rbottom, int MAPSIZE, List<List<bool>> MAP)
    {
        this.rleft = rleft;
        this.rright = rright;
        this.rtop = rtop;
        this.rbottom = rbottom;

        this.MAPSIZE = MAPSIZE;

        this.MAP = MAP;
    }
}