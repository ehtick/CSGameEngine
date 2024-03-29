﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using RSG;

public class SocketHandler
{
    public static List<Socket> handlers = new List<Socket>();
    static Dictionary<string, PlayerObject> PlayerObjects = new Dictionary<string, PlayerObject>();

    public static List<List<bool>> LEVEL;

    static List<string> PlayerUsernames = new List<string>();

    public static int MAPSIZE = 1600;

    public static int MaximumPlayers = 4;
    public static int PlayerCount = 0;

    public static bool gameStarted = false;

    public static bool networkLock = false;

    public static List<Socket> workerThreadSocketList = new List<Socket>();
    public static List<HandshakeClient> workerThreadHandshakeList = new List<HandshakeClient>();

    static Dictionary<string, PlayerObject> GetPOBJListWithout(string without)
    {
        Dictionary<string, PlayerObject> copied = new Dictionary<string, PlayerObject>(PlayerObjects);
        copied.Remove(without);
        return copied;
    }

    public static async void SendMessage(string message, Socket handler)
    {
        try
        {
            var echoBytes = Encoding.UTF8.GetBytes(message);
            // Console.WriteLine("SENT => " + message);
            await handler.SendAsync(Compression.Compress(echoBytes), 0);
        }
        catch (Exception e)
        {
            throw new Exception();
        }

    }

    public static void SendMessageAll(string message)
    {
        foreach (Socket socket in handlers)
        {
            SendMessage(message, socket);
        }
    }

    public static void SendMessageAllExcept(string message, Socket handler)
    {
        foreach (Socket socket in handlers)
        {
            if (socket != handler)
            {
                SendMessage(message, socket);
            }
        }
    }

    public static async Task<string> GetResponse(Socket handler)
    {
        var buffer = new byte[2_048];
        try
        {
            var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
            byte[] decompressed = Compression.Decompress(buffer);
            var response = Encoding.UTF8.GetString(decompressed, 0, decompressed.Length);
            // Console.WriteLine("RECEIVED => " + response);
            return response;
        }
        catch (SocketException e)
        {
            return "TERROR";
        }
        catch (ObjectDisposedException e)
        {
            return "TERROR";
        }
    }

