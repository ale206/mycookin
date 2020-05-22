using System.Collections.Generic;
using TaechIdeas.Core.Core.Network.Dto;

namespace TaechIdeas.Core.Core.Network
{
    public interface INetworkManager
    {
        SaveEmailToSendOutput SaveEmailToSend(SaveEmailToSendInput saveEmailToSendInput);
        IEnumerable<EmailsToSendOutput> EmailsToSend(EmailsToSendInput emailsToSendInput);
        SendEmailOutput SendEmail(SendEmailInput sendEmailInput);
        string GetCurrentPathUrl();
        string GetCurrentPageUrl();
        string GetCurrentPageName();
        void DestroyCookie();
        string GetIp();
    }
}