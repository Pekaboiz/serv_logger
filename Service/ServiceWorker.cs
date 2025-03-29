using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Hosting;

namespace ServPkaLog
{
    public class ServiceWorker : ServiceBase
    {
        private Timer _timer;
        private float minute = 1;
        private readonly string configPath = string.Empty;
       
        public ServiceWorker()
        {
            string configPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + $@"\Config\{JSONGen.confName}";
            
            if (File.Exists(configPath))
            {
                LoadConfiguration();
            }
            else
            {
                throw new FileNotFoundException($"Configuration file not found at {configPath}");
            }
        }

        private void LoadConfiguration()
        {
            string json = File.ReadAllText(configPath);
            JObject config = JObject.Parse(json);

            this.ServiceName = config["Config"]?["Name"]?.Value<string>() ?? "ServiceLoggerPKA";
            minute = config["Config"]?["TimeOut"]?.Value<float>() ?? 1;
        }

        protected override void OnStart(string[] args)
        {
            if (this.ServiceName.Length != 0) return;
           
            try
            {
                if (!EventLog.SourceExists(this.ServiceName))
                {
                    EventLog.CreateEventSource(this.ServiceName, "Application");
                }

                EventLog.WriteEntry("Service is starting.");

                _timer = new Timer(minute * 60000); 
                _timer.Elapsed += OnTimerElapsed;
                _timer.Start();

                EventLog.WriteEntry("Service started.");
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Error starting service: " + ex.Message, EventLogEntryType.Error);
            }
        }

        protected override void OnStop()
        {
            _timer.Stop();
            EventLog.WriteEntry("Service is stopped!", EventLogEntryType.Information);
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            EventLog.WriteEntry($"Service is working. Massages every {minute} min", EventLogEntryType.Information);
        }
    }
}
