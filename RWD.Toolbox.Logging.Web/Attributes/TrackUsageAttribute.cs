// TODO Delete

//using Microsoft.AspNetCore.Mvc.Filters;

//namespace RWD.Toolbox.Logging.Web.Attributes
//{
//   public sealed class TrackUsageAttribute : ActionFilterAttribute
//   {
//      private readonly string _productName;
//      private readonly string _layerName;
//      private readonly string _name;

//      public TrackUsageAttribute(string product, string layer, string name)
//      {
//         _productName = product;
//         _layerName = layer;
//         _name = name;
//      }

//      public override void OnResultExecuted(ResultExecutedContext filterContext)
//      {
//         Helpers.LogWebUsage(_productName, _layerName, _name, filterContext.HttpContext);
//      }
//   }
//}
