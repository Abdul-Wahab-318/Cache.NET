using System;
using Server.TcpServer;

internal class Program
{
    private static void Main(string[] args)
    {
        TCPServer server = new TCPServer();
        server.Start();

        Console.ReadKey();
    }
}