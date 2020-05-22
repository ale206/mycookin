using System;
using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class NewUserIn
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool ContractSigned { get; set; }
        public int LanguageId { get; set; }
        public string Ip { get; set; }
        public string Mobile { get; set; }
        public int? CityId { get; set; }
        public int? GenderId { get; set; }
        public int Offset { get; set; }
        public MyWebsite WebsiteId { get; set; }
    }
}