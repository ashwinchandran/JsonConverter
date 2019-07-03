using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JsonConvertTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private WeatherLogic _weatherLogic;
        public WeatherController()
        {
            _weatherLogic = new WeatherLogic();
        }
        public async Task<ObjectResult> Get()
        {
           var result  = _weatherLogic.GetWeather();
           return Ok(result);
        }

    }
}