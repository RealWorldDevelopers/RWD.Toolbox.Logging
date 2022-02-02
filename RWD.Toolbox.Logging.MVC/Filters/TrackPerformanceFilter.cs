using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace RWD.Toolbox.Logging.MVC.Filters
{

   //TODO DELETE Me
   public class TrackPerformanceFilter 
   {
      //private PerfTracker _tracker;
      //private string _product, _layer;
      //public TrackPerformanceFilter(string product, string layer)
      //{
      //   _product = product;
      //   _layer = layer;
      //}

      //public void OnActionExecuting(ActionExecutingContext context)
      //{
      //   var request = context.HttpContext.Request;
      //   var activity = $"{request.Path}-{request.Method}";

      //   var dict = new Dictionary<string, object>();
      //   foreach (var key in context.RouteData.Values?.Keys)
      //      dict.Add($"RouteData-{key}", (string)context.RouteData.Values[key]);

      //   // TODO eliminate web helper if can 
      //   var details = WebHelper.GetWebLogDetails(_product, _layer, activity,
      //       context.HttpContext, dict);

      //   // TODO use Ilogger if possible
      //   _tracker = new PerfTracker(details);
      //}

      //public void OnActionExecuted(ActionExecutedContext context)
      //{
      //   if (_tracker != null)
      //      _tracker.Stop();
      //}


      // TODO version 2 with high performance call using ILogger

      //private Stopwatch _timer;
      //private readonly ILogger<TrackActionPerformanceFilter> _logger;

      //public TrackActionPerformanceFilter(ILogger<TrackActionPerformanceFilter> logger)
      //{
      //   _logger = logger;
      //}
      //public void OnActionExecuting(ActionExecutingContext context)
      //{
      //   _timer = new Stopwatch();
      //   _timer.Start();
      //}

      //public void OnActionExecuted(ActionExecutedContext context)
      //{
      //   _timer.Stop();
      //   if (context.Exception == null)
      //   {
      //      _logger.LogRoutePerformance(context.HttpContext.Request.Path,
      //          context.HttpContext.Request.Method,
      //          _timer.ElapsedMilliseconds);
      //   }
      //}

   }

}
