using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GUI
{
    class Student
    {
        //        string name;
        //        int id;

        public string Name { get; set; }
        public int Id { get; set; }
        public int Grade { get; set; }

        public Student()
        {
        }
        public Student(string name, int id, int grade)
        {
            Name = name;
            Id = id;
            Grade = grade;
        }

        public static implicit operator JObject(Student v)
        {
            throw new NotImplementedException();
        }

        public string ToJSON()
        {
            JObject studentObject = new JObject();
            studentObject["Name"] = Name;
            studentObject["Id"] = Id;
            studentObject["Grade"] = Grade;
            return studentObject.ToString();
        }

        public static Student FromJSON(string str)
        {
            Student student = new Student();
            JObject studentObject = JObject.Parse(str);
            student.Name = (string)studentObject["Name"];
            student.Id = (int)studentObject["Id"];
            student.Grade = (int)studentObject["Grade"];
            return student;
        }
    }
}
