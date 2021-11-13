using Core.Pipelines;
using Core.Pipelines.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Infra.Pipelines
{
    public sealed class PipelinePackage : IPipelinePackage
    {
        public bool IsLockedWithSuccessResponse { get { return (int)_statusCode >= 200 && (int)_statusCode <= 299; } }
        public string Message { get { return _message; } }
        private string _message;
        [JsonIgnore]
        public PipelineStatusCode StatusCode { get { return _statusCode; } }
        private PipelineStatusCode _statusCode;
        private PipelineStatusCode _defaultStatusCode;

        private bool _isLocked;

        public bool IsLocked {
            get { return _isLocked; }
        }

        private readonly IDictionary<string, object> _package;
        public IDictionary<string, object> Content => _package;

        public PipelinePackage() {
            _package = new Dictionary<string, object>();
            _defaultStatusCode = PipelineStatusCode.OK;
        }

        public PipelinePackage(PipelineStatusCode defaultStatusCode)
        {
            _package = new Dictionary<string, object>();
            _defaultStatusCode = defaultStatusCode;
        }

        public T GetContent<T>() {
            object result = null;
            foreach(var item in _package) {
                if (item.Value is T) {
                    result = item.Value;
                    break;
                }
            }

            return (T)result;
        }

        public T GetContent<T>(string key) {
            return (T)_package[key];
        }

        public string AddContent<T>(T obj) {
            return AddContent<T>(Guid.NewGuid().ToString(), obj);
        }

        public string AddContent<T>(string key, T obj) {
            if (IsLocked) {
                return key;
            }
            _package.Add(key, obj);
            return key;
        }

        public void LockPackage(PipelineStatusCode statusCode = PipelineStatusCode.OK, string message = "")
        {
            if (IsLocked)
            {
                throw new Exception("Pipeline package is locked.");
            }

            _isLocked = true;
            if (statusCode == PipelineStatusCode.OK) {
                statusCode = _defaultStatusCode;
            }
            _statusCode = statusCode;
            _message = message;
        }
    }
}
