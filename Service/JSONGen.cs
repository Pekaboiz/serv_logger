using System;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace ServiceLogger
{
    /// <summary>
    /// Генератор json конфигуратора
    /// </summary>
    public class JSONGen
    {
        public static void GenerateConfig()
        {
            var config = new
            {
                timeout = 600
            };

            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText("config.json", json);
        }
    }
}
