using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RWD.Toolbox.Logging.Demo.Communication
{
   public interface ICommunicationAgent
   {
      Task<List<T>> GetListFromApiAsync<T>(string path, HttpContext context, ILogger logger);
   }

   public class CommunicationAgent : ICommunicationAgent
   {
      public async Task<List<T>> GetListFromApiAsync<T>(string path, HttpContext context, ILogger logger)
      {
         var client = GetHttpClientWithBearerToken(context);

         var apiRequestPath = $"{path}";
         var response = await client.GetAsync(apiRequestPath);

         if (!response.IsSuccessStatusCode)
         {
            var jsonContent = await response.Content.ReadAsStringAsync();
            var error = JObject.Parse(jsonContent);
            string errorId = null, errorTitle = null, errorDetail = null;
            if (error != null)
            {
               errorId = error["Id"]?.ToString();
               errorTitle = error["Title"]?.ToString();
               errorDetail = error["Detail"]?.ToString();
            }
            var ex = new Exception("API Failure");

            var reqPath = context.Request.Path;
            var qryString = context.Request.QueryString;
            var reqUri = reqPath + qryString;

            ex.Data.Add("API Route", $"GET {reqUri}");
            ex.Data.Add("API Status", (int)response.StatusCode);
            ex.Data.Add("API ErrorId", errorId);
            ex.Data.Add("API Title", errorTitle);
            ex.Data.Add("API Detail", errorDetail);

            logger.Log(LogLevel.Warning, ex, $"API Error when calling GET: {reqUri}");
         }

         client.Dispose();

         var xx = await response.Content.ReadAsStringAsync();
         var returnObj = JsonConvert.DeserializeObject<List<T>>(xx);

         return returnObj;
      }

      private static HttpClient GetHttpClientWithBearerToken(HttpContext context)
      {
         // var token = await context.GetTokenAsync("access_token");

         var client = new HttpClient();
         //client.SetBearerToken(token);
         return client;
      }

   }

}
