using System;
using System.Collections.Generic;
using TaechIdeas.Core.Core.Common.Dto;
using TaechIdeas.Core.Core.Common.Enums;

namespace TaechIdeas.Core.Core.Common
{
    public interface IUtilsManager
    {
        string GetAllPropertiesAndValues<T>(T myObject);
        bool IsIpValid(string ip);
        bool IsEmailValid(string email);

        void LogStoredProcedure(LogLevel logDbLevel, UspReturnValue uspReturn);
        string GetLanguageCodeFromId(int languageId);
        IEnumerable<DateTime> EachDay(DateTime from, DateTime to, int dayInterval);
    }
}