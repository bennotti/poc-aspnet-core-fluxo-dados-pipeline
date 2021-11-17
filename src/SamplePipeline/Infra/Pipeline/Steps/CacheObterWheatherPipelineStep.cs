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
    public sealed class CacheObterWheatherPipelineStep : AbstractPipelineStep, ICacheObterWheatherPipelineStep
    {
        private readonly IMemoryCache _cache;
        public CacheObterWheatherPipelineStep(IMemoryCache cache)
        {
            _cache = cache;
        }
        public override IEnumerable<IPipelineStepResponseVM> Execute(IPipelinePackage package)
        {
            string jsonString;
            if (_cache.TryGetValue("wheather-forecast", out jsonString))
            {
                package.AddContent(JsonConvert.DeserializeObject<ListWheatherForecastResponseDto>(jsonString));
                
                package.LockPackage();

                yield return package.ToPipelineStepResponse();
                yield break;
            }

            yield return package.ToPipelineStepResponse();
        }
    }
}
