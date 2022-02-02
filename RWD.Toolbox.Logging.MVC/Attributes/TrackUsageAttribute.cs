using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace RWD.Toolbox.Logging.MVC.Attributes
{

   // TODO DELETE
   public sealed class TrackUsageAttribute : ActionFilterAttribute
   {
      //private readonly string _product;
      //private readonly string _layer;
      //private readonly string _activityName;

      //public TrackUsageAttribute(string product, string layer, string activityName)
      //{
      //   _product = product;
      //   _layer = layer;
      //   _activityName = activityName;
      //}

      //public override void OnActionExecuted(ActionExecutedContext context)
      //{
      //   var dict = new Dictionary<string, object>();
      //   foreach (var key in context.RouteData.Values?.Keys)
      //      dict.Add($"RouteData-{key}", (string)context.RouteData.Values[key]);

      //  // WebHelper.LogWebUsage(_product, _layer, _activityName, context.HttpContext, dict);
      //}
   }
}
