using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.User.Dto
{
    public class MyUserProperty
    {
        public int IdProperty { get; set; }
        public MyUserPropertyTypes PropertyType { get; set; }
        public MyUserPropertyCategory PropertyCategory { get; set; }
        public bool Enabled { get; set; }
        public bool Mandatory { get; set; }
        public int PropertySortOrder { get; set; }
        public int IdLanguage { get; set; }
        public string Property { get; set; }
        public string PropertyToolTip { get; set; }
        public string Description { get; set; }
        public MyUserPropertyAllowedValue[] PropertyAllowedValues { get; set; }
    }
}