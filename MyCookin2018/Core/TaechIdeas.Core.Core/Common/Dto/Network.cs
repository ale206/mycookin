namespace TaechIdeas.Core.Core.Common.Dto
{
    public class Network
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string ClientSmtp { get; set; }
        public int ClientSmtpPort { get; set; }
        public string SmtpServerUsn { get; set; }
        public string SmtpServerPsw { get; set; }
        public string HtmlFilePath { get; set; }
    }
}