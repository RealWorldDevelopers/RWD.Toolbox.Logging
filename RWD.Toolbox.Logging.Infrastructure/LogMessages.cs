using System;
using Microsoft.Extensions.Logging;

namespace RWD.Toolbox.Logging.Infrastructure
{
    /// <summary>
    /// Configure Performance and Usage Logs
    /// </summary>
    public static class LogMessages
    {
        private static readonly Action<ILogger, long, Exception> _routePerformance;
        private static readonly Action<ILogger, string, Exception> _routeUsage;

        /// <summary>
        /// Contructor
        /// </summary>
        static LogMessages()
        {
            _routePerformance = LoggerMessage.Define<long>(LogLevel.Information, 0, "{ElapsedMilliseconds}");
            _routeUsage = LoggerMessage.Define<string>(LogLevel.Information, 0, "{Message}");
        }

        /// <summary>
        /// Called to Log a Performance Metrics
        /// </summary>
        public static void LogRoutePerformance(this ILogger logger, long elapsedMilliseconds)
        {
            _routePerformance(logger, elapsedMilliseconds, null);
        }

        /// <summary>
        /// Called to Log Usage Metrics
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        public static void LogRouteUsage(this ILogger logger, string message)
        {
            _routeUsage(logger, message, null);
        }

    }

}
