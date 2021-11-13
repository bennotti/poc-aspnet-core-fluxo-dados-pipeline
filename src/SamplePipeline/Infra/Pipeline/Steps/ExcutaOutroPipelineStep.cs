using Core.Pipelines.Interfaces;
using Infra.Pipelines;
using SamplePipeline.Core.Dto.WeatherForecast;
using SamplePipeline.Core.Pipeline.Interfaces;
using SamplePipeline.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SamplePipeline.Infra.Pipeline.Steps {
    public class ExcutaOutroPipelineStep : IExcutaOutroPipelineStep {
        private readonly IPipelineBuilder _pipelineBuilder;
        public ExcutaOutroPipelineStep(IPipelineBuilder pipelineBuilder) {
            _pipelineBuilder = pipelineBuilder;
        }

        public IList<string> InputRequiredContents => new List<string> {
            "valor"
        };

        public IList<string> OutputRequiredContents => new List<string>();

        public IList<Type> InputTypeRequiredContents => new List<Type>();

        public IList<Type> OutputTypeRequiredContents => new List<Type>();

        public IEnumerable<IPipelinePackage> Execute(IPipelinePackage package)
        {
            IPipeline pipeline = _pipelineBuilder.Build(package)
                .AddStep<IAcaoExcutaOutroPipelineStep>();

            package = pipeline.Execute(false).GetAwaiter().GetResult();

            yield return package;
        }
    }
}
