using Core.Pipelines.Interfaces;
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
    public class AcaoExecutaEventoPipelineEventStep : AbstractPipelineStep, IAcaoExecutaEventoPipelineEventStep
    {
        public override IEnumerable<IPipelineStepResponseVM> Execute(IPipelinePackage package)
        {
            Console.WriteLine("=========================================");
            Console.WriteLine($"Executou: {nameof(AcaoExecutaEventoPipelineEventStep)}");
            Console.WriteLine("=========================================");

            yield return package.ToPipelineStepResponse();
        }
    }
}
