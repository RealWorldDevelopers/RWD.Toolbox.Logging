
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace RWD.Toolbox.Logging
{
   public static class Logger
   {
      // TODO match DI log settings to this
      static readonly Serilog.ILogger _logger = new Serilog.LoggerConfiguration()
         .MinimumLevel.Debug() // default is information
         .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
         .Enrich.FromLogContext()
         .Enrich.WithExceptionDetails()
         .Enrich.WithMachineName()
         .Enrich.WithEnvironmentName()
         .Enrich.WithEnvironmentUserName()
         .Enrich.WithProperty("Assembly", $"{Assembly.GetExecutingAssembly().GetName().Name}")
         .Enrich.WithProperty("Version", $"{Assembly.GetExecutingAssembly().GetName().Version}")
         .WriteTo.File(new RenderedCompactJsonFormatter(), @"E:\Testing\error.json", shared: true)
        //.WriteTo.MSSqlServer(
        //   connectionString: AppSettings.ConnString,
        //   sinkOptions: new MSSqlServerSinkOptions { TableName = "Log_Error", AutoCreateSqlTable = true, BatchPostingLimit = 1 },
        //   columnOptions: GetSqlColumnOptions())
        .CreateLogger();


      public static void Log(LogEventLevel logLevel, Exception ex, string message)
      {
         // TODO cause a error needs figured out
         // Serilog.Debugging.SelfLog.Enable(msg => File.AppendAllText(@"E:\testing\serilog.txt", msg + Environment.NewLine));

         _logger.Write(logLevel, ex, message);
      }

      public static void Log(LogEventLevel logLevel, string message)
      {
         // TODO cause a error needs figured out
         // Serilog.Debugging.SelfLog.Enable(msg => File.AppendAllText(@"E:\testing\serilog.txt", msg + Environment.NewLine));

         _logger.Write(logLevel, message);
      }

      public static void Log(LogEventLevel logLevel, string messageTemplate, params object[] propertyValues)
      {
         // TODO cause a error needs figured out
         // Serilog.Debugging.SelfLog.Enable(msg => File.AppendAllText(@"E:\testing\serilog.txt", msg + Environment.NewLine));

         _logger.Write(logLevel, messageTemplate, propertyValues);
      }

           
      // TODO make method to build MSSqlServerSinkOptions like sql columns object

          

      // TODO config builder
      //private static LoggerConfiguration BuildConfig(IServiceProvider serviceProvider)
      //{
      //   return new Serilog.LoggerConfiguration()
      //   .MinimumLevel.Debug() // default is information
      //                         //.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
      //   .Enrich.FromLogContext()
      //   .Enrich.WithExceptionDetails()
      //   .Enrich.WithMachineName()
      //   //.Enrich.WithAspnetcoreHttpcontext(provider, false,
      //   //               AddCustomContextInfo)
      //   .Enrich.WithProperty("Assembly", $"{Assembly.GetExecutingAssembly().GetName().Name}")
      //   .Enrich.WithProperty("Version", $"{Assembly.GetExecutingAssembly().GetName().Version}")
      //   //.WriteTo.File(path: @"E:\testing\diagnostic.txt", shared:true) 
      //   .WriteTo.File(new RenderedCompactJsonFormatter(), @"E:\Testing\diagnostic.json", shared: true)
      //   .WriteTo.MSSqlServer(
      //      connectionString: AppSettings.ConnString,
      //      sinkOptions: new MSSqlServerSinkOptions { BatchPeriod = new TimeSpan(0, 0, 0, 1, 0), TableName = "Log_Diagnostic", AutoCreateSqlTable = true, BatchPostingLimit = 1 },
      //      columnOptions: GetSqlColumnOptions());
      //}

      

      public static ColumnOptions GetSqlColumnOptions()
      {
         var colOptions = new ColumnOptions();
         // colOptions.Store.Remove(StandardColumn.TimeStamp);
         // colOptions.Store.Remove(StandardColumn.Message);
         // colOptions.Store.Remove(StandardColumn.Level);
         // colOptions.Store.Remove(StandardColumn.Exception);

         colOptions.Store.Remove(StandardColumn.Properties);
         colOptions.Store.Remove(StandardColumn.MessageTemplate);

         colOptions.AdditionalColumns = new List<SqlColumn>
         {
          //  new SqlColumn { AllowNull = true, DataType = SqlDbType.DateTime, ColumnName = "Timestamp" },
         //   new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = -1, ColumnName = "SourceContext" },

            new SqlColumn { AllowNull = true, DataType = SqlDbType.BigInt, ColumnName = "ElapsedMilliseconds" },
            new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "ErrorId" },
            new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "SourceContext" },
            new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "ActionId" },
            new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "ActionName" },
            new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "RequestId" },
            new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "RequestPath" },
            new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "SpanId" },
            new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "TraceId" },
            new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "ParentId" },
            new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "MachineName" },
            new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "EnvironmentName" },
            new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "EnvironmentUserName" },
            new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "Assembly" },
            new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "Version" },



            //new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "UserId" },
            //new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "UserName" },
           // new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = -1, ColumnName = "Exception" },

            

            //new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "CorrelationId" },
            //new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = -1, ColumnName = "CustomException" },
            //new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = -1, ColumnName = "AdditionalInfo" }
         };

         return colOptions;
      }
                


   }


}



// TODO to Readme.md
//LogEventLevels

//None 6
//Not used for writing log messages. Specifies that a logging category should not write any messages.

//Critical	5	
//Logs that describe an unrecoverable application or system crash, or a catastrophic failure that requires immediate attention.

//Error	4	
//Logs that highlight when the current flow of execution is stopped due to a failure. These should indicate a failure in the current activity, not an application-wide failure.

//Warning	3	
//Logs that highlight an abnormal or unexpected event in the application flow, but do not otherwise cause the application execution to stop.

//Information	2	- DEFAULT
//Logs that track the general flow of the application. These logs should have long-term value.

//Debug	1	
//Logs that are used for interactive investigation during development. These logs should primarily contain information useful for debugging and have no long-term value.

//Trace	0	
//Logs that contain the most detailed messages. These messages may contain sensitive application data. These messages are disabled by default and should never be enabled in a production environment.

