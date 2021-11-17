using Core.Pipelines.Interfaces;
using Core.Pipelines.ViewModels.Interfaces;
using Infra.Pipelines;
using Infra.Pipelines.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using SamplePipeline.Core.Dto.WeatherForecast;
using SamplePipeline.Core.Pipeline.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SamplePipeline.Infra.Pipeline.Steps
{
    public class CacheSalvarWheatherPipelineStep : AbstractPipelineStep, ICacheSalvarWheatherPipelineStep
    {
        private readonly IMemoryCache _cache;
        public CacheSalvarWheatherPipelineStep(IMemoryCache cache)
        {
            _cache = cache;
        }
        public override IEnumerable<IPipelineStepResponseVM> Execute(IPipelinePackage package)
        {
            ListWheatherForecastResponseDto response = package.GetContent<ListWheatherForecastResponseDto>();

            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(60));

            _cache.Set("wheather-forecast", JsonConvert.SerializeObject(response), options);

            yield return package.ToPipelineStepResponse();
        }
    }
}
