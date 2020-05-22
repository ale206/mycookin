using System;
using System.Globalization;
using TaechIdeas.Core.Core.Common;

namespace TaechIdeas.Core.BusinessLogic.Common
{
    public class MyConvertManager : IMyConvertManager
    {
        public int ToInt32(string value, int defaultValue)
        {
            try
            {
                if (value.IndexOf(".", StringComparison.Ordinal) > -1)
                {
                    return Convert.ToInt32(value.Substring(0, value.IndexOf(".", StringComparison.Ordinal)));
                }

                if (value.IndexOf(",", StringComparison.Ordinal) > -1)
                {
                    return Convert.ToInt32(value.Substring(0, value.IndexOf(",", StringComparison.Ordinal)));
                }

                return Convert.ToInt32(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public int ToInt32(int? value, int defaultValue)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public int ToInt32(bool value, int defaultValue)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public double ToDouble(string value, double defaultValue)
        {
            try
            {
                var cultureString = "it-IT";
                // se "." americano, se "," italiano
                cultureString = value.LastIndexOf(".", StringComparison.Ordinal) > -1 ? "en-US" : "it-IT";
                IFormatProvider culture = new CultureInfo(cultureString);
                return double.Parse(value, culture);
            }
            catch
            {
                return defaultValue;
            }
        }

        public DateTime ToLocalTime(DateTime utcTime, int offset)
        {
            try
            {
                return utcTime.AddMinutes(-offset);
            }
            catch
            {
                return utcTime;
            }
        }

        public bool ToBoolean(string value, bool defaultValue)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                if (value == "on")
                {
                    return true;
                }

                if (value == "off")
                {
                    return false;
                }

                return defaultValue;
            }
        }

        public bool ToBoolean(bool? value, bool defaultValue)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public DateTime ToDateTime(string dateTime, CultureInfo culture, DateTime defaultValue)
        {
            DateTime convertedDate;
            try
            {
                convertedDate = Convert.ToDateTime(dateTime, culture);
            }
            catch (Exception)
            {
                convertedDate = defaultValue;
            }

            return convertedDate;
        }
    }
}