using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class MyUser
    {
        public Guid IdUser { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public int? UserDomain { get; set; }
        public int? UserType { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? LastPasswordChange { get; set; }
        public DateTime? PasswordExpireOn { get; set; }
        public bool? ChangePasswordNextLogon { get; set; }
        public bool? ContractSigned { get; set; }
        public DateTime? BirthDate { get; set; }
        public string EMail { get; set; }
        public DateTime? MailConfirmedOn { get; set; }
        public string Mobile { get; set; }
        public string MobileConfirmationCode { get; set; }
        public DateTime? MobileConfirmedOn { get; set; }
        public int? IdLanguage { get; set; }
        public int? IdCity { get; set; }
        public Guid? IdProfilePhoto { get; set; }
        public bool? UserEnabled { get; set; }
        public bool? UserLocked { get; set; }
        public bool? MantainanceMode { get; set; }
        public int? IdSecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public DateTime? DateMembership { get; set; }
        public DateTime? AccountExpireOn { get; set; }
        public DateTime? LastLogon { get; set; }
        public DateTime? LastLogout { get; set; }
        public int Offset { get; set; }

        public DateTime? LastProfileUpdate { get; set; }
        public bool? UserIsOnLine { get; set; }
        public string LastIpAddress { get; set; }
        public int? IdVisibility { get; set; }
        public string ConfirmationCode { get; set; }
        public string SecurityQuestion { get; set; }
        public int? IdGender { get; set; }
        public string IdSecurityGroupList { get; set; }
        public DateTime? AccountDeletedOn { get; set; }

        //public bool? isProfessionalCook { get; set; }
        //public bool? cookInRestaurant { get; set; }
        //public bool? cookAtHome { get; set; }
        //public string cookDescription { get; set; }
        //public DateTime? cookMembership { get; set; }

        public string IdUserSocial { get; set; }

        public MyUserPropertyCompiled[] PropertyCompiled { get; set; }

        public string CompleteName { get; set; }
        private Guid Guid { get; set; }
        public bool? IsProfessionalCook { get; set; }
        public bool? CookInRestaurant { get; set; }
        public bool? CookAtHome { get; set; }
        public string CookDescription { get; set; }
        public DateTime? CookMembership { get; set; }

        //public MyUser()
        //{
        //}

        //public MyUser(Guid guid)
        //{
        //    this.Guid = guid;
        //}

        #region Operators

        //public static implicit operator MyUser(Guid guid)
        //{
        //    var user = new MyUser(guid);
        //    return user;
        //}

        //public static implicit operator Guid(MyUser user)
        //{
        //    var guid = new Guid();
        //    return user == null ? guid : user.IdUser;
        //}

        //public static bool operator ==(MyUser user1, MyUser user2)
        //{
        //    if ((Object) user1 == null)
        //    {
        //        user1 = new MyUser(new Guid());
        //    }
        //    if ((Object) user2 == null)
        //    {
        //        user2 = new MyUser(new Guid());
        //    }
        //    if ((Object) user1 == null || (Object) user2 == null)
        //    {
        //        return (Object) user1 == (Object) user2;
        //    }
        //    else if (string.IsNullOrEmpty(user1.EMail) || String.IsNullOrEmpty(user2.EMail))
        //    {
        //        return user1.IdUser == user2.IdUser;
        //    }

        //    return user1.EMail == user2.EMail;
        //}

        //public static bool operator !=(MyUser user1, MyUser user2)
        //{
        //    return !(user1 == user2);
        //}

        //public override bool Equals(System.Object obj)
        //{
        //    // If parameter is null return false.
        //    if (obj == null)
        //    {
        //        return false;
        //    }

        //    // If parameter cannot be cast to Recipe return false.
        //    var user = obj as MyUser;
        //    if ((System.Object) user == null)
        //    {
        //        return false;
        //    }

        //    // Return true if the fields match:
        //    return (EMail == user.EMail);
        //}

        //public bool Equals(MyUser user)
        //{
        //    // If parameter is null return false:
        //    if ((object) user == null)
        //    {
        //        return false;
        //    }

        //    // Return true if the fields match:
        //    return (EMail == user.EMail);
        //}

        //public override int GetHashCode()
        //{
        //    return IdUser.GetHashCode();
        //}

        #endregion
    }
}