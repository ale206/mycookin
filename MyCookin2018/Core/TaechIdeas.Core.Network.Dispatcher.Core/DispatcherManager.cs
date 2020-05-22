using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TaechIdeas.Core.Core.Common.Enums;
using TaechIdeas.Core.Core.Configuration;
using TaechIdeas.Core.Core.LogAndMessage;
using TaechIdeas.Core.Core.LogAndMessage.Dto;
using TaechIdeas.Core.Core.Network;
using TaechIdeas.Core.Core.Network.Dto;

namespace TaechIdeas.Core.Network.Dispatcher.Core
{
    public class DispatcherManager
    {
        private readonly TimeSpan _lifetime;
        private readonly INetworkManager _networkManager;
        private readonly INetworkConfig _networkConfig;
        private readonly ILogManager _logManager;
        private readonly IMapper _mapper;
        private CancellationTokenSource _lifecycleToken = new CancellationTokenSource();

        public DispatcherManager(INetworkManager networkManager, INetworkConfig networkConfig, ILogManager logManager, IMapper mapper)
        {
            _networkManager = networkManager;
            _networkConfig = networkConfig;
            _logManager = logManager;
            _mapper = mapper;
            _lifetime = new TimeSpan(_networkConfig.EmailDispatcherLifetime);
        }

        private async void BeginDispatchingAsync()
        {
            try
            {
                Console.WriteLine($"Task Delay LifeTime: {_lifetime}");

                while (true)
                {
                    await Task.Delay(_lifetime, _lifecycleToken.Token);

                    StartSendEmails();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void StartSendEmails()
        {
            try
            {
                var emailsToSend = _networkManager.EmailsToSend(new EmailsToSendInput {MaxNumber = _networkConfig.MaxEmailsToSend});

                Console.WriteLine($"Max Email to send: {_networkConfig.MaxEmailsToSend}");

                var emailsToSendOutputs = emailsToSend as IList<EmailsToSendOutput> ?? emailsToSend.ToList();
                Console.WriteLine($"I am going to send {emailsToSendOutputs.Count()} emails");

                foreach (var email in emailsToSendOutputs)
                {
                    _networkManager.SendEmail(_mapper.Map<SendEmailInput>(email));

                    Console.WriteLine("Email Sent!");
                }
            }
            catch (Exception ex)
            {
                var logRowIn = new LogRowIn
                {
                    ErrorMessage = $"{ex.Message}{ex.InnerException ?? ex.InnerException}",
                    ErrorMessageCode = "NW-ER-9999",
                    ErrorSeverity = LogLevel.CriticalErrors,
                    FileOrigin = $"{MethodBase.GetCurrentMethod().Name} | {GetType().FullName}"
                };

                _logManager.WriteLog(logRowIn);

                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        #region Start/Stop Dispatching

        public void StopDispatching()
        {
            _lifecycleToken.Cancel();
        }

        public void StartDispatching()
        {
            _lifecycleToken = new CancellationTokenSource();

            BeginDispatchingAsync();
        }

        #endregion
    }
}