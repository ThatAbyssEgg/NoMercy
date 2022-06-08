using System.Net.Sockets;

namespace NoMercyClient
{
    public class Program
    {
        static void Main()
        {
            Authorization.RequestAuthorisation();
            Console.ReadLine();
        }
    }
}