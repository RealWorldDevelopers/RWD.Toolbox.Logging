using Microsoft.Extensions.Logging;


namespace RWD.Toolbox.Logging.Demo.ClassLibrary
{
   public interface IWeatherData
   {
      string[] GetWeatherValues();
   }

   public class WeatherData : IWeatherData
   {
      private readonly string[] Summaries = new[]
      { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

      private readonly ILogger<WeatherData> _logger;

      public WeatherData(ILogger<WeatherData> logger)
      {
         _logger = logger;
      }

      public string[] GetWeatherValues()
      {
        // TODO _logger.LogInformation("Inside the WeatherData Library.");
         return Summaries;
      }

   }
}
