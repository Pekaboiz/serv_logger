using System;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ServPkaLog
{
    /// <summary>
    /// Генератор json конфигуратора
    /// </summary>
    public class JSONGen
    {
        public static void GenerateConfig()
        {
            var config = new JObject
            {
                ["TimeOut"] = 0.5,
                ["Name"] = "ServiceLogger"
            };

            var root = new JObject
            {
                ["Config"] = config
            };

            string json = JsonConvert.SerializeObject(root, Formatting.Indented);
            File.WriteAllText("config.json", json);
        }
    }
}
