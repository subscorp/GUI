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
    class ConsoleClient : IClient
    {
        string logSource;
        string logName;
        string outputDir;
        string thumbnailSize;
        string[] handlers;
        string handler;
        int eventLogLength;


        public void HandleClient()
        {
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

                        outputDir = reader.ReadString();
                        Console.WriteLine("Output Directory: {0}", outputDir);

                        logSource = reader.ReadString();
                        Console.WriteLine("Source Name: {0}", logSource);

                        logName = reader.ReadString();
                        Console.WriteLine("Log Name: {0}", logName);

                        thumbnailSize = reader.ReadString();
                        Console.WriteLine("Thumbnail Size: {0}", thumbnailSize);

                        num = reader.ReadInt32();
                        Console.WriteLine();
                        Console.WriteLine("Handlers:");
                        for (int i = 0; i < num; i++)
                        {
                            handler = reader.ReadString();
                            Console.WriteLine(handler);
                        }
                        Console.WriteLine();
                    }

                    else
                    {
                        Console.WriteLine("Log:");

                        eventLogLength = reader.ReadInt32();
                        for (int k = 0; k < eventLogLength; k++)
                        {
                            logStr = reader.ReadString();
                            Console.WriteLine(logStr);
                        }
                        Console.WriteLine();
                    }
                }
            }
            client.Close();
        }
    }
}
