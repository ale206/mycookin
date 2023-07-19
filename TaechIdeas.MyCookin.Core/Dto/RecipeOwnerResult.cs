using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class RecipeOwnerResult
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public bool? UserIsOnLine { get; set; }
        public int? GenderId { get; set; }
    }
}