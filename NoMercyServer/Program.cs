using System.Net.Sockets;

namespace NoMercyServer
{
    public class Program
    {
        static void Main()
        {
            SocketServer.StartListening();
        }
    }
}