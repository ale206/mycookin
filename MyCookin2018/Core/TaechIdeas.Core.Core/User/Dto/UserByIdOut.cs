using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class UserByIdOut
    {
        public Guid IdUser { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? PasswordExpireOn { get; set; }
        public bool? ChangePasswordNextLogon { get; set; }
        public DateTime? BirthDate { get; set; }
        public string EMail { get; set; }
        public int? IdLanguage { get; set; }
        public int? IdCity { get; set; }
        public Guid? IdProfilePhoto { get; set; }
        public bool? UserEnabled { get; set; }
        public bool? UserLocked { get; set; }
        public int? IdSecurityQuestion { get; set; }
        public DateTime? DateMembership { get; set; }
        public DateTime? AccountExpireOn { get; set; }
        public int Offset { get; set; }
        public bool? UserIsOnLine { get; set; }
        public string SecurityQuestion { get; set; }
        public int? IdGender { get; set; }
        public DateTime? AccountDeletedOn { get; set; }
        public DateTime? LastLogon { get; set; }
        public DateTime? LastLogout { get; set; }
        public string Mobile { get; set; }

        public DateTime? MailConfirmedOn { get; set; }
        public string MobileConfirmationCode { get; set; }
        public DateTime? MobileConfirmedOn { get; set; }
        public bool? ContractSigned { get; set; }
        public string SecurityAnswer { get; set; }
        public bool? MantainanceMode { get; set; }
        public DateTime? LastProfileUpdate { get; set; }
        public int? UserDomain { get; set; }
        public int? UserType { get; set; }
        public DateTime? LastPasswordChange { get; set; }
        public string IdSecurityGroupList { get; set; }
        public string IdUserSocial { get; set; }
        public string LastIpAddress { get; set; }
        public int? IdVisibility { get; set; }
        public string ConfirmationCode { get; set; }
        public string CompleteName { get; set; }
        public bool? IsProfessionalCook { get; set; }
        public bool? CookInRestaurant { get; set; }
        public bool? CookAtHome { get; set; }
        public string CookDescription { get; set; }
        public DateTime? CookMembership { get; set; }
    }
}