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

        public  void RunWeather()
        {
            //Console.WriteLine("çalışıyor");
            GetWeather();
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
                await _weatherRepository.CreateAsync(_weather);
            }
        }

    }
}
