using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class UserByEmailResult
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Username { get; set; }

        //public string PasswordHash { get; set; }
        //public DateTime? PasswordExpireOn { get; set; }
        //public bool? ChangePasswordNextLogon { get; set; }
        //public DateTime? DateOfBirth { get; set; }
        //public string Email { get; set; }
        public int? LanguageId { get; set; }

        //public int? CityId { get; set; }
        //public Guid? ProfilePhotoId { get; set; }
        //public bool? UserEnabled { get; set; }
        //public bool? UserLocked { get; set; }
        //public int? SecurityQuestionId { get; set; }
        //public DateTime? DateMembership { get; set; }
        //public DateTime? AccountExpireOn { get; set; }
        //public int Offset { get; set; }
        //public bool? UserIsOnLine { get; set; }
        //public string SecurityQuestion { get; set; }
        public int? GenderId { get; set; }

        //public DateTime? AccountDeletedOn { get; set; }
        public string ImageUrl { get; set; }
    }
}