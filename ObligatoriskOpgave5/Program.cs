using System;

namespace ObligatoriskOpgave5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Server";
            Server server = new Server();
            server.Start();
        }
    }
}
