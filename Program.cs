using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace ServiceLogger
{
    internal static class Program
    {
        static void Main()
        {

#if DEBUG
            JSONGen.GenerateConfig();
            var serv = new ServiceWorker();

            serv.Start();

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
