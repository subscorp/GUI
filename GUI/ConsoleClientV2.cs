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
            string logStr;

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
                    Console.WriteLine("please choose operation: enter 1 for settings or 2 for log");
                    int num = int.Parse(Console.ReadLine());
                    writer.Write(num);

                    if (num == 1)
                    {
                        Console.WriteLine("Settings:");
                        settingsStr = reader.ReadString();
                        settings = Settings.FromJSON(settingsStr);

                        Console.WriteLine("Output Directory: {0}", settings.OutputDir);

                        Console.WriteLine("Source Name: {0}", settings.LogSource);

                        Console.WriteLine("Log Name: {0}", settings.LogName);

                        Console.WriteLine("Thumbnail Size: {0}", settings.ThumbnailSize);


                        //               num = reader.ReadInt32();
                        //               Console.WriteLine();
                        Console.WriteLine("Handlers:");

                        //                for (int i = 0; i < num; i++)
                        //                {
                        //                    handler = reader.ReadString();
                        //                    Console.WriteLine(handler);
                        //                }
                        Console.WriteLine();
                    }

                    else
                    {
                        Console.WriteLine("Log:");
                        logStr = reader.ReadString();
                        Console.WriteLine(logStr);
                    }
                }
            }
            client.Close();
        }
    }
}
