using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RWD.Toolbox.Logging.Demo.ClassLibrary;
using RWD.Toolbox.Logging.Demo.Models.WebAPI;
using RWD.Toolbox.Logging.Demo.WebAPI.Models;
using RWD.Toolbox.Logging.Infrastructure.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RWD.Toolbox.Logging.Demo.WebAPI.Controllers
{
   [Produces("application/json")]
   [Route("api/data")]
   public class PageDataController : Controller
   {
      private readonly string[] Summaries = new[]
      {
         "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
      };

      private readonly ILogger<PageDataController> _logger;

      public PageDataController(IWeatherData weatherData, ILogger<PageDataController> logger)
      {
         _logger = logger;
         Summaries = weatherData.GetWeatherValues();
      }

      [HttpGet]
      [Route("todos")]
      public IEnumerable<ToDoItem> GetTodos()
      {
         var users = new List<ToDoItem>();
         users.Add(new ToDoItem { Completed = false, Id = 1, Item = "test item 1" });
         users.Add(new ToDoItem { Completed = true, Id = 2, Item = "test item 2" });
         users.Add(new ToDoItem { Completed = false, Id = 3, Item = "test item 3" });

         return users;
      }

      [HttpGet]
      [Route("error")]
      public IEnumerable<ToDoItem> GetWithError()
      {   
         // Throw exception for catch in client
         throw new Exception("some dumb mistake happened");        
      }

      // Usage Tracking via Attribute
      [HttpGet]
      [TypeFilter(typeof(TrackUsageAttribute))]
      [Route("weather")]
      public IEnumerable<WeatherForecast> GetWeather()
      {
         var rng = new Random();

         //  Manual Logging
         _logger.Log(LogLevel.Warning, "warning something happened from controller in web API");
         _logger.Log(LogLevel.Error, "fake error from controller in web API");

         return Enumerable.Range(1, 5).Select(index => new WeatherForecast
         {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
         })
         .ToArray();
      }


   }
}
