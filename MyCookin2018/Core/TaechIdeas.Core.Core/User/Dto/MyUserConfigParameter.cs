using System;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class MyUserConfigParameter
    {
        public Guid IdUserConfigurationParameter { get; set; }
        public string ConfigurationName { get; set; }
        public string ConfigurationValue { get; set; }
        public string ConfigurationNote { get; set; }
    }
}