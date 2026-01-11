using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RWD.Toolbox.Logging.Demo.Communication;
using RWD.Toolbox.Logging.Demo.MVC.Models;
using RWD.Toolbox.Logging.Infrastructure.Attribute;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RWD.Toolbox.Logging.Demo.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICommunicationAgent _commAgent;
        private readonly IConfiguration _configuration;
        private readonly string _apiBaseUrl;
        
        public HomeController(ICommunicationAgent commAgent, ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _commAgent = commAgent;
            _configuration = configuration;
            _apiBaseUrl = _configuration.GetSection("ApiSettings").GetValue<string>("BaseUrl");
        }

        // Track Usage via Attribute
        [TypeFilter(typeof(TrackUsageAttribute))]
        public async Task<IActionResult> Index()
        {
            var model = new WeatherForecasts();
            var f = await _commAgent.GetListFromApiAsync<WeatherForecast>($"{_apiBaseUrl}/api/data/weather", HttpContext, _logger);
            model.Forecasts.AddRange(f);

            // example of manual log
            _logger.Log(LogLevel.Information, "from MVC after Web API call for model....");

            return View(model);

        }

        // Track Usage and Performance  via Attribute 
        [TypeFilter(typeof(TrackUsageAttribute))]
        [TypeFilter(typeof(TrackPerformanceAttribute))]
        public async Task<IActionResult> PageTwo()
        {
            var dataList = await _commAgent.GetListFromApiAsync<ToDoItem>($"{_apiBaseUrl}/api/data/todos", HttpContext, _logger);
            return View();
        }

        // Example of Error coming from Web API Call
        public async Task<IActionResult> PageTwoError()
        {
            var dataList = await _commAgent.GetListFromApiAsync<ToDoItem>($"{_apiBaseUrl}/api/data/error", HttpContext, _logger);
            return View("Index");
        }


        // General Error Display to User
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
