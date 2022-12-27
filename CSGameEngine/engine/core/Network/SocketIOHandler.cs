using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class SocketIOHandler
{
    public Socket? client;

    public SocketIOHandler()
    {

    }

    public async void SendMessage(string message)
    {
        var messageBytes = Encoding.UTF8.GetBytes(message);
        _ = await client.SendAsync(messageBytes, SocketFlags.None);
    }

    public string GetResponse()
    {
        if (client is Socket)
        {
            byte[] buffer = new byte[4_096];
            int received = client.Receive(buffer, 0, 4096, SocketFlags.None);
            string response = Encoding.UTF8.GetString(buffer, 0, received);
            return response;
        }

        return "";
    }

    public async Task<int> ConnectToServer(string uri)
    {
        IPAddress ipAddress = IPAddress.Parse(uri);
        IPEndPoint ipEndPoint = new(ipAddress, 11_000);

        Socket client = new Socket(
    ipEndPoint.AddressFamily,
    SocketType.Stream,
    ProtocolType.Tcp);
        this.client = client;
        await client.ConnectAsync(ipEndPoint);
        return 0;
    }
}