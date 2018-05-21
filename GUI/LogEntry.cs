using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{

    public class LogEntry
    {
        public string Message { get; set; }
        public string Type { get; set; }

        public string ToJSON()
        {
            JObject LogObj = new JObject();
            LogObj["Message"] = Message;
            LogObj["Type"] = Type;
            return LogObj.ToString();
        }

        public static LogEntry FromJSON(string str)
        {
            LogEntry logEntry = new LogEntry();
            JObject appConfigObj = JObject.Parse(str);
            logEntry.Message = (string)appConfigObj["Message"];
            logEntry.Type = (string)appConfigObj["Type"];
            return logEntry;
        }

    }
}
