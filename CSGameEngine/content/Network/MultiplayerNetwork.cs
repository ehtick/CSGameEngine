class MultiplayerNetwork
{
    public SocketIOHandler handler;

    public MultiplayerNetwork()
    {
        handler = new SocketIOHandler();
    }

    public int ConnectToServer(string uri)
    {
        return handler.ConnectToServer(uri);
    }
}