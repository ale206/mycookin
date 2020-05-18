namespace MyCookin.Domain.Entities
{
    public class Language
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Enabled { get; set; }
    }
}