using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ServiceLogger
{
    public class ServiceWorker : ServiceBase
    {
        private Timer _timer;
        private string _logSource = "ServiceLogger";
        private int _logIntervalMinutes = 1;
        private const string ConfigFilePath = "config.json";

        public ServiceWorker() 
        {
            this.ServiceName = "ServiceLogger";
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    string json = File.ReadAllText(ConfigFilePath);
                    JObject config = JObject.Parse(json);

                    _logIntervalMinutes = config["timeout"]?.Value<int>() ?? 1;
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("MyLoggingService", $"Ошибка загрузки конфига: {ex.Message}", EventLogEntryType.Error);
            }
        }

       // protected override void OnStart(string[] args)
        public void Start()
        {
            LoadConfiguration(); // Загружаем конфиг при старте

            if (!EventLog.SourceExists(_logSource))
            {
                EventLog.CreateEventSource(_logSource, "Application");
            }

            _timer = new Timer(_logIntervalMinutes * 60000); // Интервал из конфига
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();

            EventLog.WriteEntry(_logSource, "Сервис запущен!", EventLogEntryType.Information);
        }

        protected override void OnStop()
        {
            _timer.Stop();
            EventLog.WriteEntry(_logSource, "Сервис остановлен!", EventLogEntryType.Information);
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            EventLog.WriteEntry(_logSource, $"Сервис работает: {DateTime.Now}", EventLogEntryType.Information);
        }
    }
}
