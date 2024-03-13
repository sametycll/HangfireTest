using HangfireTest.Dtos;
using HangfireTest.Models;

namespace HangfireTest.Repositories.WeatherRepositories
{
    public interface IWeatherRepository
    {
        Task CreateAsync(WeatherVM entity);
    }
}
