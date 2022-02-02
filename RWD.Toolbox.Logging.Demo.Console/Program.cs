
using System;
using System.Diagnostics;

namespace RWD.Toolbox.Logging.Demo.Console
{
   class Program
   {
      static void Main(string[] args)
      {
         // Info Log
         Logger.Log(Serilog.Events.LogEventLevel.Information, "starting demo console application");

         // Start Performance Tracker
         var timer = new Stopwatch();
         timer.Start();

         // Usage Tracking
         Logger.Log(Serilog.Events.LogEventLevel.Information, "{ActionName}{ElapsedMilliseconds}", "Console Demo Application", timer.ElapsedMilliseconds);

         try
         {
            //Throw error to test error logger
            var ex = new Exception("Something bad has happened!");
            ex.Data.Add("input param", "nothing to see here");
            throw ex;
         }
         catch (Exception ex)
         {
            // Error Log
            Logger.Log(Serilog.Events.LogEventLevel.Error, ex, "From Within console demo try/catch");
         }
         finally
         {
            // End Performance Tracker
            timer.Stop();
            Logger.Log(Serilog.Events.LogEventLevel.Information, "{ActionName}{ElapsedMilliseconds}", "Console Demo Application", timer.ElapsedMilliseconds);


            // fake code to allow logs to complete before exiting console app
            var sec = 10;
            while (sec >= 0)
            {
               System.Threading.Thread.Sleep(1000);
               System.Console.WriteLine(sec);
               sec--;
            }

         }


      }


   }
}
