using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace RWD.Toolbox.Logging.Infrastructure.Attribute
{
   public sealed class TrackUsageAttribute : ActionFilterAttribute
   {
      private readonly ILogger<TrackUsageAttribute> _logger;
     // private readonly Stopwatch _timer;

      public TrackUsageAttribute(ILogger<TrackUsageAttribute> logger)
      {
         _logger = logger;
         //_timer = new Stopwatch();
      }

      //public override void OnActionExecuting(ActionExecutingContext context)
      //{
      //   //_timer.Start();
      //}

      public override void OnActionExecuted(ActionExecutedContext context)
      {
        // _timer.Stop();
         if (context.Exception == null)
         {
            _logger.LogRouteUsage(context.HttpContext.Request.Path);
         }
      }

   }

}
