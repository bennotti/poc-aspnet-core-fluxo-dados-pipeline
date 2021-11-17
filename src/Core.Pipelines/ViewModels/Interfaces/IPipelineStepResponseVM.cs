using Core.Pipelines.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Pipelines.ViewModels.Interfaces
{
    public interface IPipelineStepResponseVM
    {
        IPipelinePackage Package { get; }
        Type StepToRunType { get; }
        bool IsRunStepBeforeContinue { get; }
    }

    public interface IPipelineStepResponseVM<TData> where TData : IPipelineStep
    {

    }
}
