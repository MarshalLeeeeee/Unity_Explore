using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class NetworkUtil
{
    private static TcpClient client;
    private static NetworkStream stream;
    private static Thread thread;
    private static string serverIP = "127.0.0.1";
    private static int serverPort = 2000;
    private static byte[] buffer = new byte[1024];
    private static byte[] head = new byte[4];
    private static byte[] data = new byte[1024];

    public static string username;
    private static string password;

    public static bool connected = false;
    public static bool waitLogin = false;
    public static bool waitLogout = false;
    public static bool waitRead = false;
    public static bool waitWrite = false;
    public static bool isLogin = false;
    public static bool updateLogin = true;
    public static bool toShowLoginError = false;

    public static void login(string u, string p)
    {
        if (isLogin) return;
        try
        {
            connect();
            username = u;
            password = p;
            string msg = "login " + username + "$ " + password + "$";
            waitLogin = true;
            sendMsg(msg);
        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e.ToString());
            disconnect();
        }
    }

    public static void logout()
    {
        if (!isLogin) return;
        string msg = "logout";
        waitLogout = true;
        sendMsg(msg);
    }

    public static void connect()
    {
        if (!connected)
        {
            try
            {
                client = new TcpClient(serverIP, serverPort);
                stream = client.GetStream();
                thread = new Thread(receiveMsg);
                thread.IsBackground = true;
                thread.Start();
                connected = true;
            }
            catch (Exception e)
            {
                Debug.Log("Exception: " + e.ToString());
                disconnect();
            }
        }
    }

    public static void disconnect()
    {
        if (connected)
        {
            stream.Close();
            client.Close();
            connected = false;
        }
    }

    public static void setLoginState(bool f)
    {
        if (isLogin == f) updateLogin = false;
        else
        {
            isLogin = f;
            updateLogin = true;
        }
    }

    public static void sendMsg(string msg)
    {
        int len = msg.Length + 4;
        byte[] head = BitConverter.GetBytes(len);
        byte[] data = Encoding.ASCII.GetBytes(msg);
        byte[] socket = new byte[head.Length + data.Length];
        head.CopyTo(socket, 0);
        data.CopyTo(socket, head.Length);
        if (stream.CanWrite) stream.Write(socket, 0, socket.Length);
    }

    public static void receiveMsg()
    {
        if (!connected) return;
        while (connected && stream.CanRead)
        {
            try
            {
                int byteLen = stream.Read(buffer, 0, buffer.Length);
                if (byteLen >= 4)
                {
                    for (var i = 0; i < byteLen; i++)
                    {
                        if (i < 4) head[i] = buffer[i];
                        else data[i - 4] = buffer[i];
                    }

                    string receiveMsg = Encoding.ASCII.GetString(data, 0, byteLen - 4);
                    stream.Flush();
                    processMsg(receiveMsg);
                }
            }
            catch (Exception e)
            {
                Debug.Log("Exception: " + e.ToString());
            }
        }
    }

    private static void processMsg(string receiveMsg)
    {
        Debug.Log("receive msg: " + receiveMsg);
        checkLogin(receiveMsg);
        checkLogout(receiveMsg);
        checkRead(receiveMsg);
    }

    private static void checkLogin(string msg)
    {
        if (!waitLogin) return;
        if (msg == "login yes") setLoginState(true);
        else if (msg == "login no")
        {
            toShowLoginError = true;
            setLoginState(false);
            disconnect();
        }
        waitLogin = false;
    }

    private static void checkLogout(string msg)
    {
        if (!waitLogout) return;
        if (msg == "logout yes") setLoginState(false);
        waitLogout = false;
        if (!isLogin) disconnect();
    }

    private static void checkRead(string msg)
    {
        if (!waitRead) return;
        msg = msg.Substring(1, msg.Length - 2);
        var records = msg.Split(',');
        for (var i = 0; i < records.Length; i++)
        {
            TimeRecord.timeRecord[i] = float.Parse(records[i]);
        }
        waitRead = false;
    }
}
