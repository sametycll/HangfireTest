using HangfireTest.Dtos;
using HangfireTest.Models;
using HangfireTest.Repositories.WeatherRepositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace HangfireTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWeatherRepository _weatherRepository;


        public HomeController(ILogger<HomeController> logger, IWeatherRepository weatherRepository)
        {
            _logger = logger;
            _weatherRepository = weatherRepository;

        }

        public async Task<IActionResult> Index()
        {
            string key = "ed7e4ffd1577e625d9215150e74d5298";
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.openweathermap.org/data/2.5/weather?q=istanbul&appid=" + key
                + "&units=metric")
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                WeatherVM _weather = JsonConvert.DeserializeObject<WeatherVM>(body);
                var equal = await IsEqual(_weather);
                if (equal != true)
                {
                    await _weatherRepository.CreateAsync(_weather);

                }
            }

            return View();
        }


        public async Task<bool> IsEqual(WeatherVM WJson)
        {
            AddWeather dbWeather = await _weatherRepository.LastWVm();
            if (dbWeather != null)
            {
                if (
                   WJson.coord.lon != dbWeather.Lon ||
                   WJson.coord.lat != dbWeather.Lat ||
                   WJson.weather[0].id != dbWeather.WeatherId ||
                   WJson.weather[0].main != dbWeather.WeatherMain ||
                   WJson.weather[0].description != dbWeather.WeatherDescription ||                  
                   WJson.weather[0].icon != dbWeather.WeatherIcon ||
                   WJson.Base != dbWeather.Base ||
                   WJson.main.temp != dbWeather.Temp ||
                   WJson.main.feels_like != dbWeather.FeelsLike ||
                   WJson.main.temp_min != dbWeather.TempMin ||
                   WJson.main.temp_max != dbWeather.TempMax ||
                   WJson.main.pressure != dbWeather.Pressure ||
                   WJson.main.humidity != dbWeather.Humidity ||
                   WJson.visibility != dbWeather.Visibility ||
                   WJson.wind.deg != dbWeather.WindDeg ||
                   WJson.wind.speed != dbWeather.WindSpeed ||
                   WJson.clouds.all != dbWeather.CloudsAll ||
                   WJson.dt != dbWeather.Dt ||
                   WJson.sys.type != dbWeather.SysType ||
                   WJson.sys.id != dbWeather.SysId ||
                   WJson.sys.country != dbWeather.SysCountry ||
                   WJson.sys.sunrise != dbWeather.SysSunrise ||
                   WJson.sys.sunset != dbWeather.SysSunset ||
                   WJson.timezone != dbWeather.TimeZone ||
                   WJson.id != dbWeather.CityId ||
                   WJson.name != dbWeather.CityName ||
                   WJson.cod != dbWeather.Cod

                   ) { return false; }
                return true;
            }




            return false;
        }

















        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
