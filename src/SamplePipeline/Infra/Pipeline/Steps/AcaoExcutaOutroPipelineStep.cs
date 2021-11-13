using Core.Pipelines.Interfaces;
using Infra.Pipelines;
using SamplePipeline.Core.Dto.WeatherForecast;
using SamplePipeline.Core.Pipeline.Interfaces;
using SamplePipeline.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SamplePipeline.Infra.Pipeline.Steps
{
    public class AcaoExcutaOutroPipelineStep : AbstractPipelineStep, IAcaoExcutaOutroPipelineStep
    {
        public override IEnumerable<IPipelinePackage> Execute(IPipelinePackage package)
        {
            package.AddContent("teste", "valor");

            yield return package;
        }
    }
}
