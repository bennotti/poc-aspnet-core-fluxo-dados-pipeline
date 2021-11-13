using Core.Pipelines.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Pipelines
{
    public sealed class PipelineBuilder : IPipelineBuilder
    {
        private readonly IServiceProvider _serviceProvider;
        public PipelineBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IPipeline Build(IPipelinePackage package)
        {
            return new Pipeline(_serviceProvider, package);
        }
        public IPipeline Build()
        {
            return new Pipeline(_serviceProvider);
        }
    }
}
