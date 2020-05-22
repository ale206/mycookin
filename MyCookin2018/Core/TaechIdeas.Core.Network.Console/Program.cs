using System;
using AutoMapper;
using TaechIdeas.Core.Core.Configuration;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.Network;
using TaechIdeas.Core.Network.Dispatcher.Core;

//using Ninject;

namespace TaechIdeas.Core.Network.Console
{
    internal static class Program
    {
        private static DispatcherManager _dispatcherManager;
        private static INetworkManager _networkManager;
        private static INetworkConfig _networkConfig;
        private static ILogManager _logManager;
        private static IMapper _mapper;

        private static void Main()
        {
            try
            {
                LoadKernel();

                System.Console.Title = "Network Dispatcher Console";

                //System.Console.WriteLine("Press enter to init dispatcher:");
                //System.Console.ReadLine();

                _dispatcherManager = new DispatcherManager(_networkManager, _networkConfig, _logManager, _mapper);

                System.Console.WriteLine("Commands Available: [start] | [stop] | [exit] ");

                System.Console.WriteLine("Starting Network Dispatcher...");
                _dispatcherManager.StartDispatching();
                System.Console.WriteLine("Network Dispatcher Started");

                while (true)
                {
                    var str = System.Console.ReadLine();

                    switch (str)
                    {
                        case "exit":
                            Environment.Exit(0);
                            break;
                        case "stop":
                            System.Console.WriteLine("Stopping Network Dispatcher...");
                            _dispatcherManager.StopDispatching();
                            System.Console.WriteLine("Network Dispatcher Stopped");
                            break;
                        case "start":
                            System.Console.WriteLine("Starting Network Dispatcher...");
                            _dispatcherManager.StartDispatching();
                            System.Console.WriteLine("Network Dispatcher Started");
                            break;
                        default:
                            System.Console.WriteLine("Command not recognized!");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Error: {ex.Message}");
                System.Console.ReadLine();
            }
        }

        /// <summary>
        ///     Load Ninject Kernel
        /// </summary>
        private static void LoadKernel()
        {
            //AutoFac
            //var container = AppBuilderExtensions.IoCSetup();

            //_networkManager = container.Resolve<INetworkManager>();
            //_networkConfig = container.Resolve<INetworkConfig>();
            //_logManager = container.Resolve<ILogManager>();
            //_mapper = container.Resolve<IMapper>();
        }
    }
}