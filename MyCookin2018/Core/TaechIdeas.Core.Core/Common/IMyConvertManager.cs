using System;
using System.Globalization;

namespace TaechIdeas.Core.Core.Common
{
    public interface IMyConvertManager
    {
        int ToInt32(string value, int defaultValue);
        int ToInt32(int? value, int defaultValue);
        int ToInt32(bool value, int defaultValue);
        double ToDouble(string value, double defaultValue);
        DateTime ToLocalTime(DateTime utcTime, int offset);
        bool ToBoolean(string value, bool defaultValue);
        bool ToBoolean(bool? value, bool defaultValue);
        DateTime ToDateTime(string dateTime, CultureInfo culture, DateTime defaultValue);
    }
}