using System;

namespace TaechIdeas.MyCookin.Core.Dto
{
    public class Beverage
    {
        //TODO: Check!

        public Beverage(Guid idBeverage)
        {
            IdBeverage = idBeverage;
        }

        private Guid IdBeverage { get; }

        #region Operators

        public static implicit operator Beverage(Guid guid)
        {
            var beverage = new Beverage(guid);
            return beverage;
        }

        public static implicit operator Guid(Beverage beverage)
        {
            var guid = new Guid();
            if (beverage == null) return guid;

            return beverage.IdBeverage;
        }

        public static bool operator ==(Beverage beverage1, Beverage beverage2)
        {
            if ((object) beverage1 == null) beverage1 = new Beverage(new Guid());

            if ((object) beverage2 == null) beverage2 = new Beverage(new Guid());

            if ((object) beverage1 == null || (object) beverage2 == null) return beverage1 == (object) beverage2;

            return beverage1.IdBeverage == beverage2.IdBeverage;
        }

        public static bool operator !=(Beverage beverage1, Beverage beverage2)
        {
            return !(beverage1 == beverage2);
        }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null) return false;

            // If parameter cannot be cast to Recipe return false.
            var beverage = obj as Beverage;
            if ((object) beverage == null) return false;

            // Return true if the fields match:
            return IdBeverage == beverage.IdBeverage;
        }

        public bool Equals(Beverage beverage)
        {
            // If parameter is null return false:
            if ((object) beverage == null) return false;

            // Return true if the fields match:
            return IdBeverage == beverage.IdBeverage;
        }

        public override int GetHashCode()
        {
            return IdBeverage.GetHashCode();
        }

        #endregion
    }
}