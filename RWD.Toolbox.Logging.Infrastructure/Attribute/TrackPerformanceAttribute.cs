using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace RWD.Toolbox.Logging.Infrastructure.Attribute
{
   public sealed class TrackPerformanceAttribute : ActionFilterAttribute
   {
      private readonly ILogger<TrackPerformanceAttribute> _logger;
      private readonly Stopwatch _timer;

      public TrackPerformanceAttribute(ILogger<TrackPerformanceAttribute> logger)
      {
         _logger = logger;
         _timer = new Stopwatch();
      }

      public override void OnActionExecuting(ActionExecutingContext context)
      {
         _timer.Start();
      }

      public override void OnActionExecuted(ActionExecutedContext context)
      {
         _timer.Stop();
         if (context.Exception == null)
         {
            _logger.LogRoutePerformance( _timer.ElapsedMilliseconds);
         }
      }

   }
}
