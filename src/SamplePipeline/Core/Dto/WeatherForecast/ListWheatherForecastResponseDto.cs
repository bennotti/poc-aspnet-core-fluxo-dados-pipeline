using System;
using System.Collections.Generic;
using System.Text;

namespace SamplePipeline.Core.Dto.WeatherForecast
{
    public class ListWheatherForecastResponseDto
    {
        public IEnumerable<WeatherForecastDto> Lista { get; set; }
    }
}
