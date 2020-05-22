using System;
using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.LogAndMessage.Dto
{
    public class LogRow
    {
        public DateTime DateAndTime => DateTime.UtcNow;
        public string ErrorMessage { get; set; }
        public string ErrorMessageCode { get; set; }
        public LogLevel ErrorSeverity { get; set; }
        public string ErrorLine { get; set; }
        public string FileOrigin { get; set; }
        public string IdUser { get; set; }

        /// <summary>
        ///     Force disabling the sending of the email. Not necessary if use LogLevel.JustALog
        /// </summary>
        public bool DisableEmail { get; set; }
    }
}