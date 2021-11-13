using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SamplePipeline.Core.Dto.WeatherForecast;
using SamplePipeline.Core.Pipeline.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamplePipeline.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastPipeline _pipeline;
        public WeatherForecastController(IWeatherForecastPipeline pipeline)
        {
            _pipeline = pipeline;
        }
        [HttpGet]
        public async Task<ListWheatherForecastResponseDto> Get()
        {
            return await _pipeline.Handle(new ListWheatherForecastRequestDto());
        }
    }
}
