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

    public static int MAPSIZE = 1600;

    static Dictionary<string, PlayerObject> GetPOBJListWithout(string without)
    {
        Dictionary<string, PlayerObject> copied = new Dictionary<string, PlayerObject>(PlayerObjects);
        copied.Remove(without);
        return copied;
    }

    public static async void SendMessage(string message, Socket handler)
    {
        var echoBytes = Encoding.UTF8.GetBytes(message);
        await handler.SendAsync(echoBytes, 0);
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
        var buffer = new byte[4_096];
        try
        {
            var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
            var response = Encoding.UTF8.GetString(buffer, 0, received);
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

    public static void Main()
    {
        IPAddress ipAddress = IPAddress.Parse("192.168.203.234");
        IPEndPoint ipEndPoint = new(ipAddress, 11_000);

        using Socket listener = new(
            ipEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);

        listener.Bind(ipEndPoint);
        listener.Listen(100);

        Console.WriteLine("STARTED SOCKET SERVER");

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

    public static async void PlayerThread(Socket s)
    {
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

        string serialized = JsonSerializer.Serialize(new MapObject(rleft, rright, rtop, rbottom, MAPSIZE, LEVEL));
        SendMessage(serialized, s);

        string? username = null;

        s.ReceiveTimeout = 500;

        while (true)
        {
            string received = await GetResponse(s);
            if (received != "TERROR")
            {
                try
                {
                    PlayerObject? pobj = JsonSerializer.Deserialize<PlayerObject>(received);

                    if (pobj is PlayerObject)
                    {
                        PlayerObjects[pobj.username] = pobj;
                        username = pobj.username;
                    }

                    SendMessage(JsonSerializer.Serialize(GetPOBJListWithout(pobj.username)), s);
                }
                catch (Exception e)
                {
                    if (username is string && PlayerObjects.ContainsKey(username))
                    {
                        PlayerObjects.Remove(username);
                        s.Close();
                    }
                }
            }
            else
            {
                if (username is string && PlayerObjects.ContainsKey(username))
                {
                    PlayerObjects.Remove(username);
                    s.Close();
                }
            }
        }
    }
}