using System.Collections.Generic;
using TaechIdeas.Core.Core.Network.Dto;

namespace TaechIdeas.Core.Core.Network
{
    public interface INetworkRepository
    {
        SaveEmailToSendOut SaveEmailToSend(SaveEmailToSendIn saveEmailToSendIn);
        IEnumerable<EmailsToSendOut> EmailsToSend(EmailsToSendIn emailsToSendIn);
        UpdateEmailStatusOut UpdateEmailStatus(UpdateEmailStatusIn updateEmailStatusIn);
    }
}