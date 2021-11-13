using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Pipelines.Interfaces
{
    public interface IPipelineBuilder
    {
        IPipeline Build();
        IPipeline Build(IPipelinePackage package);
    }
}
