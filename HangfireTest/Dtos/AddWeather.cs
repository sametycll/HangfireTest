using System;
namespace HangfireTest.Dtos
{
    public class AddWeather
    {
        public DateTime CreateDate { get; set; }
        public float Lon { get; set; }
        public float Lat { get; set; }
        public int WeatherId { get; set; }
        public string WeatherMain { get; set; }
        public string WeatherDescription { get; set; }
        public string WeatherIcon { get; set; }
        public string Base { get; set; }
        public float Temp { get; set; }
        public int FeelsLike { get; set; }
        public float TempMin { get; set; }
        public float TempMax { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int Visibility { get; set; }
        public float WindSpeed { get; set; }
        public int WindDeg { get; set; }
        public int CloudsAll { get; set; }
        public int Dt { get; set; }
        public int SysType { get; set; }
        public int SysId { get; set; }
        public string SysCountry { get; set; }
        public int SysSunrise { get; set; }
        public int SysSunset { get; set; }
        public int TimeZone { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int Cod { get; set; }



    }
}
