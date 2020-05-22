using System;

namespace TaechIdeas.Core.Core.Network.Dto
{
    public class EmailsToSendOut
    {
        public Guid Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Cc { get; set; }
        public string Bcc { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public string HtmlFilePath { get; set; }
    }
}