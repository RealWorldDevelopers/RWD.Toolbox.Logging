using Microsoft.AspNetCore.Http;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;

namespace RWD.Toolbox.Logging.MVC
{
   // TODO DELETE  
   public static class WebHelper
   {
      //public static void LogWebUsage(string product, string layer, string activityName,
      //    HttpContext context, Dictionary<string, object> additionalInfo = null)
      //{
      //   var details = GetWebLogDetails(product, layer, activityName, context, additionalInfo);

      //   //TODO build this into logger instead of a helper
      //   Logger.WriteUsage(details);
      //}

      //public static void LogWebDiagnostic(string product, string layer, string message,
      //    HttpContext context, Dictionary<string, object> diagnosticInfo = null)
      //{
      //   var details = GetWebLogDetails(product, layer, message, context, diagnosticInfo);
      //   Logger.Log(LogEventLevel.Information, details.Message);
      //}

      //public static void LogWebError(string product, string layer, Exception ex, HttpContext context)
      //{
      //   var details = GetWebLogDetails(product, layer, null, context, null);
      //   // details.Exception = ex;

      //   Logger.Log(LogEventLevel.Error, ex, details.Message);
      //}

      //public static LogDetails GetWebLogDetails(string product, string layer,
      //    string activityName, HttpContext context,
      //    Dictionary<string, object> additionalInfo = null)
      //{
      //   var detail = new LogDetails
      //   {
      //      Product = product,
      //      Layer = layer,
      //      Message = activityName,
      //      Hostname = Environment.MachineName,
      //      CorrelationId = Activity.Current?.Id ?? context.TraceIdentifier,
      //      AdditionalInfo = additionalInfo ?? new Dictionary<string, object>()
      //   };

      //   GetUserData(detail, context);
      //   GetRequestData(detail, context);
      //   // TODO Session data??
      //   // TODO Cookie data??
      //   // TODO get browser name and version
      //   // TODO get operating system

      //   return detail;
      //}

      //private static void GetRequestData(LogDetails detail, HttpContext context)
      //{
      //   // TODO refine or delete         
      //   var request = context.Request;
      //   if (request != null)
      //   {
      //      detail.Location = request.Path;

      //      detail.AdditionalInfo.Add("UserAgent", request.Headers["User-Agent"]);
      //      // non en-US preferences here??
      //      detail.AdditionalInfo.Add("Languages", request.Headers["Accept-Language"]);

      //      var qdict = Microsoft.AspNetCore.WebUtilities
      //          .QueryHelpers.ParseQuery(request.QueryString.ToString());
      //      foreach (var key in qdict.Keys)
      //      {
      //         detail.AdditionalInfo.Add($"QueryString-{key}", qdict[key]);
      //      }

      //   }
      //}

      //private static void GetUserData(LogDetails detail, HttpContext context)
      //{
      //   // TODO refine or delete
      //   var userId = "";
      //   var userName = "";
      //   var user = context.User;
      //   if (user != null)
      //   {
      //      var i = 1; // i included in dictionary key to ensure uniqueness
      //      foreach (var claim in user.Claims)
      //      {
      //         if (claim.Type == ClaimTypes.NameIdentifier)
      //            userId = claim.Value;
      //         else if (claim.Type == "name")
      //            userName = claim.Value;
      //         else
      //            // example dictionary key: UserClaim-4-role 
      //            detail.AdditionalInfo.Add($"UserClaim-{i++}-{claim.Type}", claim.Value);
      //      }
      //   }
      //   detail.UserId = userId;
      //   detail.UserName = userName;
      //}

   }

}
