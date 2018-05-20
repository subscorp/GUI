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
    class ConsoleClientV2 : IClient
    {
        public void HandleClient()
        {
            Settings settings = new Settings();
            string settingsStr;
            int numHandlers;

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("You are connected\n");

            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                while (true)
                {
                    Console.WriteLine("please choose oper ation: enter 1 for settings or 2 for log");
                    int num = int.Parse(Console.ReadLine());
                    writer.Write(num);

                    if (num == 1)
                    {
                        Console.WriteLine("Settings:");
                        settingsStr = reader.ReadString();
                        settings = Settings.FromJSON(settingsStr);
                        numHandlers = settings.Handlers.Length;

                        Console.WriteLine("Output Directory: {0}", settings.OutputDir);

                        Console.WriteLine("Source Name: {0}", settings.LogSource);

                        Console.WriteLine("Log Name: {0}", settings.LogName);

                        Console.WriteLine("Thumbnail Size: {0}", settings.ThumbnailSize);

                        Console.WriteLine("Handlers:");

                        for (int i = 0; i < numHandlers; i++)
                            Console.WriteLine(settings.Handlers[i]);
                    }

                    else
                    {
                        Console.WriteLine(reader.ReadString());
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
            }
            client.Close();
        }
    }
}
