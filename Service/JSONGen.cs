using System;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ServPkaLog
{
   
    public class JSONGen
    {
        public static string confName = "servicesetting.json";
        public static void GenerateConfig()
        {
            var config = new JObject
            {
                ["TimeOut"] = 0.5,
                ["Name"] = "ServiceLoggerPKA"
            };

            var root = new JObject
            {
                ["Config"] = config
            };

            string json = JsonConvert.SerializeObject(root, Formatting.Indented);
            File.WriteAllText(confName, json);
            
        }
    }
}
