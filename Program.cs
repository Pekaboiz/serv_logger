using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using serv_logger;
using System.ServiceProcess;

namespace ServiceLogger
{
    internal static class Program
    {
        static void Main()
        {

#if DEBUG
            JSONGen.GenerateConfig();
            var serv = new ServiceWorker();
            ServiceBase[] ServiceWorker;
            ServiceWorker = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServiceWorker);

#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
#endif

        }
    }
}
