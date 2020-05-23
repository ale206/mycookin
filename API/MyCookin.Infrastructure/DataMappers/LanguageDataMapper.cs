using System.ComponentModel.DataAnnotations.Schema;
using MyCookin.Domain.Entities;

namespace MyCookin.Infrastructure.DataMappers
{
    public class LanguageDataMapper
    {
        [Column("id")] public long Id { get; set; }

        [Column("name")] public string Name { get; set; }

        [Column("code")] public string Code { get; set; }

        [Column("is_enabled")] public bool IsEnabled { get; set; }

        internal Language CovertToEntity()
        {
            return new Language
            {
                Id = Id,
                Name = Name,
                Code = Code,
                IsEnabled = IsEnabled
            };
        }
    }
}