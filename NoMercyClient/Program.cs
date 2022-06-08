using System.Net.Sockets;

namespace NoMercyClient
{
    public class Program
    {
        static void Main()
        {
            while (true)
            {
                Authorization.RequestAuthorisation();
                Console.ReadLine();
            }
        }
    }
}