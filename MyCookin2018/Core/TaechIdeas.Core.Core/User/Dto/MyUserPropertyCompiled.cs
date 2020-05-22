using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class MyUserPropertyCompiled
    {
        public Guid IdUserInfoPropertyCompiled { get; set; }
        public MyUser User { get; set; }
        public int IdLanguage { get; set; }
        public MyUserProperty Property { get; set; }
        public MyUserPropertyAllowedValue[] AllowedValue { get; set; }
        public string PropertyValue { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}