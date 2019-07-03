using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonConvertTest
{
    [JsonConverter(typeof(WeatherDTOConverter))]
    public class WeatherForecast
    {
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("temperatureC")]
        public decimal? TemperatureC { get; set; }
        [JsonProperty("temperatureF")]
        public decimal? TemperatureF { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }

    }
}
