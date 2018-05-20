using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace GUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IClient client = new ConsoleClientV2();
            client.HandleClient();
        }
    }
}
