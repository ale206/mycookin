using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class MyUserToken
    {
        public Guid UserId { get; set; }

        public Guid UserToken { get; set; }
        //public string ResultExecutionCode { get; set; }
        //public bool IsError { get; set; }
        //public string RejectionReason { get; set; }
    }
}