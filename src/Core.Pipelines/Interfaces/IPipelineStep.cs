using Core.Pipelines.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Pipelines.Interfaces
{
    public interface IPipelineStep
    {
        IList<Type> InputTypeRequiredContents { get; }
        IList<Type> OutputTypeRequiredContents { get; }
        IList<string> InputRequiredContents { get; }
        IList<string> OutputRequiredContents { get; }
        IEnumerable<IPipelineStepResponseVM> Execute(IPipelinePackage package);
    }
}
