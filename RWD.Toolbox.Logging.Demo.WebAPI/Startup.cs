using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RWD.Toolbox.Logging.Demo.ClassLibrary;
using RWD.Toolbox.Logging.Infrastructure.Filters;
using RWD.Toolbox.Logging.Infrastructure.Middleware;
using System;
using System.Data.SqlClient;

namespace RWD.Toolbox.Logging.Demo.WebAPI
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }


      public void ConfigureServices(IServiceCollection services)
      {
         services.AddTransient<IWeatherData, WeatherData>();

         // TODO Global Performance and Usage Tracking
         services.AddControllers(options =>
         {
            // options.Filters.Add(typeof(TrackActionPerformanceFilter));
            options.Filters.Add(typeof(TrackActionUsageFilter));
         });

         services.AddSwaggerGen(c =>
         {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
               Title = "Demo API",
               Version = "v1",
               Description = "Description for the API goes here.",
               Contact = new OpenApiContact
               {
                  Name = "Ankush Jain",
                  Email = string.Empty,
                  Url = new Uri("https://coderjony.com/"),
               },
            });
         });

      }

      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         app.UseSwagger();
         app.UseSwaggerUI(c =>
         { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API V1"); });

         // TODO uncomment in real time usage
         //if (env.IsDevelopment())
         //{
         //   app.UseDeveloperExceptionPage();
         //}
         //else
         //{         

         // Return custom safe API errors in production
         app.UseApiExceptionHandler(options =>
         {
            options.AddResponseDetails = UpdateApiErrorResponse;
            options.DetermineLogLevel = DetermineLogLevel;
         });
         app.UseHsts();

         //}


         app.UseHttpsRedirection();

         app.UseRouting();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }

      // Determine how to classify error
      private LogLevel DetermineLogLevel(Exception ex)
      {
         if (ex.Message.StartsWith("cannot open database", StringComparison.InvariantCultureIgnoreCase) ||
             ex.Message.StartsWith("a network-related", StringComparison.InvariantCultureIgnoreCase))
         {
            return LogLevel.Critical;
         }

         return LogLevel.Error;
      }

      
      // Add Custom Notes to Errors
      private void UpdateApiErrorResponse(HttpContext context, Exception ex, ApiError error)
      {
         if (ex.GetType().Name == nameof(SqlException))
         {
            error.Detail = "Exception was a database exception!";
         }
      }




   }
}
