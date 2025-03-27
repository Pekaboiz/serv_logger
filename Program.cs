using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using serv_logger;
using System.ServiceProcess;

namespace ServPkaLog
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
                new ServiceWorker()
            };
            ServiceBase.Run(ServiceWorker);
#else
            //JSONGen.GenerateConfig();
            var serv = new ServiceWorker();
            ServiceBase[] ServiceWorker;
            ServiceWorker = new ServiceBase[]
            {
                new ServiceWorker()
            };
            ServiceBase.Run(ServiceWorker);
#endif

        }
    }
}
