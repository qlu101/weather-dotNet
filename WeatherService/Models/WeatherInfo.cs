using System;

namespace WeatherService.Models
{
    public class WeatherInfo
    {
        public string Date { get; set; }
        public string Summary { get; set; }
        public float DailyHighTemperature { get; set; }
        public float DailyLowTemperature { get; set; }
    }
}