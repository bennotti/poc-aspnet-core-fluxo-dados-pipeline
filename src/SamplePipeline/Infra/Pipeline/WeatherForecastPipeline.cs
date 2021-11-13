using Core.Pipelines.Interfaces;
using Infra.Pipelines.Extensions;
using SamplePipeline.Core.Dto.WeatherForecast;
using SamplePipeline.Core.Pipeline.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SamplePipeline.Infra.Pipeline
{
    public class WeatherForecastPipeline : IWeatherForecastPipeline
    {
        private readonly IPipelineBuilder _pipelineBuilder;
        public WeatherForecastPipeline(IPipelineBuilder pipelineBuilder)
        {
            _pipelineBuilder = pipelineBuilder;
        }
        public async Task<ListWheatherForecastResponseDto> Handle(ListWheatherForecastRequestDto request)
        {
            IPipelinePackage package = request.ToPackage();
            package.AddContent("valor", new { Valor = "a" });

            IPipeline pipeline = _pipelineBuilder.Build(package)
                .AddStep<IExcutaOutroPipelineStep>()
                .AddStep<ICacheObterWheatherPipelineStep>()
                //.AddStep<ILockSeCacheEncontrado>()
                .AddStep<IObterWheatherPipelineStep>()
                //.AddStep<IChamadaSocketIO>()
                .AddStep<ICacheSalvarWheatherPipelineStep>();

            package = await pipeline.Execute();

            return package.GetContent<ListWheatherForecastResponseDto>();
        }
    }
}
