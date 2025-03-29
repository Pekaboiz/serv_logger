using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.ServiceProcess;
using System;

namespace ServPkaLog
{
    internal static class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            JSONGen.GenerateConfig();
#else
            if (args.Length > 0 && args[0] == "/genjson")
            {
                JSONGen.GenerateConfig();
                return;
            }
#endif
            var serv = new ServiceWorker();
            ServiceBase[] ServiceWorker;
            ServiceWorker = new ServiceBase[]
            {
               new ServiceWorker()
            };
            ServiceBase.Run(ServiceWorker);
        }
    }
}
