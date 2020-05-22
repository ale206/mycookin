using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class LogoutUserRequest
    {
        [ApiMember(Name = "UserToken", DataType = "guid", IsRequired = true)]
        [Required(ErrorMessage = "UserToken Required")]
        public Guid UserToken { get; set; }
    }
}