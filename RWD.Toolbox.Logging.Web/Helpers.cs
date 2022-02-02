//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.Features;
//using Microsoft.AspNetCore.WebUtilities;
//using System;
//using System.Collections.Generic;
//using System.Security.Claims;
//using System.Web;


// TODO Delete

//namespace RWD.Toolbox.Logging.Web
//{

//   // 1st edition  prior to middleware
//   public static class Helpers
//   {

//      public static void LogWebUsage(string product, string layer, string activityName,
//          HttpContext httpContext, Dictionary<string, object> additionalInfo = null)
//      {

//         string userId, userName, location;
//         bool usesSessions = (httpContext.Features.Get<ISessionFeature>()?.Session != null && httpContext.Session.IsAvailable);
//         var webInfo = GetWebFloggingData(httpContext, out userId, out userName, out location);

//         if (additionalInfo != null)
//         {
//            foreach (var key in additionalInfo.Keys)
//               webInfo.Add($"Info-{key}", additionalInfo[key]);
//         }

//         var usageInfo = new LogDetails()
//         {
//            Product = product,
//            Layer = layer,
//            Timestamp = DateTime.Now,
//            Location = location,
//            UserId = userId,
//            UserName = userName,
//            Hostname = Environment.MachineName,
//            CorrelationId = usesSessions ? httpContext.Session.Id : string.Empty,
//            Message = activityName,
//            AdditionalInfo = webInfo
//         };

//         Logger.WriteUsage(usageInfo);
//      }

//      public static void LogWebDiagnostic(string product, string layer, string message,
//          HttpContext httpContext, Dictionary<string, object> diagnosticInfo = null)
//      {
//         var _writeDiagnostics = true; // TODO get from somewhere
//         if (!_writeDiagnostics)
//            return;

//         string userId, userName, location;
//         bool usesSessions = (httpContext.Features.Get<ISessionFeature>()?.Session != null && httpContext.Session.IsAvailable);
//         var webInfo = GetWebFloggingData(httpContext, out userId, out userName, out location);
//         if (diagnosticInfo != null)
//         {
//            foreach (var key in diagnosticInfo.Keys)
//               webInfo.Add(key, diagnosticInfo[key]);
//         }

//         var diagInfo = new LogDetails()
//         {
//            Product = product,
//            Layer = layer,
//            Location = location,
//            Timestamp = DateTime.Now,
//            UserId = userId,
//            UserName = userName,
//            Hostname = Environment.MachineName,
//            CorrelationId = usesSessions ? httpContext.Session.Id : string.Empty,
//            Message = message,
//            AdditionalInfo = webInfo
//         };

//         Logger.WriteDiagnostic(diagInfo);
//      }

//      public static void LogWebError(string product, string layer, Exception ex, HttpContext httpContext)
//      {
//         string userId, userName, location;
//         bool usesSessions = (httpContext.Features.Get<ISessionFeature>()?.Session != null && httpContext.Session.IsAvailable);
//         var webInfo = GetWebFloggingData(httpContext, out userId, out userName, out location);

//         var errorInformation = new LogDetails()
//         {
//            Product = product,
//            Layer = layer,
//            Location = location,
//            Timestamp = DateTime.Now,
//            UserId = userId,
//            UserName = userName,
//            Hostname = Environment.MachineName,
//            CorrelationId = usesSessions ? httpContext.Session.Id : string.Empty,
//            Exception = ex,
//            AdditionalInfo = webInfo
//         };

//         Logger.WriteError(errorInformation);
//      }

//      public static void GetHttpStatus(HttpContext httpContext, out int httpStatus)
//      {
//         httpStatus = httpContext.Response.StatusCode;
//      }

//      public static Dictionary<string, object> GetWebFloggingData(HttpContext httpContext, out string userId,
//          out string userName, out string location)
//      {
//         var data = new Dictionary<string, object>();

//         GetRequestData(httpContext, data, out location);
//         GetUserData(data, ClaimsPrincipal.Current, out userId, out userName);
//         GetSessionData(httpContext, data);
//         // got cookies?  

//         return data;
//      }

//      private static void GetRequestData(HttpContext httpContext, Dictionary<string, object> data, out string location)
//      {
//         location = "";
//         var request = httpContext.Request;
//         // rich object - you may want to explore this
//         if (request != null)
//         {
//            location = request.Path;

