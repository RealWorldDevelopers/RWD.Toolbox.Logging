
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace RWD.Toolbox.Logging
{
   
    /// <summary>
    /// Sample static logging class
    /// </summary>
    public static class Logger
    {        
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


        /// <summary>
        /// Log Exception
        /// </summary>
        public static void Log(LogEventLevel logLevel, Exception ex, string message)
        {
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

            _logger.Write(logLevel, ex, message);
        }

        /// <summary>
        /// Log Message
        /// </summary>
        public static void Log(LogEventLevel logLevel, string message)
        {
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

            _logger.Write(logLevel, message);
        }

        /// <summary>
        /// Log Complex Message
        /// </summary>
        public static void Log(LogEventLevel logLevel, string messageTemplate, params object[] propertyValues)
        {
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

            _logger.Write(logLevel, messageTemplate, propertyValues);
        }


        /// <summary>
        /// Create Custom SQL Columns for Serilog
        /// </summary>
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
            //  new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = -1, ColumnName = "SourceContext" },

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


            // new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "UserId" },
            // new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "UserName" },
            // new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = -1, ColumnName = "Exception" },

            
            // new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = 500, ColumnName = "CorrelationId" },
            // new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = -1, ColumnName = "CustomException" },
            // new SqlColumn { AllowNull = true, DataType = SqlDbType.NVarChar, DataLength = -1, ColumnName = "AdditionalInfo" }

         };

            return colOptions;
        }

    }

}