    public static string GetLocalIPAddress()
    {
        string localIP;
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 65530);
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            localIP = endPoint.Address.ToString();
        }
        return localIP;
    }

    [Obsolete]
    public static void Main()
    {
        string IP = GetLocalIPAddress();

        Console.WriteLine("Attempting server start on Local Ip Address: " + IP);

        IPAddress ipAddress = IPAddress.Parse(IP);
        IPEndPoint ipEndPoint = new(ipAddress, 11_000);

        using Socket listener = new(
            ipEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);

        listener.Bind(ipEndPoint);
        listener.Listen(100);

        Console.WriteLine("STARTED SOCKET SERVER");

        new Thread(WorkerThread).Start();

        GenerateLevel(listener);

        WaitForPlayers(listener);
    }

    public static void WaitForPlayers(Socket listener)
    {
        while (true)
        {
            Socket s = listener.Accept();

            Thread thr = new Thread(() => SocketHandler.PlayerThread(s));
            thr.Start();
        }
    }

    public static int GenerateLevel(Socket listener)
    {
        List<List<bool>> result = LevelGeneration.GenerateLevel(-(MAPSIZE / 2), MAPSIZE / 2, -(MAPSIZE / 2), MAPSIZE / 2);
        LEVEL = result;
        return 0;
    }

    private static void DisconnectPlayer(string username, Socket s)
    {
        int index = PlayerUsernames.IndexOf(username);

        if (username is string && PlayerObjects.ContainsKey(username))
        {
            PlayerObjects.Remove(username);
        }
        s.Close();
        PlayerCount--;
        PlayerUsernames.Remove(username);
        handlers.Remove(s);

        workerThreadHandshakeList.RemoveAt(index);
        workerThreadSocketList.RemoveAt(index);

        if (PlayerCount == 0)
        {
            gameStarted = false;
        }
    }

    public static async void WorkerThread()
    {
        while (true)
        {
            int index = 0;
            foreach (Socket s in workerThreadSocketList.ToList())
            {
                HandshakeClient handshake = workerThreadHandshakeList[index];
                try
                {
                    SendMessage(JsonSerializer.Serialize(PlayerObjects), s);
                }
                catch (Exception e)
                {
                    DisconnectPlayer(handshake.username, s);
                }

                string received = await GetResponse(s);
                if (received != "TERROR")
                {
                    try
                    {
                        Dictionary<string, PlayerObject>? pobjs = JsonSerializer.Deserialize<Dictionary<string, PlayerObject>>(received);

                        if (pobjs is Dictionary<string, PlayerObject>)
                        {
                            if (!networkLock)
                            {
                                networkLock = true;
                                PlayerObjects = pobjs;
                                networkLock = false;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        DisconnectPlayer(handshake.username, s);
                    }
                }
                else DisconnectPlayer(handshake.username, s);

                index++;
            }
        }
    }

    public static async void PlayerThread(Socket s)
    {
        bool gameStartedForPlayer = false;

        if (PlayerCount + 1 <= MaximumPlayers)
        {
            if (!gameStarted)
            {
                PlayerCount++;
                handlers.Add(s);

                int left = -800;
                int right = 800;
                int top = -800;
                int bottom = 800;
                int wallsize = 100;

                int rleft = (int)Math.Floor((double)(left + wallsize) / wallsize);
                int rright = (int)Math.Floor((double)(right) / wallsize);
                int rtop = (int)Math.Floor((double)(top + wallsize) / wallsize);
                int rbottom = (int)Math.Floor((double)(bottom - wallsize) / wallsize);

                HandshakeClient? handshake = JsonSerializer.Deserialize<HandshakeClient>(await GetResponse(s));

                if (PlayerUsernames.Contains(handshake.username))
                {
                    SendMessage(JsonSerializer.Serialize(new Handshake(false, null, HandshakeError.MAT_USR, false)), s);
                    s.Close();
                }
                else
                {
                    PlayerUsernames.Add(handshake.username);

                    string serialized = JsonSerializer.Serialize(new Handshake(true, new MapObject(rleft, rright, rtop, rbottom, MAPSIZE, LEVEL), null, PlayerCount == 1));
                    SendMessage(serialized, s);
                    bool isHost = PlayerCount == 1;

                    while (!gameStartedForPlayer)
                    {
                        string msg = await GetResponse(s);

                        if (msg != "TERROR")
                        {
                            if (msg == "request_players")
                            {
                                SendMessage(JsonSerializer.Serialize(PlayerUsernames), s);
                            }
                            else if (msg == "game_started")
                            {
                                SendMessage(JsonSerializer.Serialize(gameStarted), s);
                                gameStartedForPlayer = gameStarted;
                            }
                            else if (msg == "start_game")
                            {
                                gameStarted = true;
                                gameStartedForPlayer = true;
                            }
                        }
                        else DisconnectPlayer(handshake.username, s);
                    }

                    if (isHost)
                    {
                        string gamemodeResponse = await GetResponse(s);

                        Console.WriteLine(gamemodeResponse);

                        if (gamemodeResponse != "TERROR")
                        {
                            SendMessageAllExcept(gamemodeResponse, s);
                        }
                        else DisconnectPlayer(handshake.username, s);
                    }

                    if (!isHost)
                    {
                        string gamemodecheck = await GetResponse(s);

                        if (gamemodecheck != "received_gamemode")
                            throw new Exception();
                    }

                    s.ReceiveTimeout = 500;

                    workerThreadSocketList.Add(s);
                    workerThreadHandshakeList.Add(handshake);
                }
            }
            else
            {
                SendMessage(JsonSerializer.Serialize<Handshake>(new Handshake(false, null, HandshakeError.GAME_STARTED, false)), s);
                s.Close();
            }
        }
        else
        {
            SendMessage(JsonSerializer.Serialize<Handshake>(new Handshake(false, null, HandshakeError.SER_MAX_CAP, false), new JsonSerializerOptions() { WriteIndented = true }), s);
            s.Close();
        }
    }
}