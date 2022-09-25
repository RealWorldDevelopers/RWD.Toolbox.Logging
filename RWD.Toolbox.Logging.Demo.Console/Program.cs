
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using System;
using System.Diagnostics;
using System.Reflection;

namespace RWD.Toolbox.Logging.Demo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrWhiteSpace(environment))
                throw new NullReferenceException(nameof(environment));

            var services = ConfigureServices(environment);
            var serviceProvider = services.BuildServiceProvider();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug() // default is information
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithProperty("Assembly", $"{Assembly.GetExecutingAssembly().GetName().Name}")
                .Enrich.WithProperty("Version", $"{Assembly.GetExecutingAssembly().GetName().Version}")
                .WriteTo.Console() // output to console
                .WriteTo.File(new RenderedCompactJsonFormatter(), @"E:\Testing\error.json", shared: true)
                //.WriteTo.MSSqlServer(
                //   connectionString: AppSettings.ConnString,
                //   sinkOptions: new MSSqlServerSinkOptions { TableName = "Log_Error", AutoCreateSqlTable = true, BatchPostingLimit = 1 },
                //   columnOptions: GetSqlColumnOptions())
                .CreateLogger();


            // Info Log
            Log.Information("starting demo console application");
            
            // Start Performance Tracker
            var timer = new Stopwatch();
            timer.Start();

            // Usage Tracking
            Log.Information("{ActionName}{ElapsedMilliseconds}", "Console Demo Application", timer.ElapsedMilliseconds);

            try
            {
                // create 2 persons
                var person1 = new Person("Jonh", "Gold");
                var person2 = new Person("James", "Miller");
                // create 2 cars
                var car1 = new Car("Tesla Model S", 2020, person1);
                var car2 = new Car("Tesla Model X", 2020, person2);

                // sample logging
                Log.Verbose("Some verbose log");
                Log.Debug("Some debug log");
                Log.Information("Person1: {@person}", person1);
                Log.Information("Car2: {@car}", car2);
                Log.Warning("Warning accrued at {now}", DateTime.Now);
                Log.Error("Error accrued at {now}", DateTime.Now);
                Log.Fatal("Problem with car {@car} accrued at {now}", car1, DateTime.Now);

                //Throw error to test error logger
                var ex = new Exception("Something bad has happened!");
                ex.Data.Add("input param", "nothing to see here");
                throw ex;
            }
            catch (Exception ex)
            {
                // Error Log
                Log.Error(ex, "From Within console demo try/catch");
            }
            finally
            {
                // End Performance Tracker
                timer.Stop();
                Log.Information("{ActionName}{ElapsedMilliseconds}", "Console Demo Application", timer.ElapsedMilliseconds);


                // fake code to allow logs to complete before exiting console app
                var sec = 10;
                while (sec >= 0)
                {
                    System.Threading.Thread.Sleep(1000);
                    System.Console.WriteLine(sec);
                    sec--;
                }

                Log.CloseAndFlush();
            }


        }

        private static IServiceCollection ConfigureServices(string environment)
        {
            var configuration = LoadConfiguration(environment);

            IServiceCollection services = new ServiceCollection();

            //var appSettings = new AppSettings();
            //configuration.GetSection("AppSettings").Bind(appSettings);

            //services.AddSingleton(configuration);
            //services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            //services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));

            //services.AddAutoMapper(typeof(Program));                       

            //services.AddDbContext<Data.PrologueContext>(options =>
            //{
            //    options.UseSqlServer(configuration.GetConnectionString("PrologueDbContext"),
            //           sqlServerOptionsAction: sqlOptions =>
            //           {
            //               sqlOptions.EnableRetryOnFailure(
            //                  maxRetryCount: 10,
            //                  maxRetryDelay: TimeSpan.FromSeconds(30),
            //                  errorNumbersToAdd: null);
            //           });
            //});

            //  Set up classes
            //services.AddTransient<IFactory, Factory>();
            

            return services;
        }


        private static IConfiguration LoadConfiguration(string environment)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", false, true)
                .AddJsonFile($"appsettings.{environment}.json", false, true)
                .AddEnvironmentVariables();
                    
            return builder.Build();
        }


    }
}
