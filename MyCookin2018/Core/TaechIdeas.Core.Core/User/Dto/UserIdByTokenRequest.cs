using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class UserIdByTokenRequest
    {
        [ApiMember(Name = "Token", DataType = "guid", IsRequired = true)]
        [Required(ErrorMessage = "Token Required")]
        public Guid Token { get; set; }
    }
}