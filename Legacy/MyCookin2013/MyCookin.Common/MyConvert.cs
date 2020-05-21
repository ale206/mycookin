using System;
using System.Collections.Generic;
using System.Text;

namespace MyCookin.Common
{
    public class MyConvert
    {
        public static int ToInt32(string value,int defaultValue)
        {
            try
            {
                if (value.IndexOf(".") > -1 )
                {
                    return Convert.ToInt32(value.Substring(0,value.IndexOf(".")));
                }
                else if(value.IndexOf(",") > -1)
                {
                    return Convert.ToInt32(value.Substring(0, value.IndexOf(",")));
                }
                else
                {
                    return Convert.ToInt32(value);
                }
            }
            catch
            {
                return defaultValue;
            }
        }

        public static int ToInt32(int? value, int defaultValue)
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

        public static int ToInt32(bool value, int defaultValue)
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

        public static double ToDouble(string value, double defaultValue)
        {
            try
            {
                string _cultureString = "it-IT";
                // se "." americano, se "," italiano
                if (value.LastIndexOf(".") > -1)
                {
                    _cultureString = "en-US";
                }
                else
                {
                    _cultureString = "it-IT";
                }
                System.IFormatProvider _culture = new System.Globalization.CultureInfo(_cultureString);
                return double.Parse(value, _culture);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static DateTime ToLocalTime(DateTime UtcTime, int Offset)
        {
            try
            {
                return UtcTime.AddMinutes(-Offset);
            }
            catch
            {
                return UtcTime;
            }
        }
		
		public static bool ToBoolean(string value, bool defaultValue)
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
        public static bool ToBoolean(bool? value, bool defaultValue)
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
    }
}
