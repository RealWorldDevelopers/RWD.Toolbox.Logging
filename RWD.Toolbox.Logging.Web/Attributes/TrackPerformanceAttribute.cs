// TODO Delete

//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc.Filters;

//namespace RWD.Toolbox.Logging.Web.Attributes
//{
//   public sealed class TrackPerformanceAttribute : ActionFilterAttribute
//   {
//      private readonly string _productName;
//      private readonly string _layerName;
      
//      // can use like [TrackPerformance("ToDos", "Mvc")]
//      public TrackPerformanceAttribute(string product, string layer) 
//      {
//         _productName = product;
//         _layerName = layer;
//      }

//      public override void OnActionExecuting(ActionExecutingContext filterContext)
//      {
//         string userId, userName, location;
//         var dict = Helpers.GetWebFloggingData(filterContext.HttpContext, out userId, out userName, out location);

//         var type = filterContext.HttpContext.Request.Method;
//         var perfName = filterContext.ActionDescriptor.DisplayName + "_" + type;

//         var stopwatch = new PerfTracker(perfName, userId, userName, location,
//             _productName, _layerName, dict);
//         filterContext.HttpContext.Items["Stopwatch"] = stopwatch;
//      }

//      public override void OnResultExecuted(ResultExecutedContext filterContext)
//      {
//         var stopwatch = (PerfTracker)filterContext.HttpContext.Items["Stopwatch"];
//         if (stopwatch != null)
//            stopwatch.Stop();
//      }
//   }
//}
