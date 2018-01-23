using ForecastIO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WeatherService.Models;

namespace WeatherService.Controllers
{
    public class WeatherController : ApiController
    {
        public async Task<WeatherInfoResponse> GetAsyn(float latF, float longF)
        {
            string apiKey = "28896f6afb4e54d741e4d57dbe5d2749";
            List<WeatherInfo> pastWeekWeatherInfo = new List<WeatherInfo>();

            try
            {
                for (int i = -7; i <= -1; i++)  //for the past 7 days
                {
                    var date = DateTime.Today.AddDays(i);
                    var request = new ForecastIORequest(apiKey, latF, longF, date, Unit.us);
                    var response = await request.GetAsync();
                    var weatherInfoDate = UnixTimeStampToDateTime(response.daily.data[0].time);
                    pastWeekWeatherInfo.Add(new WeatherInfo
                    {
                        Date = weatherInfoDate.ToShortDateString(),
                        Summary = response.daily.data[0].summary,
                        DailyHighTemperature = response.daily.data[0].apparentTemperatureMax,
                        DailyLowTemperature = response.daily.data[0].apparentTemperatureMin
                    });
                }

                WeatherInfoResponse wiResponse = new WeatherInfoResponse
                {
                    status = "OK",
                    results = pastWeekWeatherInfo
                };

                return wiResponse;
            }
            catch(Exception ex)
            {
                WeatherInfoResponse wiResponse = new WeatherInfoResponse
                {
                    status = "Error",
                    errorMessage = ex.ToString()
                };

                return wiResponse;
            }
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime.Date;
        }
    }
}
