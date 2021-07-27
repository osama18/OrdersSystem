using System;
using System.Collections.Generic;


namespace OrderManagementSystem.Common.Loggers
{
    public interface ILogger
    {
        void LogError(Enum logEvent, string Format);
        void LogInformation(Enum logEvent, string Format);
        void LogWarning(Enum logEvent, string Format);
        void LogException(Enum logEvent, Exception ex);

    }
}
