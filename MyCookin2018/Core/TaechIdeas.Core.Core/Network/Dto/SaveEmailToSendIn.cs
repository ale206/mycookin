﻿namespace TaechIdeas.Core.Core.Network.Dto
{
    public class SaveEmailToSendIn
    {
        public string From { get; set; }

        public string To { get; set; }

        public string Cc { get; set; }

        public string Bcc { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public string HtmlFilePath { get; set; }
    }
}