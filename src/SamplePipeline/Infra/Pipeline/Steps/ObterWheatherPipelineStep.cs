﻿using Core.Pipelines.Interfaces;
using Core.Pipelines.ViewModels.Interfaces;
using Infra.Pipelines;
using Infra.Pipelines.Extensions;
using SamplePipeline.Core.Dto.WeatherForecast;
using SamplePipeline.Core.Pipeline.Interfaces;
using SamplePipeline.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SamplePipeline.Infra.Pipeline.Steps
{
    public class ObterWheatherPipelineStep : AbstractPipelineStep, IObterWheatherPipelineStep
    {
        private readonly IWeatherForecastService _service;
        public ObterWheatherPipelineStep(IWeatherForecastService service)
        {
            _service = service;
        }
        public override IEnumerable<IPipelineStepResponseVM> Execute(IPipelinePackage package)
        {
            ListWheatherForecastRequestDto request = package.GetContent<ListWheatherForecastRequestDto>();

            ListWheatherForecastResponseDto response = _service.Obter(request).GetAwaiter().GetResult();

            package.AddContent(response);

            yield return package.ToPipelineStepResponse();
        }
    }
}
