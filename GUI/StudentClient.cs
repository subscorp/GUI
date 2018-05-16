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
    class StudentClient : IClient
    {
        public void HandleClient()
        {
            Student student = new Student("ori", 5, 100);
            string studentStr = student.ToJSON();

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("You are connected");
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                while (true)
                {
                    Console.WriteLine("please enter student name");
                    student.Name = Console.ReadLine();
                    Console.WriteLine("please enter student id");
                    student.Id = int.Parse(Console.ReadLine());
                    Console.WriteLine("please enter student grade");
                    student.Grade = int.Parse(Console.ReadLine());
                    studentStr = student.ToJSON();
                    writer.Write(studentStr);
                    if (student.Name.Equals("exit"))
                        break;
                    int result = reader.ReadInt32();
                    Console.WriteLine("the server returned the result: {0}", result);
                    if (result == 1)
                        Console.WriteLine("the student passed!");
                    else
                        Console.WriteLine("the student failed!");
                }
            }
            client.Close();
        }
    }
}
