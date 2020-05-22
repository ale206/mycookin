using System;

namespace TaechIdeas.Core.Core.Common.Dto
{
    public class MyException : Exception
    {
        public new string Message { get; }

        public string MessageCode { get; }

        public Exception Exception { get; }

        public MyException(string messageCode, long languageId = 1)
        {
            //_message = GlobalConnectorManager.GetMessage(messageCode, languageId);
            //_messageCode = messageCode;
        }

        public MyException(string messageCode, Exception innerException, long languageId = 1) : base(messageCode, innerException)
        {
            //_message = GlobalConnectorManager.GetMessage(messageCode, languageId);
            //_messageCode = messageCode;
            //_exception = innerException;
        }
    }
}