using Core.Pipelines.Interfaces;
using Core.Pipelines.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Pipelines
{
    public abstract class AbstractPipelineStep : IPipelineStep
    {
        public AbstractPipelineStep()
        {
            _inputRequiredContents = new List<string>();
            _outputRequiredContents = new List<string>();
            _inputTypeRequiredContents = new List<Type>();
            _outputTypeRequiredContents = new List<Type>();
        }
        private readonly IList<string> _inputRequiredContents;
        private readonly IList<string> _outputRequiredContents;
        private readonly IList<Type> _inputTypeRequiredContents;
        private readonly IList<Type> _outputTypeRequiredContents;

        public IList<string> InputRequiredContents
        {
            get
            {
                return _inputRequiredContents;
            }
        }

        public IList<string> OutputRequiredContents
        {
            get
            {
                return _outputRequiredContents;
            }
        }

        public IList<Type> InputTypeRequiredContents
        {
            get
            {
                return _inputTypeRequiredContents;
            }
        }

        public IList<Type> OutputTypeRequiredContents
        {
            get
            {
                return _outputTypeRequiredContents;
            }
        }

        public abstract IEnumerable<IPipelineStepResponseVM> Execute(IPipelinePackage package);
    }
}
