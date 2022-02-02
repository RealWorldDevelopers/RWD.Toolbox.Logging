using System;
using System.Collections.Generic;

namespace RWD.Toolbox.Logging.Demo.MVC.Models
{
   public class WeatherForecast
   {
      public DateTime Date { get; set; }

      public int TemperatureC { get; set; }

      public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

      public string Summary { get; set; }
   }

   public class WeatherForecasts
   {
      public WeatherForecasts()
      {
         Forecasts = new List<WeatherForecast>();
      }
      public List<WeatherForecast> Forecasts { get;  }
   }

}
