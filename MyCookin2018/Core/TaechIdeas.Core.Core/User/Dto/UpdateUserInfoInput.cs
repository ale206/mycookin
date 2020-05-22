using System;
using TaechIdeas.Core.Core.Token.Dto;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class UpdateUserInfoInput : TokenRequiredInput
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public int LanguageId { get; set; }

        public int? CityId { get; set; }

        public Guid? ProfilePhotoId { get; set; }

        public int? SecurityQuestionId { get; set; }

        public string SecurityAnswer { get; set; }

        public int Offset { get; set; }

        public string LastIpAddress { get; set; }

        public int? GenderId { get; set; }
    }
}