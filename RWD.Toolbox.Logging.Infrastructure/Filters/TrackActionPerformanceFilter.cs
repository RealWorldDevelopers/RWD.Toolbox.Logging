﻿
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace RWD.Toolbox.Logging.Infrastructure.Filters
{
    /// <summary>
    /// Action Performance Filter
    /// </summary>
    public class TrackActionPerformanceFilter : IActionFilter
    {
        private Stopwatch _timer;
        private readonly ILogger<TrackActionPerformanceFilter> _logger;


        public TrackActionPerformanceFilter(ILogger<TrackActionPerformanceFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _timer = new Stopwatch();
            _timer.Start();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _timer.Stop();
            if (context.Exception == null)
            {
                _logger.LogRoutePerformance(_timer.ElapsedMilliseconds);
            }

        }

    }

}
