using System;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

namespace TaechIdeas.Core.Network.Dispatcher
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                switch (string.Concat(args))
                {
                    case "i":
                    case "install":
                        ManagedInstallerClass.InstallHelper(new[] {Assembly.GetExecutingAssembly().Location});
                        break;

                    case "u":
                    case "uninstall":
                        ManagedInstallerClass.InstallHelper(new[] {"/u", Assembly.GetExecutingAssembly().Location});
                        break;
                    default:
                        throw new Exception("Command not supported!");
                }
            }
            else
            {
                var servicesToRun = new ServiceBase[]
                {
                    new ExecutorService()
                };

                ServiceBase.Run(servicesToRun);
            }
        }
    }
}