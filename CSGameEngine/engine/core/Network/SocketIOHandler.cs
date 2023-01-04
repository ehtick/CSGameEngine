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

        if (Configuration.config.showNetworkMessages)
            Console.WriteLine("SENT => " + message);

        _ = await client.SendAsync(Compression.Compress(messageBytes), SocketFlags.None);
    }

    public string GetResponse()
    {
        if (client is Socket)
        {
            byte[] buffer = new byte[4_096];
            var received = client.Receive(buffer, SocketFlags.None);
            byte[] decompressed = Compression.Decompress(buffer);
            var response = Encoding.UTF8.GetString(decompressed, 0, decompressed.Length);

            if (Configuration.config.showNetworkMessages)
                Console.WriteLine("RECEIVED => " + response);

            return response;
        }

        return "";
    }

    public int ConnectToServer(string uri)
    {
        IPAddress ipAddress = IPAddress.Parse(uri);
        IPEndPoint ipEndPoint = new(ipAddress, 11_000);

        Socket client = new Socket(
    ipEndPoint.AddressFamily,
    SocketType.Stream,
    ProtocolType.Tcp);
        this.client = client;

        IAsyncResult result = client.BeginConnect(ipEndPoint, null, null);
        bool success = result.AsyncWaitHandle.WaitOne(5000, true);

        if (client.Connected)
        {
            client.EndConnect(result);
        }
        else
        {
            client.Close();
            throw new SocketException();
        }

        return 0;
    }
}