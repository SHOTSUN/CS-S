using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        

        static void Main(string[] args)
        {
            int port = 8888;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            Server server = new Server(port, localAddr);

            server.run();

        }
    }
}

