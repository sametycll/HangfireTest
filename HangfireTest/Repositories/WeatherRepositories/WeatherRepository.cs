using Dapper;
using HangfireTest.Context;
using HangfireTest.Dtos;
using HangfireTest.Models;

namespace HangfireTest.Repositories.WeatherRepositories
{
    public class WeatherRepository : IWeatherRepository
    {
        public readonly DbContext _dbContext;

        public WeatherRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(WeatherVM entity)
        {
            string Sql;
            Sql = "Insert Into Weather (CreateDate,Lon,Lat,WeatherId,WeatherMain,WeatherDescription,WeatherIcon,Base,Temp,";
            Sql += "FeelsLike,TempMin,TempMax,Pressure,Humidity,Visibility,WindSpeed,WindDeg,CloudsAll,Dt,";
            Sql += "SysType,SysId,SysCountry,SysSunrise,SysSunset,TimeZone,CityId,CityName,Cod) ";
            Sql += "Values(@CreateDate,@Lon,@Lat,@WeatherId,@WeatherMain,@WeatherDescription,@WeatherIcon,@Base,@Temp,";
            Sql += "@FeelsLike,@TempMin,@TempMax,@Pressure,@Humidity,@Visibility,@WindSpeed,@WindDeg,@CloudsAll,@Dt,";
            Sql += "@SysType,@SysId,@SysCountry,@SysSunrise,@SysSunset,@TimeZone,@CityId,@CityName,@Cod) Select @@IDENTITY";

            var parameters = new DynamicParameters();
            parameters.Add("@CreateDate",DateTime.Now);
            parameters.Add("@Lon", entity.coord.lon);
            parameters.Add("@Lat", entity.coord.lat);
            parameters.Add("@WeatherId", entity.weather[0].id);
            parameters.Add("@WeatherMain", entity.weather[0].main);
            parameters.Add("@WeatherDescription", entity.weather[0].description);
            parameters.Add("@WeatherIcon", entity.weather[0].icon);
            parameters.Add("@Base", entity.Base);
            parameters.Add("@Temp", entity.main.temp);
            parameters.Add("@FeelsLike", entity.main.feels_like);
            parameters.Add("@TempMin", entity.main.temp_min);
            parameters.Add("@TempMax", entity.main.temp_max);
            parameters.Add("@Pressure", entity.main.pressure);
            parameters.Add("@Humidity", entity.main.humidity);
            parameters.Add("@Visibility", entity.visibility);
            parameters.Add("@WindSpeed", entity.wind.speed);
            parameters.Add("@WindDeg", entity.wind.deg);
            parameters.Add("@CloudsAll", entity.clouds.all);
            parameters.Add("@Dt", entity.dt);
            parameters.Add("@SysType", entity.sys.type);
            parameters.Add("@SysId", entity.sys.id);
            parameters.Add("@SysCountry", entity.sys.country);
            parameters.Add("@SysSunrise", entity.sys.sunrise);
            parameters.Add("@SysSunset", entity.sys.sunset);
            parameters.Add("@TimeZone", entity.timezone);
            parameters.Add("@CityId", entity.id);
            parameters.Add("@CityName", entity.name);
            parameters.Add("@Cod", entity.cod);

            using(var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(Sql,parameters);
            }
        }
    }
}