//            string type, version;
//            // MS Edge requires special detection logic
//            GetBrowserInfo(request, out type, out version);
//            data.Add("Browser", $"{type}{version}");
//            data.Add("UserAgent", request.Headers["User-Agent"]);
//            data.Add("Languages", request.Headers["Accept-Language"].ToString());  // non en-US preferences here??
//            foreach (var qsKey in QueryHelpers.ParseQuery(request.QueryString.Value))
//            {
//               data.Add(string.Format("QueryString-{0}", qsKey), qsKey.Value.ToString());
//            }
//         }
//      }
//      private static void GetBrowserInfo(HttpRequest request, out string type, out string version)
//      {
//         var userAgent = request.Headers["User-Agent"];

//         // TODO find browser info

//         //type = request.Browser.Type;
//         //if (type.StartsWith("Chrome") && userAgent.Contains("Edge/"))
//         //{
//         type = "Edge";
//         //   version = " (v " + userAgent
//         //       .Substring(userAgent.IndexOf("Edge/") + 5) + ")";
//         //}
//         //else
//         //{
//         version = ""; // " (v " + request.Browser.MajorVersion + "." +
//         //       request.Browser.MinorVersion + ")";
//         //}
//      }

//      internal static void GetUserData(Dictionary<string, object> data,
//          ClaimsPrincipal user,
//          out string userId,
//          out string userName)
//      {
//         userId = "";
//         userName = "";
//         if (user != null)
//         {
//            var i = 1; // i included in dictionary key to ensure uniqueness
//            foreach (var claim in user.Claims)
//            {
//               if (claim.Type == ClaimTypes.NameIdentifier)
//                  userId = claim.Value;
//               else if (claim.Type == ClaimTypes.Name)
//                  userName = claim.Value;
//               else
//                  // example dictionary key: UserClaim-4-role 
//                  data.Add(string.Format("UserClaim-{0}-{1}", i++, claim.Type),
//                      claim.Value);
//            }
//         }
//      }

//      internal static void GetLocationForApiCall(HttpContext requestContext,
//        Dictionary<string, object> dict, out string location)
//      {
//         // example route template: api/{controller}/{id}
//         var routeTemplate = requestContext.Request.RouteValues.ToString();

//         var method = requestContext.Request.Method;  // GET, POST, etc.

//         foreach (var key in requestContext.Request.RouteValues.Keys)
//         {
//            var value = requestContext.Request.RouteValues[key].ToString();
//            if (Int64.TryParse(value, out long numeric))  // C# 7 inline declaration
//                                                          // must be numeric part of route
//               dict.Add($"Route-{key}", value.ToString());
//            else
//               routeTemplate = routeTemplate.Replace("{" + key + "}", value);
//         }

//         location = $"{method} {routeTemplate}";

//         var qs = HttpUtility.ParseQueryString(requestContext.Request.QueryString.ToString());
//         var i = 0;
//         foreach (string key in qs.Keys)
//         {
//            var newKey = string.Format("q-{0}-{1}", i++, key);
//            if (!dict.ContainsKey(newKey))
//               dict.Add(newKey, qs[key]);
//         }

//         // misspell of referrer is intentional due to MicroSoft misspelling
//         var referrer = requestContext.Request.Headers["Referer"].ToString();
//         if (!string.IsNullOrWhiteSpace(referrer))
//         {
//            if (referrer.ToLower().Contains("swagger"))
//               referrer = "Swagger";
//            if (!dict.ContainsKey("Referrer"))
//               dict.Add("Referrer", referrer);
//         }
//      }

//      private static void GetSessionData(HttpContext httpContext, Dictionary<string, object> data)
//      {
//         if (httpContext.Features.Get<ISessionFeature>()?.Session != null && httpContext.Session.IsAvailable)
//         {
//            if (httpContext.Session != null)
//            {
//               foreach (var key in httpContext.Session.Keys)
//               {
//                  var keyValue = httpContext.Session.GetString(key);
//                  if (string.IsNullOrWhiteSpace(keyValue))
//                  {
//                     data.Add(string.Format("Session-{0}", key), keyValue);
//                  }
//               }
//               data.Add("SessionId", httpContext.Session.Id);
//            }
//         }
//      }

//   }
//}
