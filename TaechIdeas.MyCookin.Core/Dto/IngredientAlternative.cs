using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class IngredientAlternative
    {
        public Guid IdIngredientAlternative;
        public Guid IngredientMain;
        public Guid IngredientSlave;
        public Guid AddedByUser;
        public DateTime AddedOn;
        public Guid CheckedBy;
        public DateTime? CheckedOn;
        public bool IsChecked;
    }
}