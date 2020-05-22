using System.ServiceProcess;
using AutoMapper;
using TaechIdeas.Core.Core.Configuration;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.Network;
using TaechIdeas.Core.Network.Dispatcher.Core;

//using Ninject;

namespace TaechIdeas.Core.Network.Dispatcher
{
    internal partial class ExecutorService : ServiceBase
    {
        private static INetworkManager _networkManager;
        private static INetworkConfig _networkConfig;
        private static ILogManager _logManager;
        private readonly IMapper _mapper;

        public static readonly string DispatcherName = typeof(ExecutorService).ToString();
        private readonly DispatcherManager _networkDispatcherManager;

        public ExecutorService()
        {
            InitializeComponent();
            LoadKernel();
            _networkDispatcherManager = new DispatcherManager(_networkManager, _networkConfig, _logManager, _mapper);
        }

        protected override void OnStart(string[] args)
        {
            _networkDispatcherManager.StartDispatching();
        }

        protected override void OnStop()
        {
            _networkDispatcherManager.StopDispatching();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        /// <summary>
        ///     Load Ninject Kernel
        /// </summary>
        private static void LoadKernel()
        {
            //TODO: Use AutoFac

            //IKernel kernel = new StandardKernel();
            //kernel.Load(Assembly.GetExecutingAssembly());
            //_networkManager = kernel.Get<NetworkManager>();
            //_networkConfig = kernel.Get<NetworkConfig>();
            //_logManager = kernel.Get<LogManager>();
        }
    }
}