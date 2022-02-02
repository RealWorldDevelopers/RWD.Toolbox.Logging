using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using System;
using System.Reflection;
using Serilog.Enrichers.AspnetcoreHttpcontext;


namespace RWD.Toolbox.Logging.Demo.MVC
{
   public static class Program
   {
      //private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
      //     .SetBasePath(Directory.GetCurrentDirectory())
      //     .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
      //     .AddEnvironmentVariables()
      //     .Build();

      // Add Product and Layer options to ILogger

      public static void Main(string[] args)
      {
         var name = Assembly.GetExecutingAssembly().GetName();
         Log.Logger = new LoggerConfiguration()
         //.ReadFrom.Configuration(Configuration)
         .MinimumLevel.Debug() // default is information
         //.MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
         .Enrich.FromLogContext()
         .Enrich.WithExceptionDetails()
         .Enrich.WithMachineName()
         .Enrich.WithEnvironmentName()
         .Enrich.WithEnvironmentUserName()
         .Enrich.WithProperty("Assembly", $"{name.Name}")
         .Enrich.WithProperty("Version", $"{name.Version}")
         .WriteTo.File(new RenderedCompactJsonFormatter(), @"E:\Testing\error.json", shared: true)
         //.WriteTo.MSSqlServer(connectionString: AppSettings.ConnString,
         //   sinkOptions: new MSSqlServerSinkOptions { TableName = "Log_Error", AutoCreateSqlTable = true, BatchPostingLimit = 1 },
         //   columnOptions: Logger.GetSqlColumnOptions())
         .CreateLogger();

         try
         {
            // TODO Set to usage log
            Log.Information("Starting web host in MVC Demo");
            CreateHostBuilder(args).Build().Run();
         }
         catch (Exception ex)
         {
            // Log Critical Errors
            Log.Fatal(ex, "Host terminated unexpectedly");
         }
         finally
         {
            Log.CloseAndFlush();
         }


      }


      //  Apply Global Logging to App
      public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureWebHostDefaults(webBuilder =>
              {
                 webBuilder.UseStartup<Startup>()
                .UseSerilog((provider, context, loggerConfig) =>
                {
                   // TODO cause a error needs figured out
                   // Serilog.Debugging.SelfLog.Enable(msg => File.AppendAllText(@"E:\testing\serilog.txt", msg + Environment.NewLine));

                   var name = Assembly.GetExecutingAssembly().GetName();
                   loggerConfig
                   //.ReadFrom.Configuration(Configuration)
                       .MinimumLevel.Debug()
                       .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                       .Enrich.WithAspnetcoreHttpcontext(provider)
                       .Enrich.FromLogContext()
                       .Enrich.WithExceptionDetails()
                       .Enrich.WithMachineName()
                       .Enrich.WithEnvironmentName()
                       .Enrich.WithEnvironmentUserName()
                       .Enrich.WithProperty("Assembly", $"{name.Name}")
                       .Enrich.WithProperty("Version", $"{name.Version}")
                       .WriteTo.File(new RenderedCompactJsonFormatter(), @"E:\Testing\error.json", shared: true);
                   //.WriteTo.MSSqlServer(connectionString: AppSettings.ConnString,
                   //    sinkOptions: new MSSqlServerSinkOptions { TableName = "Log_Error", AutoCreateSqlTable = true, BatchPostingLimit = 1 },
                   //    columnOptions: Logger.GetSqlColumnOptions());
                });

              });
   }
}
