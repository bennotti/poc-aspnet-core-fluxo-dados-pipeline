using Core.Pipelines.Interfaces;
using Core.Pipelines.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Pipelines.ViewModels.Interfaces;

namespace Core.Pipelines.ViewModels
{
    public class PipelineStepResponseVM : IPipelineStepResponseVM
    {
        private readonly IPipelinePackage _package;
        private readonly Type _stepToRunType;

        public Type StepToRunType => _stepToRunType;
        public bool IsRunStepBeforeContinue => _stepToRunType != null;
        public IPipelinePackage Package => _package;

        public PipelineStepResponseVM(IPipelinePackage package, Type stepToRunType = null)
        {
            _package = package;
            if (stepToRunType != null && stepToRunType.HasType(typeof(IPipelineEventStep)))
            {
                _stepToRunType = stepToRunType;
            }
        }
    }

    public sealed class PipelineStepResponseVM<TData> : PipelineStepResponseVM, IPipelineStepResponseVM<TData> where TData : IPipelineEventStep
    {
        public PipelineStepResponseVM(IPipelinePackage package) : base(package, typeof(TData)) { }
    }
}
