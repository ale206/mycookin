using System;
using System.ComponentModel;
using System.Globalization;

namespace TaechIdeas.MyCookin.API.Utils
{
    //TODO: Evaluate if moving to a separate Global project
    /// <summary>
    ///     Date Utils
    /// </summary>
    public class DateUtils
    {
        /// <summary>
        ///     Convert DateTime to UTC
        /// </summary>
        public sealed class UtcDateTimeConverter : DateTimeConverter
        {
            /// <inheritdoc />
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (value == null)
                {
                    return null;
                }

                // if it's date-only, the assume that the Kind is UTC (put a 00:00:00Z time there)
                var convertedDate = value.ToString().Length <= 10 // accepts from 2018-1-1 to 2018-01-01 formats..
                    ? base.ConvertFrom(context, culture, value + " 00:00:00Z")
                    : base.ConvertFrom(context, culture, value);

                //Return converted date to UTC or NULL
                return ((DateTime?) convertedDate)?.ToUniversalTime();
            }
        }
    }
}