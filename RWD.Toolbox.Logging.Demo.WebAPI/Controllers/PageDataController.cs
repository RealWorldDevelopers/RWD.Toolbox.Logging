using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RWD.Toolbox.Logging.Demo.ClassLibrary;
using RWD.Toolbox.Logging.Demo.Models.WebAPI;
using RWD.Toolbox.Logging.Demo.WebAPI.Models;
using RWD.Toolbox.Logging.Infrastructure.Attribute;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RWD.Toolbox.Logging.Demo.WebAPI.Controllers
{
    /// <summary>
    /// Location API
    /// </summary>
    [ApiController]
    [Consumes("application/json")]
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

        /// <summary>
        /// Test Call for No Erros
        /// </summary>
        /// <response code = "200" > Returns items in collection</response>
        /// <response code = "204" > If items collection is null</response>
        /// <response code = "401" > If access is Unauthorized</response>
        /// <response code = "403" > If access is Forbidden</response>
        /// <response code = "405" > If access is Not Allowed</response>
        /// <response code = "500" > If unhandled error</response>        
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status405MethodNotAllowed)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Test Call for Errors
        /// </summary>
        /// <response code = "200" > Returns items in collection</response>
        /// <response code = "204" > If items collection is null</response>
        /// <response code = "401" > If access is Unauthorized</response>
        /// <response code = "403" > If access is Forbidden</response>
        /// <response code = "405" > If access is Not Allowed</response>
        /// <response code = "500" > If unhandled error</response>        
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status405MethodNotAllowed)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("error")]
        public IEnumerable<ToDoItem> GetWithError()
        {
            // Throw exception for catch in client
            throw new Exception("some dumb mistake happened");
        }

        // TODO trackage attribute
        /// <summary>
        /// Test Call for Usage Tracking via Attribute
        /// </summary>
        /// <response code = "200" > Returns items in collection</response>
        /// <response code = "204" > If items collection is null</response>
        /// <response code = "401" > If access is Unauthorized</response>
        /// <response code = "403" > If access is Forbidden</response>
        /// <response code = "405" > If access is Not Allowed</response>
        /// <response code = "500" > If unhandled error</response>        
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status403Forbidden)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status405MethodNotAllowed)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
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
