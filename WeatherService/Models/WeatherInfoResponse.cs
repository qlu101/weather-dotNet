using System.Collections.Generic;

namespace WeatherService.Models
{
    public class WeatherInfoResponse
    {
        public string status { get; set; }
        public string errorMessage { get; set; }
        public IEnumerable<WeatherInfo> results { get; set; }
    }
}