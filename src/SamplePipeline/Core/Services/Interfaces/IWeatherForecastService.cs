using SamplePipeline.Core.Dto.WeatherForecast;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SamplePipeline.Core.Services.Interfaces
{
    public interface IWeatherForecastService
    {
        Task<ListWheatherForecastResponseDto> Obter(ListWheatherForecastRequestDto request);
    }
}
