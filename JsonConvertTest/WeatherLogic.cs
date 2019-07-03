using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AutoMapper;

namespace JsonConvertTest
{
    public class WeatherLogic
    {
       

        public List<WeatherForecast> GetWeather()
        {
           
            return GetWeatherFromJson();
        }


        public List<WeatherForecast> GetWeatherFromJson()
        {
            var weatherForecasts = new List<WeatherForecast>();
            var pathBase = "weather";
            for (int i = 1; i < 4; i++)
            {
                var path = $"./{pathBase}{i}.json";
                
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();

                    var result = JsonConvert.DeserializeObject<WeatherForecast>(json);
                    weatherForecasts.Add(result);
                }
            }

            return weatherForecasts;
        }

    }
}
