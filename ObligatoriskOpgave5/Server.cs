using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ModelLib;
using Newtonsoft.Json;

namespace ObligatoriskOpgave5
{
    class Server
    {
        private TcpListener server;
        private static List<Book> books;
        public void Start()
        {
            server = new TcpListener(IPAddress.Loopback, 4646);
            server.Start();

            books = new List<Book>()
            {
                new Book("The hunt for cake", "Hungry Chef", 124, "1337420666123"),
                new Book("The hunt for cake2", "Hungry Chef", 412, "1392496583913"),
                new Book("The Bible", "Jesus", 800, "8270836271932"),
                new Book("Harry Potter", "JK. Rowling", 901, "5463746590215")
            };

            bool serverRunning = true;
            while (serverRunning)
            {
                TcpClient socket = server.AcceptTcpClient();

                Task.Run(() =>
                {
                    TcpClient tempSocket = socket;
                    DoClient(tempSocket);
                });
            }
        }

        private void DoClient(TcpClient tempSocket)
        {
            bool doClientRunning = true;
            using (tempSocket)
            using (StreamReader sr = new StreamReader(tempSocket.GetStream(), Encoding.GetEncoding("ISO-8859-1")))
            using (StreamWriter sw = new StreamWriter(tempSocket.GetStream(), Encoding.GetEncoding("ISO-8859-1")))
            {
                while (doClientRunning)
                {
                    string methodCall = sr.ReadLine();
                    string isbn13 = sr.ReadLine();

                    string returnString;

                    if (methodCall != null)
                    {
                        switch (methodCall.ToLower())
                        {
                            case "hentalle":
                                returnString = JsonConvert.SerializeObject(books);
                                sw.WriteLine(returnString);
                                break;
                            case "hent":
                                Book rbook = books.Find(b => b.Isbn13 == isbn13);
                                string JsonString = JsonConvert.SerializeObject(rbook);
                                sw.WriteLine(JsonString);
                                break;
                            case "gem":
                                books.Add(JsonConvert.DeserializeObject<Book>(isbn13));
                                break;
                            case "stop":
                                doClientRunning = false;
                                break;
                        }
                    }
                    sw.Flush();
                }
            }
        }
    }
}
