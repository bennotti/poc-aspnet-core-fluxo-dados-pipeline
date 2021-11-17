using Core.Pipelines.Interfaces;
using Core.Pipelines.ViewModels;
using Core.Pipelines.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Pipelines.Extensions
{
    public static class PipelineStepExtensions
    {
        public static IPipelineStepResponseVM ToPipelineStepResponse(this IPipelinePackage package)
        {
            return new PipelineStepResponseVM(package);
        }
        public static IPipelineStepResponseVM ToPipelineStepResponse<T>(this IPipelinePackage package) where T : IPipelineEventStep
        {
            return new PipelineStepResponseVM<T>(package);
        }
    }
}
