using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Core.Pipelines.Interfaces
{
    public interface IPipelinePackage
    {
        bool IsLockedWithSuccessResponse { get; }
        string Message { get; }
        PipelineStatusCode StatusCode { get; }
        bool IsLocked { get; }
        IDictionary<string, object> Content { get; }
        string AddContent<T>(T obj);
        string AddContent<T>(string key, T obj);
        T GetContent<T>();
        T GetContent<T>(string key);
        void LockPackage(PipelineStatusCode statusCode = PipelineStatusCode.OK, string message = "");
    }
}
