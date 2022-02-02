using System;
using Microsoft.Extensions.Logging;

namespace RWD.Toolbox.Logging.Infrastructure
{
   public static class LogMessages
   {
      private static readonly Action<ILogger, long, Exception> _routePerformance;
      private static readonly Action<ILogger, string, Exception> _routeUsage;

      static LogMessages()
      {
         _routePerformance = LoggerMessage.Define<long>(LogLevel.Information, 0, "{ElapsedMilliseconds}");
         _routeUsage = LoggerMessage.Define<string>(LogLevel.Information, 0, "{Message}");
      }

      public static void LogRoutePerformance(this ILogger logger, long elapsedMilliseconds)
      {
         _routePerformance(logger, elapsedMilliseconds, null);
      }

      public static void LogRouteUsage(this ILogger logger, string message)
      {
         _routeUsage(logger, message, null);
      }

   }

}
