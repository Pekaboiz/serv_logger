using System;
using System.ComponentModel;
using System.Configuration.Install;
using sp = System.ServiceProcess;

namespace ServPkaLog
{
    [RunInstaller(true)]
    public class ServiceInstaller : Installer
    {
        public ServiceInstaller() 
        {
            sp.ServiceProcessInstaller procInstaller = new sp.ServiceProcessInstaller();
            sp.ServiceInstaller serviceInstaller = new sp.ServiceInstaller();

            procInstaller.Account = sp.ServiceAccount.LocalSystem;

            serviceInstaller.ServiceName = "ServiceLoggerPKA";
            serviceInstaller.DisplayName = "ServiceLoggerPKA";
            serviceInstaller.StartType = sp.ServiceStartMode.Automatic;

            Installers.Add(procInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
