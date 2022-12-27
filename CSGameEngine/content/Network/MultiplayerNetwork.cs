class MultiplayerNetwork
{
    public SocketIOHandler handler;

    public MultiplayerNetwork()
    {
        handler = new SocketIOHandler();
    }

    public async Task<int> ConnectToServer(string uri)
    {
        return await handler.ConnectToServer(uri);
    }
}