using SamplePipeline.Core.Dto.WeatherForecast;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SamplePipeline.Core.Pipeline.Interfaces
{
    public interface IWeatherForecastPipeline
    {
        Task<ListWheatherForecastResponseDto> Handle(ListWheatherForecastRequestDto request);
    }
}
