using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Pipelines.Interfaces
{
    public interface IPipeline
    {
        IPipelinePackage Package { get; }
        IList<string> RequiredContents { get; }
        Task ValidInputRequiredContent<T>(T step) where T : IPipelineStep;
        Task AddOutputRequiredContent<T>(T step) where T : IPipelineStep;
        Task<IPipeline> AddStepAsync<T>() where T : IPipelineStep;
        IPipeline AddStep<T>() where T : IPipelineStep;
        IPipeline AddEventStep<T>() where T : IPipelineEventStep;
        T GetService<T>();
        object GetService(Type type);
        Task<T> Execute<T>(bool lockWhenFinish = true);
        Task<IPipelinePackage> Execute(bool lockWhenFinish = true);
        Task ValidarOutputRequiredContent<T>(T step, IPipelinePackage package) where T : IPipelineStep;
    }
}
