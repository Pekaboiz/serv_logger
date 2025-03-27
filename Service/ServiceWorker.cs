using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ServPkaLog
{
    public class ServiceWorker : ServiceBase
    {
        private Timer _timer;
        private string _logSource;
        //private int _logIntervalMinutes = 1;
        private const string ConfigFilePath = "config.json";
        private EventLog _eventLog; 

        public ServiceWorker() 
        {
            
            //LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    string json = File.ReadAllText(ConfigFilePath);
                    JObject config = JObject.Parse(json);

                    // this.ServiceName = config["Config"]?["Name"]?.Value<string>() ?? "Undefined";
                    // _logIntervalMinutes = config["Config"]?["TimeOut"]?.Value<int>() ?? 1;
                    this.ServiceName = "ServPkaLogg";
                    //_logIntervalMinutes = 1;
                    _logSource = this.ServiceName;
                    
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("ServPkaLogg", $"Ошибка загрузки конфига: {ex.Message}", EventLogEntryType.Error);
            }
        }

        protected override void OnStart(string[] args)
        {
            //LoadConfiguration(); // Загружаем конфиг при старте
            // Путь к текущему исполняемому файлу
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            // Проверяем, существует ли источник "ServiceLogger"
            if (!EventLog.SourceExists("ServPkaLogg"))
            {
                // Создаем источник событий
                EventLog.CreateEventSource("ServPkaLogg", "Application");

                // После создания источника события, Windows автоматически использует исполняемый файл для ресурсов сообщений
                EventLog.WriteEntry("ServPkaLogg", $"Источник событий 'ServPkaSource' был создан {exePath}", EventLogEntryType.Information);
            }

            // Создаем объект EventLog
            _eventLog = new EventLog
            {
                Source = "ServPkaLogg",
                Log = "Application"
            };

            _eventLog.WriteEntry("Сервис запущен!", EventLogEntryType.Information);

            _timer = new Timer(60000); // Интервал из конфига
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        protected override void OnStop()
        {
            _timer.Stop();
            EventLog.WriteEntry("ServPkaLogg", "Сервис остановлен!", EventLogEntryType.Information);
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            EventLog.WriteEntry("ServPkaLogg", $"Сервис работает: {DateTime.Now}", EventLogEntryType.Information);
        }
    }
}
