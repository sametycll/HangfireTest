using HangfireTest.Dtos;
using HangfireTest.Models;
using HangfireTest.Repositories.WeatherRepositories;
using Newtonsoft.Json;

namespace HangfireTest.Jobs
{
    public class JobTest
    {
        private IWeatherRepository _weatherRepository;

        public JobTest(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task GetWeather()
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







    }
}
