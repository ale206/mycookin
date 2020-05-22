namespace TaechIdeas.Core.Core.User.Dto
{
    public class MyUserPropertyCategory
    {
        public int IdPropertyCategory { get; set; }
        public bool Enabled { get; set; }
        public int IdLanguage { get; set; }

        public string PropertyCategory { get; set; }

        public string PropertyCategoryToolTip { get; set; }
    }
}