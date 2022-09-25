using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;


namespace RWD.Toolbox.Logging.Infrastructure.Filters
{
    /// <summary>
    /// Usage Action Filter
    /// </summary>
    public class TrackActionUsageFilter : IActionFilter
    {

        private readonly ILogger<TrackActionUsageFilter> _logger;

        public TrackActionUsageFilter(ILogger<TrackActionUsageFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Take no Action
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                _logger.LogRouteUsage(context.HttpContext.Request.Path);
            }
        }


    }

}
