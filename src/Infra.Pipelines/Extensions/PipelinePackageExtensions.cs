using Core.Pipelines;
using Core.Pipelines.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Infra.Pipelines.Extensions
{
    public static class PipelinePackageExtensions
    {
        public static IPipelinePackage ToPackage(this object obj, PipelineStatusCode defaultStatusCode = PipelineStatusCode.OK)
        {
            var result = new PipelinePackage(defaultStatusCode);
            result.AddContent(obj);
            return result;
        }

        public static IPipelinePackage LockPackage401(this IPipelinePackage package)
        {
            package.LockPackage(PipelineStatusCode.Unauthorized, "Acesso não autorizado!");
            return package;
        }
    }
}
