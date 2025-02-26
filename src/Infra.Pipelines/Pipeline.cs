﻿using Core.Pipelines.Extensions;
using Core.Pipelines.Interfaces;
using Core.Pipelines.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Pipelines
{
    public sealed class Pipeline : IPipeline
    {
        private readonly IList<IPipelineStep> _steps;
        private readonly IList<IPipelineEventStep> _events;
        private readonly IServiceProvider _serviceProvider;

        public IList<string> RequiredContents => new List<string>();

        private IPipelinePackage _package;
        public IPipelinePackage Package => _package;

        public Pipeline(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _steps = new List<IPipelineStep>();
            _events = new List<IPipelineEventStep>();
            _package = new PipelinePackage();
        }

        public Pipeline(IServiceProvider serviceProvider, IPipelinePackage package)
        {
            _serviceProvider = serviceProvider;
            _steps = new List<IPipelineStep>();
            _events = new List<IPipelineEventStep>();
            _package = package;
        }

        public async Task ValidInputRequiredContent<T>(T step) where T : IPipelineStep
        {
            if (step.InputRequiredContents == null) return;
            if (!step.InputRequiredContents.Any()) return;

            foreach (string input in step.InputRequiredContents)
            {
                bool containInPackage = _package.Content.ContainsKey(input);
                bool containInRequiredContents = RequiredContents.Contains(input);
                if (!containInPackage && !containInRequiredContents)
                {
                    throw new Exception($"O input \"{input}\" é obrigatorio e não foi encontrado.");
                }
            }

            await Task.CompletedTask;
        }

        public async Task AddOutputRequiredContent<T>(T step) where T : IPipelineStep
        {
            if (step.OutputRequiredContents == null) return;
            if (!step.OutputRequiredContents.Any()) return;

            RequiredContents.Concat(step.OutputRequiredContents);

            await Task.CompletedTask;
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public object GetService(Type type)
        {
            return _serviceProvider?.GetService(type);
        }

        public async Task<IPipeline> AddStepAsync<T>() where T : IPipelineStep
        {
            T step = GetService<T>();
            await ValidInputRequiredContent(step);
            await AddOutputRequiredContent(step);
            _steps.Add(step);

            return this;
        }

        public IPipeline AddStep<T>() where T : IPipelineStep
        {
            return this.AddStepAsync<T>().GetAwaiter().GetResult();
        }

        public IPipeline AddEventStep<T>() where T : IPipelineEventStep
        {
            _events.Add(GetService<T>());

            return this;
        }

        public async Task ValidarOutputRequiredContent<T>(T step, IPipelinePackage package) where T : IPipelineStep
        {
            if (step.OutputRequiredContents == null) return;
            if (!step.OutputRequiredContents.Any()) return;
            foreach (string output in step.OutputRequiredContents)
            {
                if (!package.Content.ContainsKey(output))
                {
                    throw new Exception($"O output \"{output}\" é obrigatorio e não foi encontrado.");
                }
            }
            await Task.CompletedTask;
        }

        private async Task ProcessStepBefore(IPipelineStepResponseVM stepResponse)
        {
            if (!stepResponse.IsRunStepBeforeContinue) return;
            IPipelineStep stepBefore = _events.FirstOrDefault(p =>
                p.GetType().HasType(stepResponse.StepToRunType)
            );

            if (stepBefore == null) return;

            await ProcessExecute(stepBefore);
        }

        private async Task ProcessExecute(IPipelineStep step)
        {
            foreach (IPipelineStepResponseVM stepResponse in step.Execute(_package))
            {
                _package = stepResponse.Package;
                if (_package.IsLocked)
                {
                    break;
                }
                await ValidarOutputRequiredContent(step, _package);
                await ProcessStepBefore(stepResponse);
            }
        }

        private async Task ProcessSteps(bool lockWhenFinish)
        {
            foreach (IPipelineStep step in _steps.Where(p => p != null))
            {
                await ProcessExecute(step);
                if (_package.IsLocked) break;
            }
            if (!_package.IsLocked && lockWhenFinish) _package.LockPackage(message: "Pipeline Processada!");
            await Task.CompletedTask;
        }

        public async Task<T> Execute<T>(bool lockWhenFinish = true)
        {
            await ProcessSteps(lockWhenFinish);
            return _package.GetContent<T>();
        }

        public async Task<IPipelinePackage> Execute(bool lockWhenFinish = true)
        {
            await ProcessSteps(lockWhenFinish);
            return _package;
        }
    }
}
