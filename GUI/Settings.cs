using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;
using Newtonsoft.Json.Linq;

namespace GUI
{
    class Settings
    {
        public string LogSource { get; set; }
        public string LogName { get; set; }
        public string[] Handlers { get; set; }
        //int handlersLength = handlers.Length;
        //string handler;
        public string OutputDir { get; set; }
        public string ThumbnailSize { get; set; }

        public Settings()
        {
        }

        public string ToJSON()
        {
            JObject appConfigObj = new JObject();
            appConfigObj["LogSource"] = LogSource;
            appConfigObj["LogName"] = LogName;
            appConfigObj["OutputDir"] = OutputDir;
            appConfigObj["ThumbnailSize"] = ThumbnailSize;
            //   appConfigObj["Handlers"] = Handlers;
            return appConfigObj.ToString();
        }

        public static Settings FromJSON(string str)
        {
            Settings settings = new Settings();
            JObject appConfigObj = JObject.Parse(str);
            settings.LogSource = (string)appConfigObj["LogSource"];
            settings.LogName = (string)appConfigObj["LogName"];
            settings.OutputDir = (string)appConfigObj["OutputDir"];
            settings.ThumbnailSize = (string)appConfigObj["ThumbnailSize"];
            //settings.Handlers = ()appConfigObj["Handlers"];
            JArray handlers = (JArray)appConfigObj["Handlers"];
            settings.Handlers = handlers.Select(jv => (string)jv).ToArray();
            return settings;
        }

    }
}
